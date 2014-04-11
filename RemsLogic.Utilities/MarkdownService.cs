using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ColorCode;
using MarkdownSharp;

namespace RemsLogic.Utilities
{
    // Most of this code was lifted from other places around the Internet.  I just
    // combined a few things and simplified some other things.
    public interface IMarkdownService
    {
        string ToHtml(string markdown);
    }

    public class MarkdownService : IMarkdownService
    {
       private class Transform
        {
            public Regex Pattern {get; set;}
            public ILanguage Language {get; set;}
            public string CssTag {get; set;}
        }

        private const string CODE_BLOCK_MARKER = "```";

        private readonly CodeColorizer _colorizer;
        private readonly Markdown _markdownSharp;
        private readonly bool _enableSyntaxHighlighting;

        private List<Transform> _transforms;

        public MarkdownService(bool enableSyntaxHighlighting = false)
        {
            // MarkdownSharp does not support tables.  MarkdownDeep does however,
            // MarkdownDeep does not support auto hyperlinks and emails.  It also
            // has an issue with the colorized code (adds an extra blank line).
            _enableSyntaxHighlighting = enableSyntaxHighlighting;

            _colorizer = new CodeColorizer();
            _markdownSharp = new Markdown(new MarkdownOptions
                {
                    AutoHyperlink = true,
                    AutoNewLines = false,
                    EncodeProblemUrlCharacters = true,
                    LinkEmails = true
                });

            InitializeTransforms();
        }

        #region IMarkdownService implementation
        public string ToHtml(string markdown)
        {
            if(String.IsNullOrEmpty(markdown))
                return markdown;

            if(_enableSyntaxHighlighting)
                return ApplyTransforms(markdown);

            return _markdownSharp.Transform(markdown);
        }
        #endregion

        #region Utility Methods
        private void InitializeTransforms()
        {
            const string format = @"^{0}([\s]*){1}(.*?){0}";

            // Other transformers will be added later.  This isn't really something
            // REMs logic needs.  I just want it.
            _transforms = new List<Transform>
            {
                new Transform
                { // C#
                    Pattern = new Regex(String.Format(format, CODE_BLOCK_MARKER, "(c#|csharp){1}"), RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled),
                    Language = Languages.CSharp,
                    CssTag = "csharp"
                },
                new Transform
                { // SQL
                    Pattern = new Regex(String.Format(format, CODE_BLOCK_MARKER, "sql"), RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled),
                    Language = Languages.Sql,
                    CssTag = "sql"
                },
                new Transform
                { // HTML
                    Pattern = new Regex(String.Format(format, CODE_BLOCK_MARKER, "html"), RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled),
                    Language = Languages.Html,
                    CssTag = "html"
                },
                new Transform
                { // JavaScript
                    Pattern = new Regex(String.Format(format, CODE_BLOCK_MARKER, "(js|javascript){1}"), RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled),
                    Language = Languages.JavaScript,
                    CssTag = "javascript"
                }
            };
        }

        private string ApplyTransforms(string markdown)
        {
            markdown = markdown.Replace("\r\n", "\n");
            markdown = markdown.Replace(@"\<", "&lt;").Replace(@"\>", "&gt;");

            foreach(Transform transform in _transforms)
            {
                Transform t = transform;
                markdown = transform.Pattern.Replace(markdown, m => PrepForColoriz(m.Value, t.Language, t.CssTag));
            }

            return _markdownSharp.Transform(markdown).Replace("\n", "\r\n");
        }

        protected virtual string PrepForColoriz(string value, ILanguage language = null, string cssTag = null)
        {
            var output = new StringBuilder();

            foreach(var line in value.Split(new[] {"\n"}, StringSplitOptions.None).Where(s => !s.StartsWith(CODE_BLOCK_MARKER)))
            {
                if(language == null)
                    output.Append(new string(' ', 4));

                output.AppendLine(line == "\n"? String.Empty : line);
            }

            if(language == null)
                return output.ToString();

            return _colorizer.Colorize(output.ToString().Trim(), language)
                .Replace("<pre>", String.Format("<pre><code class=\"{0}\">", cssTag))
                .Replace("</pre>","</code></pre>");
        }
        #endregion
    }
}
