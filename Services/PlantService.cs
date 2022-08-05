using System;
using System.Data;
using Npgsql;

namespace ORC.workshop.Services
{
    public class PlantService
    {
        private readonly string _sqlDataSource;

        public PlantService(string sqlDataSource)
        {
            _sqlDataSource = sqlDataSource;
        }
        public DataTable GetAllPlant()
        {
            try
            {
                string query = @"select * from plant";
                DataTable table = new DataTable();
                NpgsqlDataReader reader;

                using (NpgsqlConnection connection = new NpgsqlConnection(_sqlDataSource))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        reader = command.ExecuteReader();
                        table.Load(reader);
                        reader.Close();
                    }
                    connection.Close();
                }
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
