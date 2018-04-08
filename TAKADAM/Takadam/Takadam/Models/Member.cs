using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Takadam.Tools;

namespace Takadam.Models
{
    public class Member
    {
        public int id { get; set; }
        public string name { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public DateTime date { get; set; }
        public DateTime last_connection { get; set; }

        public static List<Member> GetAll()
        {
            string query = @"SELECT * from member";
            Connection cs = new Connection();
            Dictionary<string, object> param = new Dictionary<string, object>();
            List<Member> list = new List<Member>();

            MySqlDataReader reader = cs.Select(query, param);
            list = new List<Member>();
            while (reader.Read())
            {
                Member item = new Member();

                if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    item.id = reader.GetInt32(reader.GetOrdinal("id"));

                if (!(reader["firstname"] == DBNull.Value))
                    item.firstname = reader["firstname"].ToString().Trim();

                if (!(reader["lastname"] == DBNull.Value))
                    item.lastname = reader["lastname"].ToString().Trim();

                if (!(reader["password"] == DBNull.Value))
                    item.password = reader["password"].ToString().Trim();

                if (!(reader["phone"] == DBNull.Value))
                    item.phone = reader["phone"].ToString().Trim();

                if (!(reader["email"] == DBNull.Value))
                    item.email = reader["email"].ToString().Trim();

                if (!(reader["address"] == DBNull.Value))
                    item.address = reader["address"].ToString().Trim();

                if (!reader.IsDBNull(reader.GetOrdinal("date")))
                    item.date = reader.GetDateTime(reader.GetOrdinal("date"));

                if (!reader.IsDBNull(reader.GetOrdinal("last_connection")))
                    item.last_connection = reader.GetDateTime(reader.GetOrdinal("last_connection"));

                if (!(reader["country"] == DBNull.Value))
                    item.country = reader["country"].ToString().Trim();

                list.Add(item);
            }

            cs.CloseConnection();
            return list;
        }

        public static Member GetById(int id)
        {
            Member item = null;
            string query = @"SELECT * from member where id = @id";
            Connection cs = new Connection();
            Dictionary<string, object> param = new Dictionary<string, object>();
            List<Member> list = new List<Member>();
            param.Add("id", id);

            MySqlDataReader reader = cs.Select(query, param);
            while (reader.Read())
            {
                item = new Member();

                if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    item.id = reader.GetInt32(reader.GetOrdinal("id"));

                if (!(reader["firstname"] == DBNull.Value))
                    item.firstname = reader["firstname"].ToString().Trim();

                if (!(reader["lastname"] == DBNull.Value))
                    item.lastname = reader["lastname"].ToString().Trim();

                if (!(reader["password"] == DBNull.Value))
                    item.password = reader["password"].ToString().Trim();

                if (!(reader["phone"] == DBNull.Value))
                    item.phone = reader["phone"].ToString().Trim();

                if (!(reader["email"] == DBNull.Value))
                    item.email = reader["email"].ToString().Trim();

                if (!(reader["address"] == DBNull.Value))
                    item.address = reader["address"].ToString().Trim();

                if (!reader.IsDBNull(reader.GetOrdinal("date")))
                    item.date = reader.GetDateTime(reader.GetOrdinal("date"));

                if (!reader.IsDBNull(reader.GetOrdinal("last_connection")))
                    item.last_connection = reader.GetDateTime(reader.GetOrdinal("last_connection"));

                if (!(reader["country"] == DBNull.Value))
                    item.country = reader["country"].ToString().Trim();

                list.Add(item);
            }

            cs.CloseConnection();
            return item;
        }

        public static Member GetByEmailAndPassword(string email, string password)
        {
            Member item = null;
            string query = @"SELECT * from member where email = @email and password = @password";
            Connection cs = new Connection();
            Dictionary<string, object> param = new Dictionary<string, object>();
            List<Member> list = new List<Member>();
            param.Add("email", email);
            param.Add("password", password);

            MySqlDataReader reader = cs.Select(query, param);
            while (reader.Read())
            {
                item = new Member();

                if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    item.id = reader.GetInt32(reader.GetOrdinal("id"));

                if (!(reader["firstname"] == DBNull.Value))
                    item.firstname = reader["firstname"].ToString().Trim();

                if (!(reader["lastname"] == DBNull.Value))
                    item.lastname = reader["lastname"].ToString().Trim();

                if (!(reader["password"] == DBNull.Value))
                    item.password = reader["password"].ToString().Trim();

                if (!(reader["phone"] == DBNull.Value))
                    item.phone = reader["phone"].ToString().Trim();

                if (!(reader["email"] == DBNull.Value))
                    item.email = reader["email"].ToString().Trim();

                if (!(reader["address"] == DBNull.Value))
                    item.address = reader["address"].ToString().Trim();

                if (!reader.IsDBNull(reader.GetOrdinal("date")))
                    item.date = reader.GetDateTime(reader.GetOrdinal("date"));

                if (!reader.IsDBNull(reader.GetOrdinal("last_connection")))
                    item.last_connection = reader.GetDateTime(reader.GetOrdinal("last_connection"));

                if (!(reader["country"] == DBNull.Value))
                    item.country = reader["country"].ToString().Trim();

                list.Add(item);
            }

            cs.CloseConnection();
            return item;
        }

        public static int Add(Member m)
        {
            int t = 0;
            string query = @"INSERT INTO member (lastname, username, password, phone, email, address, date, last_connection, country) " +
                                      "VALUES (@lastname, @username, @password, @phone, @email, @address, NOW(), NOW(), @country);";
            Connection cs = new Connection();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("firstname", m.firstname);
            param.Add("lastname", m.lastname);
            param.Add("password", m.password);
            param.Add("phone", m.phone);
            param.Add("email", m.email);
            param.Add("address", m.address);
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