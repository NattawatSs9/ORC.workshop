using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ORC.workshop.Models;

namespace ORC.workshop.Services
{
    public class OrderService
    {
        private readonly string _sqlDataSource;

        public OrderService(string sqlDataSource)
        {
            _sqlDataSource = sqlDataSource;
        }
        public DataTable GetNumber()
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"SELECT max(booking_id) from booking_order";
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
        public DataTable GetOrder()
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"SELECT b.booking_id , b.booking_code, q.quotation_id, q.quotation_name, b.delivery_datetime, b.delivery_date, c.company_id, c.company_name, s.site_id, s.site_name, pl.plant_id, pl.plant_name, p.product_id, p.product_name, b.quantity, b.contact_name, b.tel, b.casting_method, b.status  FROM booking_order b
                                JOIN company c ON c.company_id = b.company_id
                                JOIN site s ON s.site_id = b.site_id
                                JOIN quotation q ON q.quotation_id = b.quotation_id
                                JOIN product p ON p.product_id = b.product_id
                                JOIN plant pl ON pl.plant_id = b.plant_id";
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
        public string AddOrder(OrderModel orderModel)
        {
            try
            {
                string query =
                    @"INSERT INTO booking_order(delivery_datetime, delivery_date, casting_method, tel, contact_name, quantity, booking_code, more_detail, company_id, plant_id, site_id, quotation_id, product_id, status, qc)
                      VALUES (@delivery_datetime, @delivery_date, @casting_method, @tel, @contact_name, @quantity, @booking_code, @more_detail, @company_id, @plant_id, @site_id, @quotation_id, @product_id, @status, @qc)";
                NpgsqlDataReader reader;
                using (NpgsqlConnection connection = new NpgsqlConnection(_sqlDataSource))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@delivery_datetime", orderModel.DeliveryDateTime);
                        command.Parameters.AddWithValue("@delivery_date", orderModel.DeliveryDate);
                        command.Parameters.AddWithValue("@casting_method", orderModel.CastingMethod);
                        command.Parameters.AddWithValue("@tel", orderModel.Tel);
                        command.Parameters.AddWithValue("@contact_name", orderModel.ContactName);
                        command.Parameters.AddWithValue("@quantity", orderModel.Quantity);
                        command.Parameters.AddWithValue("@booking_code", orderModel.BookingCode);
                        command.Parameters.AddWithValue("@more_detail", orderModel.MoreDetail);
                        command.Parameters.AddWithValue("@company_id", orderModel.CompanyId);
                        command.Parameters.AddWithValue("@plant_id", orderModel.PlantId);
                        command.Parameters.AddWithValue("@site_id", orderModel.SiteId);
                        command.Parameters.AddWithValue("@quotation_id", orderModel.Quotation);
                        command.Parameters.AddWithValue("@product_id", orderModel.ProductId);
                        command.Parameters.AddWithValue("@status", ((Status_type)0).ToString());
                        command.Parameters.AddWithValue("@qc", orderModel.QC);
                        reader = command.ExecuteReader();
                        reader.Close();
                    }
                    connection.Close();
                }
                return "Add Succesfully";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string ChangeToSubmit(int booking_id)
        {
            try
            {
                string query = @"UPDATE booking_order
                                 SET status = @Submit
                                 WHERE booking_id = @booking_id";
                
                NpgsqlDataReader reader;

                using (NpgsqlConnection connection = new NpgsqlConnection(_sqlDataSource))
                {
                    //connection.TypeMapper.MapComposite<SomeType>("some_composite_type");
                    connection.Open();
                    //connection.TypeMapper.MapEnum<Status_type>("status_type");
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Submit", ((Status_type)1).ToString());
                        command.Parameters.AddWithValue("@booking_id", booking_id);
                        reader = command.ExecuteReader();
                        reader.Close();
                    }
                    connection.Close();
                }
                return "Change Status Succesfully";
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public string ChangeToConfirm(int booking_id)
        {
            try
            {
                //string select = @"select status from booking_order where booking_id = @booking_id";
                string query = @"UPDATE booking_order
                                 SET status = @Submit
                                 WHERE booking_id = @booking_id";
                NpgsqlDataReader reader;

                using (NpgsqlConnection connection = new NpgsqlConnection(_sqlDataSource))
                {
                    //connection.TypeMapper.MapComposite<SomeType>("some_composite_type");
                    connection.Open();
                    //connection.TypeMapper.MapEnum<Status_type>("status_type");
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Submit", ((Status_type)4).ToString());
                        command.Parameters.AddWithValue("@booking_id", booking_id);
                        reader = command.ExecuteReader();
                        reader.Close();
                    }
                    connection.Close();
                }
                return "Change Status Succesfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
