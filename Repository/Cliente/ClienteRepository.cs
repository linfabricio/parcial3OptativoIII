using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using OptativoIII_Parcial.Modelos;
using OptativoIII_Parcial.Repository.ConfiguracionDb;
using OptativoIII_Parcial.Validaciones;

namespace OptativoIII_Parcial.Repository.Cliente
{
    public class ClienteRepository
    {
        NpgsqlConnection connection;
        public ClienteRepository(string connectionString)
        {
            connection = new ConnectionDB().OpenConnection();
        }

        public bool create(ClienteModel cliente)
        {
            ValidatorCliente.ValidateClienteModel(cliente);

            try
            {

                var cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Cliente(id_banco, Nombre, Apellido, Documento, Direccion, Mail, Celular, Estado) " +
                                  $"VALUES({cliente.IdBanco}," +
                                  $" '{cliente.Nombre}'," +
                                  $"'{cliente.Apellido}', " +
                                  $"'{cliente.Documento}', " +
                                  $"'{cliente.Direccion}', " +
                                  $"'{cliente.Mail}', " +
                                  $"'{cliente.Celular}', " +
                                  $"'{cliente.Estado}')";
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cliente", ex);
            }
        }

        public bool update(ClienteModel cliente,String documento)
        {
            ValidatorCliente.ValidateClienteModel(cliente);

            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE Cliente " +
                                  $"SET Nombre = '{cliente.Nombre}', " +
                                  $"Apellido = '{cliente.Apellido}', " +
                                  $"id_banco = '{cliente.IdBanco}', " +
                                  $"Direccion = '{cliente.Direccion}', " +
                                  $"Mail = '{cliente.Mail}', " +
                                  $"Celular = '{cliente.Celular}', " +
                                  $"Estado = '{cliente.Estado}' " +
                                  $"WHERE Documento = '{documento}'";
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar cliente", ex);
            }
        }

        public bool delete(String documento)
        {
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"DELETE FROM Cliente WHERE Documento = '{documento}'";

                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cliente", ex);
            }
        }

        public void select()
        {
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Cliente";

                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Lista de Clientes:");
                    Console.WriteLine("ID Banco | Nombre | Apellido | Documento | Dirección | Mail | Celular | Estado");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["id_banco"]} | {reader["Nombre"]} | {reader["Apellido"]} | {reader["Documento"]} | {reader["Direccion"]} | {reader["Mail"]} | {reader["Celular"]} | {reader["Estado"]}");
                    }
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
