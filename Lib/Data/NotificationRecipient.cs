namespace Lib.Data
{
    public class NotificationRecipient
    {
        public static class Type
        {
            public const int Undefined = 0;
            public const int User = 1;
            public const int DistributionList = 2;
            public const int Facility = 3;
        };

        public int RecipientType;
        public long Id;
    }
}
