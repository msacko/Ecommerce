using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Takadam.Tools;

namespace Takadam.Models
{
    public class ProductFile
    {
        public int id { get; set; }
        public int productId { get; set; }
        public string path { get; set; }
        public bool isProfile { get; set; }
        public DateTime date { get; set; }

        private static List<ProductFile> get(string query, Dictionary<string, object> parameters)
        {
            ProductFile item = null;
            Connection cs = new Connection();
            List<ProductFile> list = new List<ProductFile>();

            MySqlDataReader reader = cs.Select(query, parameters);

            while (reader.Read())
            {
                item = new ProductFile();

                if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    item.id = reader.GetInt32(reader.GetOrdinal("id"));

                if (!reader.IsDBNull(reader.GetOrdinal("productId")))
                    item.productId = reader.GetInt32(reader.GetOrdinal("producId"));

                if (!(reader["path"] == DBNull.Value))
                    item.path = reader["path"].ToString().Trim();

                if (!reader.IsDBNull(reader.GetOrdinal("isProfile")))
                    item.isProfile = reader.GetBoolean(reader.GetOrdinal("isProfile"));

                if (!reader.IsDBNull(reader.GetOrdinal("date")))
                    item.date = reader.GetDateTime(reader.GetOrdinal("date"));

                list.Add(item);
            }

            cs.CloseConnection();
            return list;
        }

        public static List<ProductFile> GetAll()
        {
            string query = @"SELECT * from productFile";
            Dictionary<string, object> param = new Dictionary<string, object>();

            return get(query, param);
        }

        public static ProductFile GetById(int id)
        {
            string query = @"SELECT * from productFile where id = @id";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", id);

            List<ProductFile> list = get(query, param);

            //If the list contains an element return the corresponding element
            return list.Any() ? list.First() : null;
        }

        public static List<ProductFile> GetByProductId(int productId)
        {
            string query = @"SELECT * from productFile where productId = @productId";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("productId", productId);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static List<ProductFile> GetByPath(string path)
        {
            string query = @"SELECT * from productFile where path = @path";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("path", path);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static int Add(ProductFile m)
        {
            int t = 0;
            string query = @"INSERT INTO productFile (productId, path, isProfile, date) " +
                "VALUES (@productId, @path, @isProfile, NOW());";
            Connection cs = new Connection();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("productId", m.productId);
            param.Add("path", m.path);
            param.Add("isProfile", m.isProfile);

            try
            {
                t = cs.Insert(query, param);
                //si insertion reussit

            }
            catch (MySqlException e) { }
            cs.CloseConnection();
            //si insertion echoue
            return t;
        }
    }
}
