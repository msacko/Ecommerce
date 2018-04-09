using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Takadam.Tools;

namespace Takadam.Models
{
    public class CategoryProduct
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        private static List<CategoryProduct> get(string query, Dictionary<string, object> parameters)
        {
            CategoryProduct item = null;
            Connection cs = new Connection();
            List<CategoryProduct> list = new List<CategoryProduct>();

            MySqlDataReader reader = cs.Select(query, parameters);
            while (reader.Read())
            {
                item = new CategoryProduct();

                if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    item.id = reader.GetInt32(reader.GetOrdinal("id"));

                if (!(reader["name"] == DBNull.Value))
                    item.name = reader["name"].ToString().Trim();

                if (!(reader["description"] == DBNull.Value))
                    item.description = reader["description"].ToString().Trim();

                list.Add(item);
            }

            cs.CloseConnection();
            return list;
        }

        public static List<CategoryProduct> GetAll()
        {
            string query = @"SELECT * from categoryProduct";
            Dictionary<string, object> param = new Dictionary<string, object>();

            return get(query, param);
        }

        public static CategoryProduct GetById(int id)
        {
            string query = @"SELECT * from categoryProduct where id = @id";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", id);

            List<CategoryProduct> list = get(query, param);

            //If the list contains an element return the corresponding element
            return list.Any() ? list.First() : null;
        }

        public static CategoryProduct GetByName(string name)
        {
            string query = @"SELECT * from categoryProduct where name = @name";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("name", name);

            List<CategoryProduct> list = get(query, param);

            //If the list contains an element return the corresponding element
            return list.Any() ? list.First() : null;
        }

        public static int Add(CategoryProduct m)
        {
            int t = 0;
            string query = @"INSERT INTO categoryProduct (name, description) " +
                                      "VALUES (@name, @description);";
            Connection cs = new Connection();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("name", m.name);
            param.Add("description", m.description);

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
