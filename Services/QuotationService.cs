using System;
using System.Data;
using Npgsql;

namespace ORC.workshop.Services
{
    public class QuotationService
    {
        private readonly string _sqlDataScource;

        public QuotationService(string sqlDataSource)
        {
            _sqlDataScource = sqlDataSource;
        }
        public DataTable GetQuotations(int site_id)
        {
            try
            {
                string query = @"select * from quotation where site_id = @site_id";
                DataTable table = new DataTable();
                NpgsqlDataReader reader;

                using (NpgsqlConnection connection = new NpgsqlConnection(_sqlDataScource))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@site_id", site_id);
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
