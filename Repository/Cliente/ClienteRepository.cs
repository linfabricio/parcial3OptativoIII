using System;
using System.Data;
using Dapper;
using Npgsql;
using OptativoIII_Parcial.Modelos;
using OptativoIII_Parcial.Repository.ConfiguracionDb;
using OptativoIII_Parcial.Validaciones;

namespace OptativoIII_Parcial.Repository.Cliente
{
    public class ClienteRepository
    {
        private readonly NpgsqlConnection connection;

        public ClienteRepository(string connectionString)
        {
            connection = new ConnectionDB().OpenConnection();
        }

        public bool Create(ClienteModel cliente)
        {
            ValidatorCliente.ValidateClienteModel(cliente);

            try
            {
                string query = "INSERT INTO Cliente (id_banco, Nombre, Apellido, Documento, Direccion, Mail, Celular, Estado) " +
                               "VALUES (@IdBanco, @Nombre, @Apellido, @Documento, @Direccion, @Mail, @Celular, @Estado)";

                connection.Execute(query, cliente);

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cliente", ex);
            }
        }

        public bool Update(ClienteModel cliente, string documento)
        {
            ValidatorCliente.ValidateClienteModel(cliente);

            try
            {
                string query = "UPDATE Cliente " +
                               "SET Nombre = @Nombre, " +
                               "Apellido = @Apellido, " +
                               "id_banco = @IdBanco, " +
                               "Direccion = @Direccion, " +
                               "Mail = @Mail, " +
                               "Celular = @Celular, " +
                               "Estado = @Estado " +
                               "WHERE Documento = @Documento";

                connection.Execute(query, new
                {
                    cliente.Nombre,
                    cliente.Apellido,
                    cliente.IdBanco,
                    cliente.Direccion,
                    cliente.Mail,
                    cliente.Celular,
                    cliente.Estado,
                    Documento = documento
                });

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar cliente", ex);
            }
        }

        public bool Delete(string documento)
        {
            try
            {
                string query = "DELETE FROM Cliente WHERE Documento = @Documento";

                connection.Execute(query, new { Documento = documento });

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cliente", ex);
            }
        }

        public void Select()
        {
            try
            {
                string query = "SELECT * FROM Cliente";

                var clientes = connection.Query<ClienteModel>(query);

                Console.WriteLine("Lista de Clientes:");
                Console.WriteLine("ID Banco | Nombre | Apellido | Documento | Dirección | Mail | Celular | Estado");
                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"{cliente.IdBanco} | {cliente.Nombre} | {cliente.Apellido} | {cliente.Documento} | {cliente.Direccion} | {cliente.Mail} | {cliente.Celular} | {cliente.Estado}");
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de clientes", ex);
            }
        }
    }
}
