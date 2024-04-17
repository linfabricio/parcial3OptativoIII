using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace OptativoIII_Parcial.Repository.ConfiguracionDb
{
    public class ConnectionDB
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=0106;Database=postgres;Port=5432";

        public  NpgsqlConnection OpenConnection()
        {
            try
            {
                var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                return conn;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
