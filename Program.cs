using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using OptativoIII_Parcial.Modelos;
using OptativoIII_Parcial.Repository.Cliente;
using OptativoIII_Parcial.Repository.Factura;
using OptativoIII_Parcial.Repository.ConfiguracionDb;

//CREATE TABLE Cliente (
//    id SERIAL PRIMARY KEY,
//    id_banco INTEGER,
//    Nombre VARCHAR(100) NOT NULL,
//    Apellido VARCHAR(100) NOT NULL,
//    Documento VARCHAR(20) UNIQUE NOT NULL,
//    Direccion VARCHAR(100),
//    Mail VARCHAR(100),
//    Celular VARCHAR(20),
//    Estado VARCHAR(50)
//);

//CREATE TABLE Factura (
//    id SERIAL PRIMARY KEY,
//    id_cliente INTEGER REFERENCES Cliente(id),
//    Nro_Factura VARCHAR(50) UNIQUE NOT NULL,
//    Fecha_Hora TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
//    Total DECIMAL(10, 2),
//    Total_iva5 DECIMAL(10, 2),
//    Total_iva10 DECIMAL(10, 2),
//    Total_iva DECIMAL(10, 2),
//    Total_letras VARCHAR(200),
//    Sucursal VARCHAR(100)
//);

namespace OptativoIII_Parcial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=0106;Database=postgres;Port=5432";

            Console.WriteLine("Bienvenido al Sistema");
            Console.WriteLine("1. Menu Cliente");
            Console.WriteLine("2. Menu Factura");
            Console.WriteLine("3. Salir");

            string opcionMenu = Console.ReadLine();

            if (opcionMenu == "1")
            {
                Console.WriteLine("Menu Cliente Selecione una opcion");
                Console.WriteLine("1. Agregar Cliente");
                Console.WriteLine("2. Listar Clientes");
                Console.WriteLine("3. Actualizar Cliente");
                Console.WriteLine("4. Eliminar Cliente");
                Console.WriteLine("5. Salir");

                bool salirMenuCliente = false;
                while (!salirMenuCliente)
                {
                    Console.Write("Ingrese la opción deseada: ");
                    string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            ClienteRepository clienteServicesCreate = new ClienteRepository(connectionString);
                            Console.WriteLine("Creado nuevo cliente...");
                            Console.Write("Ingrese el nombre del cliente: ");
                            string nombreCreate = Console.ReadLine();
                            Console.Write("Ingrese el apellido del cliente: ");
                            string apellidoCreate = Console.ReadLine();
                            Console.Write("Ingrese el bando del cliente: ");
                            int bancoCreate = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el documento del cliente: ");
                            string documentoCreate = Console.ReadLine();
                            Console.Write("Ingrese la dirección del cliente: ");
                            string direccionCreate = Console.ReadLine();
                            Console.Write("Ingrese el correo electrónico del cliente: ");
                            string mailCreate = Console.ReadLine();
                            Console.Write("Ingrese el número de celular del cliente: ");
                            string celularCreate = Console.ReadLine();
                            Console.Write("Ingrese el estado del cliente: ");
                            string estadoCreate = Console.ReadLine();

                            var nuevoCliente = new ClienteModel();
                            nuevoCliente.Nombre = nombreCreate;
                            nuevoCliente.Apellido = apellidoCreate;
                            nuevoCliente.Documento = documentoCreate;
                            nuevoCliente.Direccion = direccionCreate;
                            nuevoCliente.Mail = mailCreate;
                            nuevoCliente.Celular = celularCreate;
                            nuevoCliente.Estado = estadoCreate;
                            nuevoCliente.IdBanco = bancoCreate;

                            clienteServicesCreate.create(nuevoCliente);
                            Console.WriteLine("Cliente agregado correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "2":
                            ClienteRepository clienteServicesSelect = new ClienteRepository(connectionString);
                            Console.WriteLine("Listado de clientes:");
                            clienteServicesSelect.select();
                            Console.WriteLine("Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "3":
                            ClienteRepository clienteServicesUpdate = new ClienteRepository(connectionString);
                            var cliente = new ClienteModel();
                            Console.WriteLine("Actualizando cliente...");
                            Console.Write("Ingrese el documento del cliente a actualizar: ");
                            string documentoActualizar = Console.ReadLine();
                            Console.Write("Ingrese el bando del cliente: ");
                            int bancoActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el nombre del cliente: ");
                            string nombreActualizar = Console.ReadLine();
                            Console.Write("Ingrese el apellido del cliente: ");
                            string apellidoActualizar = Console.ReadLine();
                            Console.Write("Ingrese la dirección del cliente: ");
                            string direccionActualizar = Console.ReadLine();
                            Console.Write("Ingrese el correo electrónico del cliente: ");
                            string mailActualizar = Console.ReadLine();
                            Console.Write("Ingrese el número de celular del cliente: ");
                            string celularActualizar = Console.ReadLine();
                            Console.Write("Ingrese el estado del cliente: ");
                            string estadoActualizar = Console.ReadLine();

                            cliente.Nombre = nombreActualizar;
                            cliente.Apellido = apellidoActualizar;
                            cliente.Documento = documentoActualizar;
                            cliente.Direccion = direccionActualizar;
                            cliente.Mail = mailActualizar;
                            cliente.Celular = celularActualizar;
                            cliente.Estado = estadoActualizar;
                            cliente.IdBanco = bancoActualizar;

                            clienteServicesUpdate.update(cliente, documentoActualizar);
                            Console.WriteLine("Cliente actualizado correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "4":
                            ClienteRepository clienteServicesDelete = new ClienteRepository(connectionString);
                            Console.WriteLine("Eliminar cliente con el nro de documento:");
                            string documentoDelete = Console.ReadLine();
                            clienteServicesDelete.delete(documentoDelete);
                            Console.WriteLine("Cliente eliminado correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "5":
                            salirMenuCliente = true;
                            break;

                        default:
                            Console.WriteLine("Opción no válida. Por favor, ingrese una opción válida.");
                            Console.WriteLine("Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            else if (opcionMenu == "2")
            {
                Console.WriteLine("Menu Factura Selecione una opcion");
                Console.WriteLine("1. Agregar Factura");
                Console.WriteLine("2. Listar Facturas");
                Console.WriteLine("3. Actualizar Factura");
                Console.WriteLine("4. Eliminar Factura");
                Console.WriteLine("5. Salir");

                bool salirMenuFactura = false;
                while (!salirMenuFactura)
                {
                    Console.Write("Ingrese la opción deseada: ");
                    string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            FacturaRepository facturaServicesCreate = new FacturaRepository(connectionString);
                            Console.WriteLine("Creando nueva factura...");
                            Console.Write("Ingrese el ID del cliente: ");
                            int idClienteCreate = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el número de factura: ");
                            string nroFacturaCreate = Console.ReadLine();
                            Console.Write("Ingrese la fecha y hora de la factura (yyyy-MM-dd HH:mm:ss): ");
                            DateTime fechaHoraCreate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            Console.Write("Ingrese el total: ");
                            decimal totalCreate = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese el total IVA 5%: ");
                            decimal totalIva5Create = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese el total IVA 10%: ");
                            decimal totalIva10Create = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese el total IVA: ");
                            decimal totalIvaCreate = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese el total en letras: ");
                            string totalLetrasCreate = Console.ReadLine();
                            Console.Write("Ingrese la sucursal: ");
                            string sucursalCreate = Console.ReadLine();

                            var nuevaFactura = new FacturaModel
                            {
                                IdCliente = idClienteCreate,
                                NroFactura = nroFacturaCreate,
                                FechaHora = fechaHoraCreate,
                                Total = totalCreate,
                                TotalIva5 = totalIva5Create,
                                TotalIva10 = totalIva10Create,
                                TotalIva = totalIvaCreate,
                                TotalLetras = totalLetrasCreate,
                                Sucursal = sucursalCreate
                            };

                            facturaServicesCreate.Create(nuevaFactura);
                            Console.WriteLine("Factura agregada correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "2":
                            FacturaRepository facturaServicesSelect = new FacturaRepository(connectionString);
                            Console.WriteLine("Listado de facturas:");
                            facturaServicesSelect.Select();
                            Console.WriteLine("Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "3":
                            FacturaRepository facturaServicesUpdate = new FacturaRepository(connectionString);
                            var facturaActualizar = new FacturaModel();
                            Console.WriteLine("Actualizando factura...");
                            Console.Write("Ingrese el ID de la factura a actualizar: ");
                            int idFacturaActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el ID del cliente: ");
                            int idClienteActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el número de factura: ");
                            string nroFacturaActualizar = Console.ReadLine();
                            Console.Write("Ingrese la fecha y hora de la factura (yyyy-MM-dd HH:mm:ss): ");
                            DateTime fechaHoraActualizar = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            Console.Write("Ingrese el total: ");
                            decimal totalActualizar = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese el total IVA 5%: ");
                            decimal totalIva5Actualizar = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese el total IVA 10%: ");
                            decimal totalIva10Actualizar = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese el total IVA: ");
                            decimal totalIvaActualizar = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese el total en letras: ");
                            string totalLetrasActualizar = Console.ReadLine();
                            Console.Write("Ingrese la sucursal: ");
                            string sucursalActualizar = Console.ReadLine();

                            facturaActualizar.Id = idFacturaActualizar;
                            facturaActualizar.IdCliente = idClienteActualizar;
                            facturaActualizar.NroFactura = nroFacturaActualizar;
                            facturaActualizar.FechaHora = fechaHoraActualizar;
                            facturaActualizar.Total = totalActualizar;
                            facturaActualizar.TotalIva5 = totalIva5Actualizar;
                            facturaActualizar.TotalIva10 = totalIva10Actualizar;
                            facturaActualizar.TotalIva = totalIvaActualizar;
                            facturaActualizar.TotalLetras = totalLetrasActualizar;
                            facturaActualizar.Sucursal = sucursalActualizar;

                            facturaServicesUpdate.Update(facturaActualizar, idFacturaActualizar);
                            Console.WriteLine("Factura actualizada correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "4":
                            FacturaRepository facturaServicesDelete = new FacturaRepository(connectionString);
                            Console.WriteLine("Eliminando factura...");
                            Console.Write("Ingrese el ID de la factura a eliminar: ");
                            int idFacturaEliminar = int.Parse(Console.ReadLine());
                            facturaServicesDelete.Delete(idFacturaEliminar);
                            Console.WriteLine("Factura eliminada correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "5":
                            salirMenuFactura = true;
                            break;

                        default:
                            Console.WriteLine("Opción no válida. Por favor, ingrese una opción válida. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;
                    }

                }
            }

        }
    }
}
