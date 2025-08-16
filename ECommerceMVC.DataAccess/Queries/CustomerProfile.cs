namespace ECommerceMVC.DataAccess.Queries
{
    public static class CustomerProfileQueries
    {
        // 1️⃣ Profil bilgilerini ekleme
        public const string InsertCustomerProfile = @"
            INSERT INTO CustomerProfile (CustomerId, FullName, [Address], City, PostalCode, PhoneNumber)
            VALUES (@CustomerId, @FullName, @Address, @City, @PostalCode, @PhoneNumber);
        ";

        // 2️⃣ Profil bilgilerini getirme (CustomerId'ye göre)
        public const string GetCustomerProfileByCustomerId = @"
            SELECT ProfileId, CustomerId, FullName, [Address], City, PostalCode, PhoneNumber
            FROM CustomerProfile
            WHERE CustomerId = @CustomerId;
        ";

        // 3️⃣ Profil bilgilerini güncelleme
        public const string UpdateCustomerProfile = @"
            UPDATE CustomerProfile
            SET FullName = @FullName,
                [Address] = @Address,
                City = @City,
                PostalCode = @PostalCode,
                PhoneNumber = @PhoneNumber
            WHERE CustomerId = @CustomerId;
        ";
    }
}
