namespace ServiceLayer.Messages.WebApplication
{
    public class NotificationMessagesWebApplication
    {
        private const string BaseAddMessage = " Has been saved! ";
        private const string BaseUpdateMessage = " Has been updated! ";
        private const string BaseDeleteMessage = " Has been deleted! ";

        public const string SuccessedTitle = " Congratulations ";
        public const string FailedTitle = " Eroor ";

        public static string AddMessages(string subject) => subject + BaseAddMessage;
        public static string UpdateMessages(string subject) => subject + BaseUpdateMessage;
        public static string DeleteMessages(string subject) => subject + BaseDeleteMessage;
    }
}
