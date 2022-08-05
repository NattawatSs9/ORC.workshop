using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ORC.workshop.Services
{
    public class CompanyService
    {
        private readonly string _sqlDataSource;

        public CompanyService(string sqlDataSource)
        {
            _sqlDataSource = sqlDataSource;
        }
        public DataTable GetAllCompany()
        {
            try
            {
                string query = @"select * from company";
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
