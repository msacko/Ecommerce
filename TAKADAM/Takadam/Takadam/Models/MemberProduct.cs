using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Takadam.Tools;

namespace Takadam.Models
{
    public class MemberProduct
    {
        public int id { get; set; }
        public int memberId { get; set; }
        public int productId { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }

        private static List<MemberProduct> get(string query, Dictionary<string, object> parameters)
        {
            MemberProduct item = null;
            Connection cs = new Connection();
            List<MemberProduct> list = new List<MemberProduct>();

            MySqlDataReader reader = cs.Select(query, parameters);

            while (reader.Read())
            {
                item = new MemberProduct();

                if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    item.id = reader.GetInt32(reader.GetOrdinal("id"));

                if (!reader.IsDBNull(reader.GetOrdinal("memberId")))
                    item.memberId = reader.GetInt32(reader.GetOrdinal("memberId"));

                if (!reader.IsDBNull(reader.GetOrdinal("productId")))
                    item.productId = reader.GetInt32(reader.GetOrdinal("producId"));
                
                if (!reader.IsDBNull(reader.GetOrdinal("date")))
                    item.date = reader.GetDateTime(reader.GetOrdinal("date"));

                if (!(reader["status"] == DBNull.Value))
                    item.status = reader["status"].ToString().Trim();

                list.Add(item);
            }

            cs.CloseConnection();
            return list;
        }

        public static List<MemberProduct> GetAll()
        {
            string query = @"SELECT * from memberProduct";
            Dictionary<string, object> param = new Dictionary<string, object>();

            return get(query, param);
        }

        public static MemberProduct GetById(int id)
        {
            string query = @"SELECT * from memberProduct where id = @id";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", id);

            List<MemberProduct> list = get(query, param);

            //If the list contains an element return the corresponding element
            return list.Any() ? list.First() : null;
        }

        public static List<MemberProduct> GetByMemberId(int memberId)
        {
            string query = @"SELECT * from memberProduct where memberId = @memberId";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("memberId", memberId);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static List<MemberProduct> GetByProductId(int productId)
        {
            string query = @"SELECT * from memberProduct where productId = @productId";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("productId", productId);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static List<MemberProduct> GetByStatus(string status)
        {
            string query = @"SELECT * from memberProduct where status = @status";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("status", status);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static int Add(MemberProduct m)
        {
            int t = 0;
            string query = @"INSERT INTO memberProduct (memberId, productId, date, status) " +
                                        "VALUES (@memberId, @productId, NOW(), @status);";
            Connection cs = new Connection();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("memberId", m.memberId);
            param.Add("productId", m.productId);
            param.Add("status", m.status);

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
