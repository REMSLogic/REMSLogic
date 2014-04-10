using System.Text;
using Framework.Security;
using Lib.Data;
using Lib.Systems.Notifications;

namespace Lib.Systems.Tasks
{
    public class CertificationReminder : BaseTaskRunner
    {
        #region Properties
        public long PrescriberId {get; set;}
        public long DrugId {get; set;}
        #endregion

        #region Base Class Implementation
        public override void Run()
        {
            Lib.Data.Drug drug = new Lib.Data.Drug(DrugId);
            Prescriber prescriber = new Prescriber(PrescriberId);
            User user = new User(prescriber.Profile.UserID);

            StringBuilder message = new StringBuilder();

            message.Append("Your certification for ");
            message.Append(drug.GenericName);
            message.Append(" needs to be renewed.");

            Notification n = NotificationService.Create(
                "Certification renewal notice", 
                message.ToString(), 
                true, 
                NotificationService.DataType.Drug, 
                DrugId);

            NotificationService.Send(n, user, NotificationService.Templates.HCOReminder);
        }
        #endregion
    }
}
