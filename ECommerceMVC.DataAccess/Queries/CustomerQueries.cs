namespace ECommerceMVC.DataAccess.Queries
{
    public class CustomerQueries
    {
        public static string GetCustomerByCustomerName = @"
        SELECT CustomerID, CustomerName, CustomerPassword, Address, City, Country, Phone 
        FROM Customers 
        WHERE CustomerName = @CustomerName";

        public static string GetCustomerByCustomerID = @"
        SELECT CustomerID, CustomerName, CustomerPassword, Address, City, Country, Phone 
        FROM Customers 
        WHERE CustomerID = @CustomerID";

        public static string GetCustomerInfo = @"
        SELECT CustomerID, CustomerName, CustomerPassword, Address, City, Country, Phone 
        FROM Customers 
        WHERE CustomerName = @CustomerName AND CustomerPassword = @CustomerPassword";

        public static string CreateCustomer = @"
        INSERT INTO Customers (CustomerName, CustomerPassword) 
        VALUES (@CustomerName, @CustomerPassword)";
    }
}
