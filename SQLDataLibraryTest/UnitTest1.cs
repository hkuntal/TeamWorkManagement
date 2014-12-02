using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataLibrary;

namespace SQLDataLibraryTest
{
    [TestClass]
    public class UnitTest1
    {
        private const string connectionString =
            @"Persist Security Info=False;User ID=sa; password = sa123; Initial Catalog=MySociety;Data Source=.\SQLEXPRESS";
        private const string connectionString1 =
            @"Persist Security Info=False;User ID=sa; password = sa123; Initial Catalog=MySociety;Data Source=3.20.133.36";

        [TestMethod]
        public void DatabaseErrorsWithQuery()
        {
            var db = new Database(connectionString1);

            var result = db.ExecuteCommand("select * from flat");
            Assert.IsTrue(result.Rows.Count > 0);
        }

        [TestMethod]
        public void DatabaseErrorsWithSP()
        {
            // Create multiple connections
            
            var db = new Database(connectionString1);

            var result = db.ExecuteStoreProc("GetFlatDetails");
            Assert.IsTrue(result.Rows.Count > 0);
        }
    }
}
