using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.DataAccess.Queries
{
    public static class ProductSqlQueries
    {
        public const string GetAllProduct = "SELECT p.ProductID, p.ProductName, c.CategoryName, p.UnitPrice\r\n    FROM Products p\r\n    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID";
        public const string AddProduct = @"
                       INSERT INTO Products
                       (ProductID, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
                       VALUES
                       (@ProductID, @ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued)";
        public const string UpdateProduct = @"
                UPDATE Products SET
                    ProductName = @ProductName,
                    SupplierID = @SupplierID,
                    CategoryID = @CategoryID,
                    QuantityPerUnit = @QuantityPerUnit,
                    UnitPrice = @UnitPrice,
                    UnitsInStock = @UnitsInStock,
                    UnitsOnOrder = @UnitsOnOrder,
                    ReorderLevel = @ReorderLevel,
                    Discontinued = @Discontinued
                WHERE ProductID = @ProductID";

        public const string GetProductsByCategory = @"
         SELECT p.ProductID, p.ProductName, c.CategoryName, p.UnitPrice
         FROM Products p
         LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
         WHERE p.CategoryID = @CategoryID";


        public const string GetTop5BestSellingProducts = @"
         SELECT TOP 5 
         p.ProductID, p.ProductName, c.CategoryName, p.UnitPrice, SUM(od.Quantity) AS TotalSold
         FROM Products p
         INNER JOIN Categories c ON p.CategoryID = c.CategoryID
         INNER JOIN [Order Details] od ON p.ProductID = od.ProductID
         GROUP BY p.ProductID, p.ProductName, c.CategoryName, p.UnitPrice
         ORDER BY TotalSold DESC";

        public static string GetProductsByCategoryName = @"
         SELECT 
         p.ProductID, 
         p.ProductName, 
         p.UnitPrice, 
         c.CategoryName 
         FROM Products p
         JOIN Categories c ON p.CategoryID = c.CategoryID
         WHERE c.CategoryName = @CategoryName";
    }
}

