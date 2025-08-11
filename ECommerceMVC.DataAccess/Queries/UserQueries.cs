namespace ECommerceMVC.DataAccess.Queries
{
    public class UserQueries
    {
        public static string GetUserByUserName = @"SELECT UserID, UserName, Password FROM Users WHERE UserName = @UserName";
        public const string GetUserInfo = "SELECT * FROM Users WHERE Username = @UserName AND Password = @Password";
        public const string CreateUser = @"
            INSERT INTO Users (UserName, Password)
            VALUES (@UserName, @Password)";
    }
}
