using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper; 
namespace ECommerceMVC.DataAccess.Queries
{
    public static class CategorySqlQueries
    {
        public const string GetAllCategories = "SELECT CategoryID, CategoryName, Description FROM Categories";

    }

}
