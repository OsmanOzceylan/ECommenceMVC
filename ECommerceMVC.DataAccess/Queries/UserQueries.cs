namespace ECommerceMVC.DataAccess.Queries
{
    public static class UserQueries
    {
        public const string GetUserInfo = "SELECT * FROM Users WHERE Username = @UserName AND Password = @Password";
    }
}
