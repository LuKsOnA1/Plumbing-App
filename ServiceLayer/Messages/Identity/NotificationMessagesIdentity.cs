namespace ServiceLayer.Messages.Identity
{
    public static class NotificationMessagesIdentity
    {
        //Admin Area Messages
        public const string LogInSuccess = "You have logged in. Have fun";
        public const string PasswordResetSuccess = "Your password reset link has been sent to your Email!";
        public const string TokenValidationError = "Your token is no more valid. Please try again!";
        public const string UserError = "User does not exist!";
        public const string PasswordChangeSuccess = "Your password has been changed. Please try to Log In";

        public const string ExtandClaimSuccess = "User has 5 more days from today!";
        public const string ExtandClaimFailed = "User extend method is failed!";

        //User Area Messages
        private const string UserEditSuccess = "Has been updated!";
        private const string SignUpSuccess = "Has been created";

        public static string SignUp(string userName) => userName + SignUpSuccess;
        public static string UserEdit(string userName) => userName + UserEditSuccess;

        // Titles
        public const string SuccessedTitle = " Congratulations ";
        public const string FailedTitle = " Eroor ";
    }
}
