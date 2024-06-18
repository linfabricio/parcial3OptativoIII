using System;
using System.Collections.Generic;
using Dapper;
using Npgsql;
using OptativoIII_Parcial.Modelos;
using OptativoIII_Parcial.Validaciones;

namespace OptativoIII_Parcial.Repository.Producto
{
    public class ProductosRepository
    {
        private readonly NpgsqlConnection connection;

        public ProductosRepository(string connectionString)
        {
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        public bool Create(ProductosModel producto)
        {
            ValidatorProducto.ValidateProductosModel(producto);

            try
            {
                string query = @"
                    INSERT INTO Productos (Descripcion, Cantidad_Minima, Cantidad_Stock, Precio_Compra, Precio_Venta, Categoria, Marca, Estado)
                    VALUES (@Descripcion, @CantidadMinima, @CantidadStock, @PrecioCompra, @PrecioVenta, @Categoria, @Marca, @Estado)";

                connection.Execute(query, producto);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar producto", ex);
            }
        }

        public bool Update(ProductosModel producto)
        {
            ValidatorProducto.ValidateProductosModel(producto);

            try
            {
                string query = @"
                    UPDATE Productos
                    SET Descripcion = @Descripcion,
                        Cantidad_Minima = @CantidadMinima,
                        Cantidad_Stock = @CantidadStock,
                        Precio_Compra = @PrecioCompra,
                        Precio_Venta = @PrecioVenta,
                        Categoria = @Categoria,
                        Marca = @Marca,
                        Estado = @Estado
                    WHERE Id = @Id";

                connection.Execute(query, producto);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar producto", ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                string query = "DELETE FROM Productos WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar producto", ex);
            }
        }

        public void Select()
        {
            try
            {
                string query = "SELECT * FROM Productos";

                var productos = connection.Query<ProductosModel>(query);

                Console.WriteLine("Lista de Productos:");
                Console.WriteLine("ID | Descripción | Cant. Mínima | Cant. Stock | Precio Compra | Precio Venta | Categoría | Marca | Estado");

                foreach (var producto in productos)
                {
                    Console.WriteLine($"{producto.Id} | {producto.Descripcion} | {producto.CantidadMinima} | {producto.CantidadStock} | {producto.PrecioCompra} | {producto.PrecioVenta} | {producto.Categoria} | {producto.Marca} | {producto.Estado}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de productos", ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
