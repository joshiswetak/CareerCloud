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
    public class CompanyLocationRepository : BaseClass, IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                                                   ([Id]
                                                   ,[Company]
                                                   ,[Country_Code]
                                                   ,[State_Province_Code]
                                                   ,[Street_Address]
                                                   ,[City_Town]
                                                   ,[Zip_Postal_Code])
                                             VALUES
                                                   (@Id
                                                   ,@Company
                                                   ,@Country_Code
                                                   ,@State_Province_Code
                                                   ,@Street_Address
                                                   ,@City_Town
                                                   ,@Zip_Postal_Code)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
                                          ,[Company]
                                          ,[Country_Code]
                                          ,[State_Province_Code]
                                          ,[Street_Address]
                                          ,[City_Town]
                                          ,[Zip_Postal_Code]
                                          ,[Time_Stamp]
                                      FROM [dbo].[Company_Locations]";

                int counter = 0;
                CompanyLocationPoco[] pocos = new CompanyLocationPoco[500];
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.CountryCode = rdr.GetString(2);
                    poco.Province = rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3);
                    poco.Street = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4);
                    poco.City = rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5);
                    poco.PostalCode = rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6);
                    poco.TimeStamp = (byte[])rdr[7];

                    pocos[counter] = poco;
                    counter++;
                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyLocationPoco> GetList(Func<CompanyLocationPoco, bool> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Func<CompanyLocationPoco, bool> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            CompanyLocationPoco[] pocos = GetAll().ToArray();
            return pocos.Where(where).ToList().FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Locations]
                                        WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Locations]
                                           SET [Id] = @Id
                                              ,[Company] = @Company
                                              ,[Country_Code] = @Country_Code
                                              ,[State_Province_Code] = @State_Province_Code
                                              ,[Street_Address] = @Street_Address
                                              ,[City_Town] = @City_Town
                                              ,[Zip_Postal_Code] = @Zip_Postal_Code
                                         WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
