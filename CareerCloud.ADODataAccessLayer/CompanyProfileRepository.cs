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
    public class CompanyProfileRepository : BaseClass, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                                   ([Id]
                                                   ,[Registration_Date]
                                                   ,[Company_Website]
                                                   ,[Contact_Phone]
                                                   ,[Contact_Name]
                                                   ,[Company_Logo])
                                             VALUES
                                                   (@Id
                                                   ,@Registration_Date
                                                   ,@Company_Website
                                                   ,@Contact_Phone
                                                   ,@Contact_Name
                                                   ,@Company_Logo)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
                    
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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
                                          ,[Registration_Date]
                                          ,[Company_Website]
                                          ,[Contact_Phone]
                                          ,[Contact_Name]
                                          ,[Company_Logo]
                                          ,[Time_Stamp]
                                      FROM [dbo].[Company_Profiles]";

                int counter = 0;
                CompanyProfilePoco[] pocos = new CompanyProfilePoco[500];
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.RegistrationDate = rdr.GetDateTime(1);
                    poco.CompanyWebsite = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2);
                    poco.ContactPhone = rdr.GetString(3);
                    poco.ContactName = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4);
                    poco.CompanyLogo = rdr.IsDBNull(5) ? (byte[])null : (byte[])rdr[5];
                    poco.TimeStamp = (byte[])rdr[6];

                    pocos[counter] = poco;
                    counter++;
                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyProfilePoco> GetList(Func<CompanyProfilePoco, bool> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Func<CompanyProfilePoco, bool> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            CompanyProfilePoco[] pocos = GetAll().ToArray();
            return pocos.Where(where).ToList().FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Profiles]
                                        WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                           SET [Id] = @Id
                                              ,[Registration_Date] = @Registration_Date
                                              ,[Company_Website] = @Company_Website
                                              ,[Contact_Phone] = @Contact_Phone
                                              ,[Contact_Name] = @Contact_Name
                                              ,[Company_Logo] = @Company_Logo
                                         WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
