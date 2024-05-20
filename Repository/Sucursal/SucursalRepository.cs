using System;
using System.Data;
using Dapper;
using Npgsql;
using OptativoIII_Parcial.Modelos;
using OptativoIII_Parcial.Repository.ConfiguracionDb;
using OptativoIII_Parcial.Validaciones;

namespace OptativoIII_Parcial.Repository.Sucursal
{
    public class SucursalRepository
    {
        private readonly NpgsqlConnection connection;

        public SucursalRepository(string connectionString)
        {
            connection = new ConnectionDB().OpenConnection();
        }

        public bool Create(SucursalModel sucursal)
        {
            ValidatorSucursal.ValidateSucursalModel(sucursal);

            try
            {
                string query = "INSERT INTO Sucursal (Descripcion, Direccion, Telefono, Whatsapp, Mail, Estado) " +
                               "VALUES (@Descripcion, @Direccion, @Telefono, @Whatsapp, @Mail, @Estado)";

                connection.Execute(query, new
                {
                    sucursal.Descripcion,
                    sucursal.Direccion,
                    sucursal.Telefono,
                    sucursal.Whatsapp,
                    sucursal.Mail,
                    sucursal.Estado
                });

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar sucursal", ex);
            }
        }

        public bool Update(SucursalModel sucursal, int Id)
        {
            ValidatorSucursal.ValidateSucursalModel(sucursal);

            try
            {
                string query = "UPDATE Sucursal " +
                               "SET Descripcion = @Descripcion, " +
                               "Direccion = @Direccion, " +
                               "Telefono = @Telefono, " +
                               "Whatsapp = @Whatsapp, " +
                               "Mail = @Mail, " +
                               "Estado = @Estado " +
                               "WHERE Id = @Id";

                connection.Execute(query, new
                {
                    sucursal.Descripcion,
                    sucursal.Direccion,
                    sucursal.Telefono,
                    sucursal.Whatsapp,
                    sucursal.Mail,
                    sucursal.Estado,
                    Id
                });

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar sucursal", ex);
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                string query = "DELETE FROM Sucursal WHERE Id = @Id";

                connection.Execute(query, new { Id });

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar sucursal", ex);
            }
        }

        public void Select()
        {
            try
            {
                string query = "SELECT * FROM Sucursal";

                var sucursales = connection.Query<SucursalModel>(query);

                Console.WriteLine("Lista de Sucursales:");
                Console.WriteLine("ID | Descripcion | Direccion | Telefono | Whatsapp | Mail | Estado");
                foreach (var sucursal in sucursales)
                {
                    Console.WriteLine($"{sucursal.Id} | {sucursal.Descripcion} | {sucursal.Direccion} | {sucursal.Telefono} | {sucursal.Whatsapp} | {sucursal.Mail} | {sucursal.Estado}");
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de sucursales", ex);
            }
        }
    }
}
