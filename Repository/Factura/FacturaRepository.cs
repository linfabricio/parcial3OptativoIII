using System;
using System.Data;
using Dapper;
using Npgsql;
using OptativoIII_Parcial.Modelos;
using OptativoIII_Parcial.Repository.ConfiguracionDb;
using OptativoIII_Parcial.Validaciones;

namespace OptativoIII_Parcial.Repository.Factura
{
    public class FacturaRepository
    {
        private readonly NpgsqlConnection connection;

        public FacturaRepository(string connectionString)
        {
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        public bool Create(FacturaModel factura)
        {
            ValidatorFactura.ValidateFacturaModel(factura);

            try
            {
                string query = "INSERT INTO Factura (id_cliente, id_sucursal, Nro_Factura, Fecha_Hora, Total, Total_iva5, Total_iva10, Total_iva, Total_letras, Sucursal) " +
                               "VALUES (@IdCliente, @IdSucursal, @NroFactura, @FechaHora, @Total, @TotalIva5, @TotalIva10, @TotalIva, @TotalLetras, @Sucursal)";

                connection.Execute(query, factura);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar factura", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool Update(FacturaModel factura, int Nro_Factura)
        {
            ValidatorFactura.ValidateFacturaModel(factura);

            try
            {
                string query = "UPDATE Factura " +
                               "SET id_cliente = @IdCliente, " +
                               "id_sucursal = @IdSucursal, " +
                               "Fecha_Hora = @FechaHora, " +
                               "Total = @Total, " +
                               "Total_iva5 = @TotalIva5, " +
                               "Total_iva10 = @TotalIva10, " +
                               "Total_iva = @TotalIva, " +
                               "Total_letras = @TotalLetras, " +
                               "Sucursal = @Sucursal " +
                               "WHERE Nro_Factura = @NroFactura";

                connection.Execute(query, new
                {
                    factura.IdCliente,
                    factura.IdSucursal,
                    factura.FechaHora,
                    factura.Total,
                    factura.TotalIva5,
                    factura.TotalIva10,
                    factura.TotalIva,
                    factura.TotalLetras,
                    factura.Sucursal,
                    Nro_Factura
                });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar factura", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool Delete(int Nro_Factura)
        {
            try
            {
                string query = "DELETE FROM Factura WHERE Nro_Factura = @NroFactura";

                connection.Execute(query, new { Nro_Factura });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar factura", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void Select()
        {
            try
            {
                string query = "SELECT * FROM Factura";

                var facturas = connection.Query<FacturaModel>(query);

                Console.WriteLine("Lista de Facturas:");
                Console.WriteLine("ID | ID Cliente | ID Sucursal | Nro Factura | Fecha Hora | Total | Total IVA 5 | Total IVA 10 | Total IVA | Total Letras | Sucursal");
                foreach (var factura in facturas)
                {
                    Console.WriteLine($"{factura.Id} | {factura.IdCliente} | {factura.IdSucursal} | {factura.NroFactura} | {factura.FechaHora} | {factura.Total} | {factura.TotalIva5} | {factura.TotalIva10} | {factura.TotalIva} | {factura.TotalLetras} | {factura.Sucursal}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de facturas", ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
