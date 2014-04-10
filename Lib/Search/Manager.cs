using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Search
{
	public class Manager
	{
		private static object indexLock;
		private static Lucene.Net.Store.Directory dir;
		private static Lucene.Net.Index.IndexWriter indexWriter;
		private static string dirLoc;
		private static bool createIndex;

		static Manager()
		{
			indexLock = new object();
			dirLoc = Framework.Config.Manager.GetSection<Config.LuceneSection>( "lucene" ).IndexDirectory;
		}

		public static void Init()
		{
			if( !System.IO.Directory.Exists( dirLoc ) || System.IO.Directory.GetFiles( dirLoc ).Length == 0 || System.IO.Directory.GetDirectories( dirLoc ).Length == 0 )
				createIndex = true;
			else
				createIndex = false;
			dir = Lucene.Net.Store.FSDirectory.Open( new System.IO.DirectoryInfo( dirLoc ) );
		}
		private static string[] fields = new string[] {
			"generic-name",
			"indication",
			"formulation",
			"brand-name",
			"company-name",
			"username",
			"email",
			"name",
			"phone",
			"website",
			"contact-name",
			"city",
			"state",
			"zip",
			"npi"
		};
		// DOCS: http://lucenenet.apache.org/docs/3.0.3/annotated.html
		public static List<SearchResult> Query(string q)
		{
			if( dir == null )
				Init();

			var searcher = new Lucene.Net.Search.IndexSearcher( dir, true );
			
			Lucene.Net.Analysis.Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
			var parser = new Lucene.Net.QueryParsers.MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29, fields, analyzer);
			var query = parser.Parse( q+"*" );

			var resultDocs = searcher.Search( query, 100 );
			var hits = resultDocs.ScoreDocs;

			var ret = new List<SearchResult>();

			//iterate over the results.
			for( int i = 0; i < hits.Length; i++ )
			{
				var doc = searcher.Doc( hits[i].Doc );
				string type = doc.Get( "object-type" );
				long iid = long.Parse(doc.Get("object-id"));
				object item = null;

				// TODO: Check permissions of logged in user
				switch (type)
				{
				case "prescriber":
					item = new Data.Prescriber(iid);
					break;
				case "provider":
					item = new Data.Provider(iid);
					break;
				case "drug-company":
					item = new Data.DrugCompany(iid);
					break;
				case "drug-system":
					item = new Data.DrugSystem(iid);
					break;
				case "drug":
					item = new Data.Drug(iid);
					break;
				case "user-profile":
					item = new Data.UserProfile(iid);
					break;
				// TODO: Add other searchable types here
				}

				ret.Add(new SearchResult(type, item));
			}

			searcher.Close();

			return ret;
		}

		public static void RemoveDocuments()
		{
			if (dir == null)
				Init();

			if (indexWriter == null)
			{
				lock (indexLock)
				{
					if (indexWriter == null)
					{
						//create an analyzer to process the text
						Lucene.Net.Analysis.Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);

						//create the index writer with the directory and analyzer defined.
						indexWriter = new Lucene.Net.Index.IndexWriter(dir, analyzer, createIndex, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);
						createIndex = false;
					}
				}
			}

			lock (indexLock)
			{
				indexWriter.DeleteAll();
				
				//optimize and close the writer
				indexWriter.Optimize();
				indexWriter.Commit();
			}
		}

		public static void UpdateDocument(Framework.Data.ActiveRecord item)
		{
			if (item == null || !item.ID.HasValue)
				throw new ArgumentNullException("The item must not be null and have been saved before indexing it.");

			if( dir == null )
				Init();

			if( indexWriter == null )
			{
				lock( indexLock )
				{
					if( indexWriter == null )
					{
						//create an analyzer to process the text
						Lucene.Net.Analysis.Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer( Lucene.Net.Util.Version.LUCENE_29 );

						//create the index writer with the directory and analyzer defined.
						indexWriter = new Lucene.Net.Index.IndexWriter( dir, analyzer, createIndex, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED );
						createIndex = false;
					}
				}
			}

			string type = "";
			long iid = item.ID.Value;

			//create a document, add in a single field
			var doc = new Lucene.Net.Documents.Document();

			doc.Add(new Lucene.Net.Documents.Field("object-id", iid.ToString(), Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.NOT_ANALYZED, Lucene.Net.Documents.Field.TermVector.NO));

			switch (item.GetType().FullName)
			{
			case "Lib.Data.Prescriber":
				type = "prescriber";
				UpdateDocument_Prescriber(ref doc, (Data.Prescriber)item);
				break;
			case "Lib.Data.Provider":
				type = "provider";
				UpdateDocument_Provider(ref doc, (Data.Provider)item);
				break;
			case "Lib.Data.DrugCompany":
				type = "drug-company";
				UpdateDocument_DrugCompany(ref doc, (Data.DrugCompany)item);
				break;
			case "Lib.Data.DrugSystem":
				type = "drug-system";
				UpdateDocument_DrugSystem(ref doc, (Data.DrugSystem)item);
				break;
			case "Lib.Data.Drug":
				type = "drug";
				UpdateDocument_Drug(ref doc, (Data.Drug)item);
				break;
			case "Lib.Data.UserProfile":
				type = "user-profile";
				UpdateDocument_UserProfile(ref doc, (Data.UserProfile)item);
				break;
			default:
				throw new ArgumentException("Unsupported record type ["+item.GetType().FullName+"].");
			}

			string unique_id = type+":"+iid.ToString();

			doc.Add(new Lucene.Net.Documents.Field("object-type", type, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.NOT_ANALYZED, Lucene.Net.Documents.Field.TermVector.NO));
			doc.Add(new Lucene.Net.Documents.Field("object-unique-id", unique_id, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.NOT_ANALYZED, Lucene.Net.Documents.Field.TermVector.NO));

			lock( indexLock )
			{
				indexWriter.DeleteDocuments(new Lucene.Net.Index.Term("object-unique-id", unique_id));

				//write the document to the index
				indexWriter.AddDocument( doc );

				//optimize and close the writer
				indexWriter.Optimize();
				indexWriter.Commit();
			}
		}

		protected static void UpdateDocument_Prescriber(ref Lucene.Net.Documents.Document doc, Data.Prescriber item)
		{
			doc.Add(new Lucene.Net.Documents.Field("npi", item.NpiId, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			doc.Add(new Lucene.Net.Documents.Field("username", item.Profile.User.Username, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			doc.Add(new Lucene.Net.Documents.Field("email", item.Profile.User.Email, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			doc.Add(new Lucene.Net.Documents.Field("name", item.Profile.PrimaryContact.Name, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			doc.Add(new Lucene.Net.Documents.Field("email", item.Profile.PrimaryContact.Email, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
		}

		protected static void UpdateDocument_Provider(ref Lucene.Net.Documents.Document doc, Data.Provider item)
		{
			doc.Add(new Lucene.Net.Documents.Field("name", item.Name, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			doc.Add(new Lucene.Net.Documents.Field("contact-name", item.PrimaryContact.Name, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			doc.Add(new Lucene.Net.Documents.Field("city", item.Address.City, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			doc.Add(new Lucene.Net.Documents.Field("state", item.Address.State, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			doc.Add(new Lucene.Net.Documents.Field("zip", item.Address.Zip, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
		}

		protected static void UpdateDocument_DrugCompany(ref Lucene.Net.Documents.Document doc, Data.DrugCompany item)
		{
			doc.Add(new Lucene.Net.Documents.Field("name", item.Name, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			if( !string.IsNullOrEmpty(item.Phone) )
				doc.Add(new Lucene.Net.Documents.Field("phone", item.Phone, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			if( !string.IsNullOrEmpty(item.Website) )
				doc.Add(new Lucene.Net.Documents.Field("website", item.Website, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
		}

		protected static void UpdateDocument_DrugSystem(ref Lucene.Net.Documents.Document doc, Data.DrugSystem item)
		{
			doc.Add(new Lucene.Net.Documents.Field("name", item.Name, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
		}

		protected static void UpdateDocument_Drug(ref Lucene.Net.Documents.Document doc, Data.Drug item)
		{
			doc.Add(new Lucene.Net.Documents.Field("generic-name", item.GenericName, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			doc.Add(new Lucene.Net.Documents.Field("indication", item.Indication, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));

			var formulations = Data.DrugFormulation.FindByDrug(item);
			if (formulations != null && formulations.Count > 0)
			{
				foreach (var f in formulations)
				{
					doc.Add(new Lucene.Net.Documents.Field("formulation", f.Formulation.Name, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
					doc.Add(new Lucene.Net.Documents.Field("brand-name", f.BrandName, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
					doc.Add(new Lucene.Net.Documents.Field("company-name", f.DrugCompany.Name, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
				}
			}
		}

		protected static void UpdateDocument_UserProfile(ref Lucene.Net.Documents.Document doc, Data.UserProfile item)
		{
			var user = item.User;
			doc.Add(new Lucene.Net.Documents.Field("username", user.Username, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			doc.Add(new Lucene.Net.Documents.Field("email", user.Email, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));

			var contact = item.PrimaryContact;
			if (contact != null)
			{
				doc.Add(new Lucene.Net.Documents.Field("name", contact.Name, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES));
			}
		}

		// TODO: Add other searchable types here

		public class SearchResult
		{
			public string type;
			public object item;
			public T GetItem<T>() { return (T)item; }

			public SearchResult(string type, object item)
			{
				this.type = type;
				this.item = item;
			}
		}
	}
}
