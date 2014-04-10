using System.Text;
using Framework.Security;
using Lib.Systems;

namespace Lib.Data.Tasks
{
    public class CertificationReminder : BaseTask
    {
        #region Properties
        public long PrescriberId {get; set;}
        public long DrugId {get; set;}
        #endregion

        #region Base Class Implementation
        public override void Run()
        {
            Drug drug = new Drug(DrugId);
            Prescriber prescriber = new Prescriber(PrescriberId);
            User user = new User(prescriber.Profile.UserID);

            StringBuilder message = new StringBuilder();

            message.Append("Your certification for ");
            message.Append(drug.GenericName);
            message.Append(" needs to be renewed.");

			Notification n = Lib.Systems.Notifications.NotificationService.Create(
                "Certification renewal notice", 
                message.ToString(), 
                true,
				Lib.Systems.Notifications.NotificationService.DataType.Drug, 
                DrugId);

			Lib.Systems.Notifications.NotificationService.Send(n, user);
        }
        #endregion
    }
}
