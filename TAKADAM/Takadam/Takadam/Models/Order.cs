using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Takadam.Tools;

namespace Takadam.Models
{
    public class Order
    {
        public int id { get; set; }
        public int memberId { get; set; }
        public int productId { get; set; }
        public DateTime date { get; set; }
        public int orderStatusId { get; set; }

        private static List<Order> get(string query, Dictionary<string, object> parameters)
        {
            Order item = null;
            Connection cs = new Connection();
            List<Order> list = new List<Order>();

            MySqlDataReader reader = cs.Select(query, parameters);

            while (reader.Read())
            {
                item = new Order();

                if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    item.id = reader.GetInt32(reader.GetOrdinal("id"));

                if (!reader.IsDBNull(reader.GetOrdinal("memberId")))
                    item.memberId = reader.GetInt32(reader.GetOrdinal("memberId"));

                if (!reader.IsDBNull(reader.GetOrdinal("productId")))
                    item.productId = reader.GetInt32(reader.GetOrdinal("producId"));

                if (!reader.IsDBNull(reader.GetOrdinal("date")))
                    item.date = reader.GetDateTime(reader.GetOrdinal("date"));

                if (!reader.IsDBNull(reader.GetOrdinal("orderStatusId")))
                    item.orderStatusId = reader.GetInt32(reader.GetOrdinal("orderStatusId"));

                list.Add(item);
            }

            cs.CloseConnection();
            return list;
        }

        public static List<Order> GetAll()
        {
            string query = @"SELECT * from orders";
            Dictionary<string, object> param = new Dictionary<string, object>();

            return get(query, param);
        }

        public static Order GetById(int id)
        {
            string query = @"SELECT * from orders where id = @id";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", id);

            List<Order> list = get(query, param);

            //If the list contains an element return the corresponding element
            return list.Any() ? list.First() : null;
        }

        public static List<Order> GetByMemberId(int memberId)
        {
            string query = @"SELECT * from orders where memberId = @memberId";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("memberId", memberId);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static List<Order> GetByProductId(int productId)
        {
            string query = @"SELECT * from orders where productId = @productId";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("productId", productId);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static List<Order> GetByOrderStatusId(string orderStatusId)
        {
            string query = @"SELECT * from orders where orderStatusId = @orderStatusId";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("orderStatusId", orderStatusId);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static int Add(Order m)
        {
            int t = 0;
            string query = @"INSERT INTO orders (memberId, productId, date, orderStatusId) " +
                "VALUES (@memberId, @productId, NOW(), @orderStatusId);";
            Connection cs = new Connection();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("memberId", m.memberId);
            param.Add("productId", m.productId);
            param.Add("orderStatusId", m.orderStatusId);

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
