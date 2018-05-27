using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Takadam.Tools;

namespace Takadam.Models
{
    public class Product
    {
        public int id { get; set; }
        public int noProd { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public string price { get; set; }
        public string status { get; set; }
        public int nbStock { get; set; }
        public string country { get; set; }

        public int memberId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string memberCountry { get; set; }

        public DateTime dateAjout { get; set; }
        public string statusAjout { get; set; }


        private static List<Product> get(string query, Dictionary<string, object> parameters)
        {      
            Connection cs = new Connection();
            List<Product> list = new List<Product>();

            MySqlDataReader reader = cs.Select(query, parameters);

            while (reader.Read())
            {
                Product item = new Product();

                if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    item.id = reader.GetInt32(reader.GetOrdinal("id"));

                if (!reader.IsDBNull(reader.GetOrdinal("noProd")))
                    item.noProd = reader.GetInt32(reader.GetOrdinal("noProd"));

                if (!(reader["name"] == DBNull.Value))
                    item.name = reader["name"].ToString().Trim();

                if (!(reader["description"] == DBNull.Value))
                    item.description = reader["description"].ToString().Trim();

                if (!reader.IsDBNull(reader.GetOrdinal("categoryId")))
                    item.categoryId = reader.GetInt32(reader.GetOrdinal("categoryId"));

                if (!(reader["price"] == DBNull.Value))
                    item.price = reader["price"].ToString().Trim();

                if (!(reader["status"] == DBNull.Value))
                    item.status = reader["status"].ToString().Trim();

                if (!reader.IsDBNull(reader.GetOrdinal("nbStock")))
                    item.nbStock = reader.GetInt32(reader.GetOrdinal("nbStock"));

                if (!(reader["country"] == DBNull.Value))
                    item.country = reader["country"].ToString().Trim();


                //Member Info
                if (!reader.IsDBNull(reader.GetOrdinal("memberId")))
                    item.memberId = reader.GetInt32(reader.GetOrdinal("memberId"));

                if (!(reader["firstname"] == DBNull.Value))
                    item.firstname = reader["firstname"].ToString().Trim();

                if (!(reader["lastname"] == DBNull.Value))
                    item.lastname = reader["lastname"].ToString().Trim();

                if (!(reader["email"] == DBNull.Value))
                    item.email = reader["email"].ToString().Trim();

                if (!(reader["memberCountry"] == DBNull.Value))
                    item.memberCountry = reader["memberCountry"].ToString().Trim();


                //Member product Info
                if (!(reader["statusAjout"] == DBNull.Value))
                    item.statusAjout = reader["statusAjout"].ToString().Trim();

                if (!reader.IsDBNull(reader.GetOrdinal("dateAjout")))
                    item.dateAjout = reader.GetDateTime(reader.GetOrdinal("dateAjout"));

                list.Add(item);
            }

            cs.CloseConnection();
            return list;
        }


        public static List<Product> GetAll()
        {
            string query = @"SELECT p.*, mp.status as statusAjout, mp.date as dateAjout, m.id as memberId, m.firstname, m.lastname, m.email, m.country as memberCountry from product p inner join memberproduct mp on p.id = mp.productId inner join member m on mp.memberId = m.id";
            Dictionary<string, object> param = new Dictionary<string, object>();

            return get(query, param);
        }


        public static Product GetById(int id)
        {
            string query = @"SELECT p.*, mp.status as statusAjout, mp.date as dateAjout, m.id as memberId, m.firstname, m.lastname, m.email, m.country as memberCountry from product p inner join memberproduct mp on p.id = mp.productId inner join member m on mp.memberId = m.id where p.id = @id";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", id);

            List<Product> list = get(query, param);

            //If the list contains an element return the corresponding element
            return list.Any() ? list.First() : null;
        }

        public static List<Product> GetByCategoryId(int categoryId)
        {
            string query = @"SELECT p.*, mp.status as statusAjout, mp.date as dateAjout, m.id as memberId, m.firstname, m.lastname, m.email, m.country as memberCountry from product p inner join memberproduct mp on p.id = mp.productId inner join member m on mp.memberId = m.id where p.categoryId = @categoryId";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("categoryId", categoryId);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static List<Product> GetByStatus(string status)
        {
            string query = @"SELECT p.*, mp.status as statusAjout, mp.date as dateAjout, m.id as memberId, m.firstname, m.lastname, m.email, m.country as memberCountry from product p inner join memberproduct mp on p.id = mp.productId inner join member m on mp.memberId = m.id where p.status = @status";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("status", status);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static List<Product> GetByCountry(string country)
        {
            string query = @"SELECT p.*, mp.status as statusAjout, mp.date as dateAjout, m.id as memberId, m.firstname, m.lastname, m.email, m.country as memberCountry from product p inner join memberproduct mp on p.id = mp.productId inner join member m on mp.memberId = m.id where p.country = @country";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("country", country);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static int Add(Product m)
        {
            int t = 0;
            string query = @"INSERT INTO product (noProd, name, description, categoryId, price, status, nbStock, country) " +
                "VALUES (@noProd, @name, @description, @categoryId, @price, @status, @nbStock, @country);";
            Connection cs = new Connection();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("noProd", m.noProd);
            param.Add("name", m.name);
            param.Add("description", m.description);
            param.Add("categoryId", m.categoryId);
            param.Add("price", m.price);
            param.Add("status", m.status);
            param.Add("nbStock", m.nbStock);
            param.Add("country", m.country);

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
