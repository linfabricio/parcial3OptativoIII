using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public bool Create(FacturaModel factura, DetalleFacturaModel detalleFactura)
        {
            ValidatorFactura.ValidateFacturaModel(factura);

            try
            {

                string queryFactura = "INSERT INTO Factura (id_cliente, id_sucursal, Nro_Factura, Fecha_Hora, Total, Total_iva5, Total_iva10, Total_iva, Total_letras) " +
                               "VALUES (@IdCliente, @IdSucursal, @NroFactura, @FechaHora, @Total, @TotalIva5, @TotalIva10, @TotalIva, @TotalLetras)";

                connection.Execute(queryFactura, factura);


                string queryDetalleFactura = @"INSERT INTO Detalle_factura (id_factura, id_producto, cantidad_producto, subtotal)
                    VALUES (@IdFactura, @IdProducto, @CantidadProducto, @Subtotal)";

                connection.Execute(queryDetalleFactura, detalleFactura);

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

        public bool Update(FacturaModel factura, int Nro_Factura, DetalleFacturaModel detalleFactura)
        {
            ValidatorFactura.ValidateFacturaModel(factura);

            try
            {
                connection.Open();

                string queryFactura = @"
            UPDATE Factura 
            SET id_cliente = @IdCliente, 
                id_sucursal = @IdSucursal, 
                Fecha_Hora = @FechaHora, 
                Total = @Total, 
                Total_iva5 = @TotalIva5, 
                Total_iva10 = @TotalIva10, 
                Total_iva = @TotalIva, 
                Total_letras = @TotalLetras, 
                Sucursal = @Sucursal 
            WHERE Nro_Factura = @NroFactura";

                connection.Execute(queryFactura, new
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

                string queryDetalleFactura = @"
            UPDATE DetalleFactura 
            SET Id_Producto = @IdProducto, 
                Cantidad_Producto = @CantidadProducto, 
                Subtotal = @Subtotal 
            WHERE Id_Factura = @IdFactura";

                connection.Execute(queryDetalleFactura, new
                {
                    detalleFactura.IdProducto,
                    detalleFactura.CantidadProducto,
                    detalleFactura.Subtotal,
                    IdFactura = factura.Id
                });


                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar factura con detalle", ex);
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

                string queryDetalleFactura = "DELETE FROM DetalleFactura WHERE Id_Factura = @NroFactura";
                connection.Execute(queryDetalleFactura, new { Nro_Factura });

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
                // Obtener todas las facturas
                string queryFacturas = @"
            SELECT 
                f.Id,
                f.Id_Cliente AS IdCliente,
                f.Id_Sucursal AS IdSucursal,
                f.Nro_Factura AS NroFactura,
                f.Fecha_Hora AS FechaHora,
                f.Total,
                f.Total_iva5 AS TotalIva5,
                f.Total_iva10 AS TotalIva10,
                f.Total_iva AS TotalIva,
                f.Total_letras AS TotalLetras
            FROM Factura f;";

                var facturas = connection.Query<FacturaModel>(queryFacturas).ToList();

                string queryDetalles = @"
            SELECT 
                df.Id AS IdDetalle,
                df.Id_Factura AS IdFactura,
                df.Id_Producto AS IdProducto,
                df.Cantidad_Producto AS CantidadProducto,
                df.Subtotal
            FROM Detalle_factura df;";

                var detalles = connection.Query<DetalleFacturaModel>(queryDetalles).ToList();

                foreach (var factura in facturas)
                {
                    detalles.Where(d => d.IdFactura == factura.Id).ToList();
                }

                Console.WriteLine("Lista de Facturas:");
                foreach (var factura in facturas)
                {
                    Console.WriteLine($"Factura {factura.NroFactura}, Fecha: {factura.FechaHora}, Total: {factura.Total}");

                    foreach (var detalle in detalles)
                    {
                        string queryProducto = @"
                        SELECT 
                            descripcion
                        FROM Productos 
                        WHERE id = @IdProducto;";

                        string descripcionProducto = connection.QueryFirstOrDefault<string>(queryProducto, new { IdProducto = detalle.IdProducto });

                        Console.WriteLine($"- Producto: {descripcionProducto}, Cantidad: {detalle.CantidadProducto}, Subtotal: {detalle.Subtotal}");
                    }

                    Console.WriteLine("---------------------------------------------");
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
