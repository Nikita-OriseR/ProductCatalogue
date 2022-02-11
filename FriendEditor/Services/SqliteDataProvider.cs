using ProductCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace ProductCatalogue.Services
{
    public class SqliteDataProvider : IDataProvider
    {
        #region Constants

        public const string FileName = "Items.db";

        #endregion Constants

        #region Constructors

        public SqliteDataProvider()
        {
            DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);

            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = DataPath;
            ConnectionString = builder.ToString();

            if (!File.Exists(DataPath))
            {
                SQLiteConnection.CreateFile(DataPath);

                string sqlCreateTables = @"PRAGMA foreign_keys=on;

                                           CREATE TABLE Price(
                                           Id varchar(40) Primary Key,
                                           Price decimal(30,2));

                                           CREATE TABLE Items(
                                           Id varchar(40) Primary Key,
                                           IdPrice varchar(40),
                                           Code integer Not Null,
                                           Name string Not Null,
                                           BarCode string Not Null,
                                           Quantity decimal(38,2),
                                           Model string,
                                           Sort string,
                                           Color string,
                                           Size string,
                                           Wight string,
                                           DateChanges datetime,
                                           FOREIGN KEY (IdPrice) REFERENCES Price(Id))";

                ExeNonQueryCommand(sqlCreateTables);
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// The full data path of the sqlite database file
        /// </summary>
        public string DataPath { get; private set; }

        /// <summary>
        /// The connection string
        /// </summary>
        protected string ConnectionString { get; set; }

        #endregion Properties

        #region Methods

        public bool Delete(IItem item)
        {
            string sqlDelete = $@"DELETE FROM Items WHERE Id='{item.Id}'";
            return ExeNonQueryCommand(sqlDelete);
        }

        public List<IItem> GetAllItems()
        {
            var list = new List<IItem>();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sqlInsert = $@"SELECT * FROM Items";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlInsert, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                    {
                        var dataTable = new DataTable();
                        da.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            var item = new Item();
                            item.Id = (Guid)row["Id"];
                            item.IdPrice = (Guid)row["IdPrice"];
                            item.Code = (int)row["Code"];
                            item.Name = row["Name"].ToString();
                            item.BarCode = row["BarCode"].ToString();
                            item.Quantity = (Decimal)row["Quantity"];
                            item.Model = row["Model"] != null ? row["Model"].ToString() : string.Empty;
                            item.Sort = row["Sort"] != null ? row["Sort"].ToString() : string.Empty;
                            item.Color = row["Color"] != null ? row["Color"].ToString() : string.Empty;
                            item.Size = row["Size"] != null ? row["Size"].ToString() : string.Empty; ;
                            item.Wight = row["Wight"] != null ? row["Wight"].ToString() : string.Empty;
                            item.DateChanges = row["DateChanges"] != null ? (DateTime)(row["BirthDate"]) : DateTime.MinValue;

                            list.Add(item);
                        }

                    }
                }
            }

            return list;
        }

        public IItem GetItemById(string id)
        {
            Item item = null;
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sqlInsert = $@"SELECT * FROM Items WHERE Id='{id}'";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlInsert, conn))
                {
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        item = new Item();
                        item.Id = (Guid)dr["Id"];
                        item.IdPrice = (Guid)dr["IdPrice"];
                        item.Code = (int)dr["Code"];
                        item.Name = dr["Name"].ToString();
                        item.BarCode = dr["BarCode"].ToString();
                        item.Quantity = (Decimal)dr["Quantity"];
                        item.Model = dr["Model"] != null ? dr["Model"].ToString() : string.Empty;
                        item.Sort = dr["Sort"] != null ? dr["Sort"].ToString() : string.Empty;
                        item.Color = dr["Color"] != null ? dr["Color"].ToString() : string.Empty;
                        item.Size = dr["Size"] != null ? dr["Size"].ToString() : string.Empty; ;
                        item.Wight = dr["Wight"] != null ? dr["Wight"].ToString() : string.Empty;
                        item.DateChanges = dr["DateChanges"] != null ? (DateTime)(dr["BirthDate"]) : DateTime.MinValue;
                    }
                }
            }

            return item;
        }

        public bool Insert(IItem item)
        {
            string sqlInsert = $@"INSERT INTO Items VALUES('{item.Id}','{item.IdPrice}','{item.Code}','{item.Name}','{item.BarCode}','{item.Quantity}','{item.Model}','{item.Sort}','{item.Color}','{item.Size}','{item.Wight}','{item.DateChanges}')";
            return ExeNonQueryCommand(sqlInsert);
        }

        public bool Update(IItem item)
        {
            string sqlUpdate = $@"UPDATE Items SET Id='{item.Id}', IdPrice='{item.IdPrice}', Code='{item.Code}', Name='{item.Name}', BarCode='{item.BarCode}', Quantity='{item.Quantity}', Model='{item.Model}', Sort='{item.Sort}', Color='{item.Color}', Size='{item.Size}', Wight='{item.Wight}', DateChanges='{item.DateChanges}' WHERE Id='{item.Id}'";
            return ExeNonQueryCommand(sqlUpdate);
        }

        private bool ExeNonQueryCommand(string sqlCommandText)
        {
            bool isSuccess = false;

            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(sqlCommandText, conn))
                {
                    isSuccess = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }

            return isSuccess;
        }

        #endregion Methods
    }
}