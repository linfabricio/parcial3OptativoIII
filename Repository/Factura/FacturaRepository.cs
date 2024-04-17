using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using OptativoIII_Parcial.Modelos;
using OptativoIII_Parcial.Repository.ConfiguracionDb;
using OptativoIII_Parcial.Validaciones;

namespace OptativoIII_Parcial.Repository.Factura
{
    public class FacturaRepository
    {
        NpgsqlConnection connection;
        public FacturaRepository(string connectionString)
        {
            connection = new ConnectionDB().OpenConnection();
        }

        public bool Create(FacturaModel factura)
        {
            ValidatorFactura.ValidateFacturaModel(factura);

            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Factura (id_cliente, Nro_Factura, Fecha_Hora, Total, Total_iva5, Total_iva10, Total_iva, Total_letras, Sucursal) " +
                                  $"VALUES ({factura.IdCliente}, " +
                                  $"'{factura.NroFactura}'," +
                                  $"'{factura.FechaHora.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                  $" {factura.Total}, " +
                                  $"{factura.TotalIva5}, " +
                                  $"{factura.TotalIva10}, " +
                                  $"{factura.TotalIva}, " +
                                  $"'{factura.TotalLetras}', " +
                                  $"'{factura.Sucursal}')";

                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar factura", ex);
            }
        }

        public bool Update(FacturaModel factura, int Nro_Factura)
        {
            ValidatorFactura.ValidateFacturaModel(factura);

            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE Factura " +
                                  $"SET id_cliente = {factura.IdCliente}, " +
                                  $"Fecha_Hora = '{factura.FechaHora.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                                  $"Total = {factura.Total}, " +
                                  $"Total_iva5 = {factura.TotalIva5}, " +
                                  $"Total_iva10 = {factura.TotalIva10}, " +
                                  $"Total_iva = {factura.TotalIva}, " +
                                  $"Total_letras = '{factura.TotalLetras}', " +
                                  $"Sucursal = '{factura.Sucursal}' " +
                                  $"WHERE Nro_Factura = {Nro_Factura}";

                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar factura", ex);
            }
        }

        public bool Delete(int Nro_Factura)
        {
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"DELETE FROM Factura WHERE Nro_Factura = {Nro_Factura}";

                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar factura", ex);
            }
        }

        public void Select()
        {
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Factura";

                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Lista de Facturas:");
                    Console.WriteLine("ID | ID Cliente | Nro Factura | Fecha Hora | Total | Total IVA 5 | Total IVA 10 | Total IVA | Total Letras | Sucursal");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["id"]} | {reader["id_cliente"]} | {reader["Nro_Factura"]} | {reader["Fecha_Hora"]} | {reader["Total"]} | {reader["Total_iva5"]} | {reader["Total_iva10"]} | {reader["Total_iva"]} | {reader["Total_letras"]} | {reader["Sucursal"]}");
                    }
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de facturas", ex);
            }
        }
    }
}
