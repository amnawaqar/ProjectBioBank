using BioBank.Shared.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BioBank.Shared.Database
{
    public class DbService : IDbService
    {
        private readonly string _connectionString;
        public DbService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        bool IDbService.AddNewCollection(Models.Tissue bioBankService)
        {
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO BiobankTable (DieaseTerm, Title) VALUES (@DiseaseTerm, @Title)", connection))
                    {
                        command.Parameters.AddWithValue("@DiseaseTerm", bioBankService.DieaseTerm);
                        command.Parameters.AddWithValue("@Title", bioBankService.Title);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
        }
        bool IDbService.AddNewSample(Samples samplesService)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string insertQuery = "INSERT INTO Samples (BioBankId,Donor_Count,Material_Type,Last_updated) " +
                                     "VALUES (@CollectionId,@DonorCount,@MaterialType,GETDATE())";
                connection.Open();
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@CollectionId", samplesService.BioBankId);
                    command.Parameters.AddWithValue("@DonorCount", samplesService.Donor_Count);
                    command.Parameters.AddWithValue("@MaterialType", samplesService.Material_Type);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        List<Models.Tissue> IDbService.GetBioBankCollections()
        {
            List<Models.Tissue> BioBankCollections = new List<Models.Tissue>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT id,DieaseTerm,title FROM BioBankTable", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Tissue collection = new Models.Tissue
                            {
                                Id = (int)reader["id"],
                                DieaseTerm = reader["DieaseTerm"].ToString(),
                                Title = reader["Title"].ToString()
                            };
                            BioBankCollections.Add(collection);
                        }
                    }
                }
                return BioBankCollections;
            }
        }
        List<Samples> IDbService.GetSamples(int id)
        {
            {
                List<Samples> SamplesCollections = new List<Samples>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Samples where biobankid=" + id, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Samples collection = new Samples
                                {
                                    Sample_Id = (int)reader["Sample_id"],
                                    Donor_Count = (int)reader["Donor_Count"],
                                    Material_Type = reader["Material_Type"].ToString(),
                                    Last_updated = (DateTime)reader["Last_updated"],
                                    BioBankId= (int)reader["biobankid"]
                                };
                                SamplesCollections.Add(collection);
                            }
                        }
                    }
                }
                return SamplesCollections;
            }
        }
    }
}
