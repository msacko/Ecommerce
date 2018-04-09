using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Takadam.Tools;

namespace Takadam.Models
{
    public class ProductComments
    {
        public int id { get; set; }
        public int productId { get; set; }
        public int memberId { get; set; }
        public string comment { get; set; }
        public DateTime date { get; set; }

        private static List<ProductComments> get(string query, Dictionary<string, object> parameters)
        {
            ProductComments item = null;
            Connection cs = new Connection();
            List<ProductComments> list = new List<ProductComments>();

            MySqlDataReader reader = cs.Select(query, parameters);

            while (reader.Read())
            {
                item = new ProductComments();

                if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    item.id = reader.GetInt32(reader.GetOrdinal("id"));

                if (!reader.IsDBNull(reader.GetOrdinal("memberId")))
                    item.memberId = reader.GetInt32(reader.GetOrdinal("memberId"));

                if (!reader.IsDBNull(reader.GetOrdinal("productId")))
                    item.productId = reader.GetInt32(reader.GetOrdinal("producId"));

                if (!(reader["comment"] == DBNull.Value))
                    item.comment = reader["comment"].ToString().Trim();

                if (!reader.IsDBNull(reader.GetOrdinal("date")))
                    item.date = reader.GetDateTime(reader.GetOrdinal("date"));

                list.Add(item);
            }

            cs.CloseConnection();
            return list;
        }

        public static List<ProductComments> GetAll()
        {
            string query = @"SELECT * from productComments";
            Dictionary<string, object> param = new Dictionary<string, object>();

            return get(query, param);
        }

        public static ProductComments GetById(int id)
        {
            string query = @"SELECT * from productComments where id = @id";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", id);

            List<ProductComments> list = get(query, param);

            //If the list contains an element return the corresponding element
            return list.Any() ? list.First() : null;
        }

        public static List<ProductComments> GetByProductId(int productId)
        {
            string query = @"SELECT * from productComments where productId = @productId";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("productId", productId);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static List<ProductComments> GetByMemberId(int memberId)
        {
            string query = @"SELECT * from productComments where memberId = @memberId";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("memberId", memberId);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static int Add(ProductComments m)
        {
            int t = 0;
            string query = @"INSERT INTO productComments (productId, memberId, comment, date) " +
                "VALUES (@productId, @memberId, @comment, NOW());";
            Connection cs = new Connection();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("memberId", m.memberId);
            param.Add("productId", m.productId);
            param.Add("comment", m.comment);

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
