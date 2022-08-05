using System;
using System.Data;
using Npgsql;

namespace ORC.workshop.Services
{
    public class SiteService
    {
        private readonly string _sqlDataScource;

        public SiteService(string sqlDataSource)
        {
            _sqlDataScource = sqlDataSource;
        }
        public DataTable GetSites(int company_id)
        {
            try
            {
                string query = @"select * from site where company_id = @company_id";
                DataTable table = new DataTable();
                NpgsqlDataReader reader;

                using (NpgsqlConnection connection = new NpgsqlConnection(_sqlDataScource))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@company_id", company_id);
                        reader = command.ExecuteReader();
                        table.Load(reader);

                        reader.Close();
                    }
                    connection.Close();
                }
                return table;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
