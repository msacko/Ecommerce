using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Takadam.Tools;

namespace Takadam.Models
{
    public class MemberInterest
    {
        public int id { get; set; }
        public int memberId { get; set; }
        public int categoryProductId { get; set; }


        private static List<MemberInterest> get(string query, Dictionary<string, object> parameters)
        {
            MemberInterest item = null;
            Connection cs = new Connection();
            List<MemberInterest> list = new List<MemberInterest>();

            MySqlDataReader reader = cs.Select(query, parameters);

            while (reader.Read())
            {
                item = new MemberInterest();

                if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    item.id = reader.GetInt32(reader.GetOrdinal("id"));

                if (!reader.IsDBNull(reader.GetOrdinal("memberId")))
                    item.memberId = reader.GetInt32(reader.GetOrdinal("memberId"));

                if (!reader.IsDBNull(reader.GetOrdinal("categoryProductId")))
                    item.categoryProductId = reader.GetInt32(reader.GetOrdinal("categoryProductId"));

                list.Add(item);
            }

            cs.CloseConnection();
            return list;
        }

        public static List<MemberInterest> GetAll()
        {
            string query = @"SELECT * from memberInterest";
            Dictionary<string, object> param = new Dictionary<string, object>();

            return get(query, param);
        }

        public static MemberInterest GetById(int id)
        {
            string query = @"SELECT * from memberInterest where id = @id";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", id);

            List<MemberInterest> list = get(query, param);

            //If the list contains an element return the corresponding element
            return list.Any() ? list.First() : null;
        }

        public static List<MemberInterest> GetByMemberId(int memberId)
        {
            string query = @"SELECT * from memberInterest where memberId = @memberId";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("memberId", memberId);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static List<MemberInterest> GetByCategoryProductId(int categoryProductId)
        {
            string query = @"SELECT * from memberInterest where categoryProductId = @categoryProductId";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("categoryProductIdId", categoryProductId);

            //If the list contains an element return the corresponding element
            return get(query, param);
        }

        public static int Add(MemberInterest m)
        {
            int t = 0;
            string query = @"INSERT INTO memberInterest (memberId, categoryProductId) " +
                                      "VALUES (@memberId, @categoryProductId);";
            Connection cs = new Connection();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("memberId", m.memberId);
            param.Add("categoryProductId", m.categoryProductId);

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
