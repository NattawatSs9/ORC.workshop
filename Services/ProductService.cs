using System;
using System.Data;
using Npgsql;

namespace ORC.workshop.Services
{
    public class ProductService
    {
        private readonly string _sqlDataScource;

        public ProductService(string sqlDataSource)
        {
            _sqlDataScource = sqlDataSource;
        }
        public DataTable GetProducts(int quotation_id)
        {
            try
            {
                string query = @"select * from product where quotation_id = @quotation_id";
                DataTable table = new DataTable();
                NpgsqlDataReader reader;

                using (NpgsqlConnection connection = new NpgsqlConnection(_sqlDataScource))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@quotation_id", quotation_id);
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
