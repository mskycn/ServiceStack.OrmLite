﻿using NUnit.Framework;
using ServiceStack.DataAnnotations;

namespace ServiceStack.OrmLite.MySql.Tests
{
    public class OrmLiteSaveStringValueTests : OrmLiteTestBase
    {
        [Test]
        public void Can_save_string_including_single_quote()
        {
            using (var db = ConnectionString.OpenDbConnection())
            using (var dbConn = db.CreateCommand())
            {
                dbConn.DropTable<StringTable>();
                dbConn.CreateTable<StringTable>(true);

                var text = "It worked! Didn't it?";
                var row = new StringTable() {Value = text};

                dbConn.Save(row);
                var id = dbConn.GetLastInsertId();

                var selectedRow = dbConn.GetById<StringTable>(id);
                Assert.AreEqual(text, selectedRow.Value);
            }
        }

        [Test]
        public void Can_save_string_including_double_quote()
        {
            using (var db = ConnectionString.OpenDbConnection())
            using (var dbConn = db.CreateCommand())
            {
                dbConn.DropTable<StringTable>();
                dbConn.CreateTable<StringTable>(true);

                var text = "\"It worked!\"";
                var row = new StringTable() { Value = text };

                dbConn.Save(row);
                var id = dbConn.GetLastInsertId();

                var selectedRow = dbConn.GetById<StringTable>(id);
                Assert.AreEqual(text, selectedRow.Value);
            }
        }

        [Test]
        public void Can_save_string_including_backslash()
        {
            using (var db = ConnectionString.OpenDbConnection())
            using (var dbConn = db.CreateCommand())
            {
                dbConn.DropTable<StringTable>();
                dbConn.CreateTable<StringTable>(true);

                var text = "\\\\mycomputer\\hasashareddirectory";
                var row = new StringTable() { Value = text };

                dbConn.Save(row);
                var id = dbConn.GetLastInsertId();

                var selectedRow = dbConn.GetById<StringTable>(id);
                Assert.AreEqual(text, selectedRow.Value);
            }
        }


    }

    public class StringTable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
