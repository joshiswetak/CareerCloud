using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemCountryCodeRepository : BaseClass, IDataRepository<SystemCountryCodePoco>
    {
        public void Add(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemCountryCodePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[System_Country_Codes]
                                                   ([Code]
                                                   ,[Name])
                                             VALUES
                                                   (@Code
                                                   ,@Name)";
                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Code]
                                          ,[Name]
                                      FROM [dbo].[System_Country_Codes]";

                int counter = 0;
                SystemCountryCodePoco[] pocos = new SystemCountryCodePoco[500];
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    SystemCountryCodePoco poco = new SystemCountryCodePoco();
                    poco.Code = rdr.GetString(0);
                    poco.Name = rdr.GetString(1);
                    
                    pocos[counter] = poco;
                    counter++;
                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemCountryCodePoco> GetList(Func<SystemCountryCodePoco, bool> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Func<SystemCountryCodePoco, bool> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            SystemCountryCodePoco[] pocos = GetAll().ToArray();
            return pocos.Where(where).ToList().FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemCountryCodePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[System_Country_Codes]
                                        WHERE Code = @Code";
                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemCountryCodePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[System_Country_Codes]
                                           SET [Code] = @Code
                                              ,[Name] = @Name
                                                WHERE Code = @Code";

                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
