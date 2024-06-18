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
using OptativoIII_Parcial.Repository.Sucursal;
using OptativoIII_Parcial.Repository.ConfiguracionDb;
using OptativoIII_Parcial.Repository.Producto;

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
//
//CREATE TABLE Sucursal (
//Id SERIAL PRIMARY KEY,
//Descripcion VARCHAR(255) NOT NULL,
//Direccion VARCHAR(255),
//    Telefono VARCHAR(20),
//    Whatsapp VARCHAR(20),
//    Mail VARCHAR(100),
//    Estado VARCHAR(50)
//);

//CREATE TABLE Factura (
//    id SERIAL PRIMARY KEY,
//    id_cliente INTEGER REFERENCES Cliente(id),
//    id_sucursal INTEGER REFERENCES Sucursal(id),
//    Nro_Factura VARCHAR(50) UNIQUE NOT NULL,
//    Fecha_Hora TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
//    Total DECIMAL(10, 2),
//    Total_iva5 DECIMAL(10, 2),
//    Total_iva10 DECIMAL(10, 2),
//    Total_iva DECIMAL(10, 2),
//    Total_letras VARCHAR(200)
//);

//CREATE TABLE Productos (
//    id SERIAL PRIMARY KEY,
//    descripcion VARCHAR(255) NOT NULL,
//    cantidad_minima INTEGER NOT NULL,
//    cantidad_stock INTEGER NOT NULL,
//    precio_compra DECIMAL(10, 2) NOT NULL,
//    precio_venta DECIMAL(10, 2) NOT NULL,
//    categoria VARCHAR(100),
//    marca VARCHAR(100),
//    estado VARCHAR(50)
//);

//CREATE TABLE Detalle_factura (
//    id SERIAL PRIMARY KEY,
//    id_factura INTEGER REFERENCES Factura(id),
//    id_producto INTEGER REFERENCES Productos(id),
//    cantidad_producto INTEGER NOT NULL,
//    subtotal DECIMAL(10, 2) NOT NULL
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
            Console.WriteLine("3. Menu Sucursal");
            Console.WriteLine("4. Menu Producto");
            Console.WriteLine("5. Salir");

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

                            clienteServicesCreate.Create(nuevoCliente);
                            Console.WriteLine("Cliente agregado correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "2":
                            ClienteRepository clienteServicesSelect = new ClienteRepository(connectionString);
                            Console.WriteLine("Listado de clientes:");
                            clienteServicesSelect.Select();
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

                            clienteServicesUpdate.Update(cliente, documentoActualizar);
                            Console.WriteLine("Cliente actualizado correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "4":
                            ClienteRepository clienteServicesDelete = new ClienteRepository(connectionString);
                            Console.WriteLine("Eliminar cliente con el nro de documento:");
                            string documentoDelete = Console.ReadLine();
                            clienteServicesDelete.Delete(documentoDelete);
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
                            int sucursalCreate = int.Parse(Console.ReadLine());
                            Console.WriteLine("Agregando nuevo detalle de factura...");
                            Console.Write("Ingrese el ID de la factura: ");
                            int idFacturaDetalleCreate = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el ID del producto: ");
                            int idProductoDetalleCreate = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese la cantidad del producto: ");
                            int cantidadProductoDetalleCreate = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el subtotal: ");
                            decimal subtotalDetalleCreate = decimal.Parse(Console.ReadLine());

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
                                IdSucursal = sucursalCreate
                            };

                            var nuevoDetalleFactura = new DetalleFacturaModel
                            {
                                IdFactura = idFacturaDetalleCreate,
                                IdProducto = idProductoDetalleCreate,
                                CantidadProducto = cantidadProductoDetalleCreate,
                                Subtotal = subtotalDetalleCreate
                            };

                            facturaServicesCreate.Create(nuevaFactura, nuevoDetalleFactura);
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
                            var detalleFacturaActualizar = new DetalleFacturaModel();
                            Console.WriteLine("Actualizando factura...");
                            Console.Write("Ingrese el ID de la factura a actualizar: ");
                            int idFacturaActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el ID del cliente: ");
                            int idClienteActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el ID del sucursal: ");
                            int idSucursalActualizar = int.Parse(Console.ReadLine());
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
                            Console.WriteLine("Actualizando detalle de factura...");
                            Console.Write("Ingrese el ID del detalle de factura a actualizar: ");
                            int idDetalleFacturaActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el ID de la factura: ");
                            int idFacturaDetalleActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el ID del producto: ");
                            int idProductoDetalleActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese la cantidad del producto: ");
                            int cantidadProductoDetalleActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el subtotal: ");
                            decimal subtotalDetalleActualizar = decimal.Parse(Console.ReadLine());

                            facturaActualizar.Id = idFacturaActualizar;
                            facturaActualizar.IdCliente = idClienteActualizar;
                            facturaActualizar.IdSucursal = idSucursalActualizar;
                            facturaActualizar.NroFactura = nroFacturaActualizar;
                            facturaActualizar.FechaHora = fechaHoraActualizar;
                            facturaActualizar.Total = totalActualizar;
                            facturaActualizar.TotalIva5 = totalIva5Actualizar;
                            facturaActualizar.TotalIva10 = totalIva10Actualizar;
                            facturaActualizar.TotalIva = totalIvaActualizar;
                            facturaActualizar.TotalLetras = totalLetrasActualizar;
                            facturaActualizar.Sucursal = sucursalActualizar;

                            detalleFacturaActualizar.Id = idDetalleFacturaActualizar;
                            detalleFacturaActualizar.IdFactura = idFacturaDetalleActualizar;
                            detalleFacturaActualizar.IdProducto = idProductoDetalleActualizar;
                            detalleFacturaActualizar.CantidadProducto = cantidadProductoDetalleActualizar;
                            detalleFacturaActualizar.Subtotal = subtotalDetalleActualizar;

                            facturaServicesUpdate.Update(facturaActualizar, idFacturaActualizar, detalleFacturaActualizar);
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
            else if (opcionMenu == "3")
            {
                Console.WriteLine("Menú Sucursal Seleccione una opción");
                Console.WriteLine("1. Agregar Sucursal");
                Console.WriteLine("2. Listar Sucursales");
                Console.WriteLine("3. Actualizar Sucursal");
                Console.WriteLine("4. Eliminar Sucursal");
                Console.WriteLine("5. Salir");

                bool salirMenuSucursal = false;
                while (!salirMenuSucursal)
                {
                    Console.Write("Ingrese la opción deseada: ");
                    string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            SucursalRepository sucursalServicesCreate = new SucursalRepository(connectionString);
                            Console.WriteLine("Creando nueva sucursal...");
                            Console.Write("Ingrese la descripción: ");
                            string descripcionCreate = Console.ReadLine();
                            Console.Write("Ingrese la dirección: ");
                            string direccionCreate = Console.ReadLine();
                            Console.Write("Ingrese el teléfono: ");
                            string telefonoCreate = Console.ReadLine();
                            Console.Write("Ingrese el Whatsapp: ");
                            string whatsappCreate = Console.ReadLine();
                            Console.Write("Ingrese el mail: ");
                            string mailCreate = Console.ReadLine();
                            Console.Write("Ingrese el estado: ");
                            string estadoCreate = Console.ReadLine();

                            var nuevaSucursal = new SucursalModel
                            {
                                Descripcion = descripcionCreate,
                                Direccion = direccionCreate,
                                Telefono = telefonoCreate,
                                Whatsapp = whatsappCreate,
                                Mail = mailCreate,
                                Estado = estadoCreate
                            };

                            sucursalServicesCreate.Create(nuevaSucursal);
                            Console.WriteLine("Sucursal agregada correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "2":
                            SucursalRepository sucursalServicesSelect = new SucursalRepository(connectionString);
                            Console.WriteLine("Listado de sucursales:");
                            sucursalServicesSelect.Select();
                            Console.WriteLine("Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "3":
                            SucursalRepository sucursalServicesUpdate = new SucursalRepository(connectionString);
                            var sucursalActualizar = new SucursalModel();
                            Console.WriteLine("Actualizando sucursal...");
                            Console.Write("Ingrese el ID de la sucursal a actualizar: ");
                            int idSucursalActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese la descripción: ");
                            string descripcionActualizar = Console.ReadLine();
                            Console.Write("Ingrese la dirección: ");
                            string direccionActualizar = Console.ReadLine();
                            Console.Write("Ingrese el teléfono: ");
                            string telefonoActualizar = Console.ReadLine();
                            Console.Write("Ingrese el Whatsapp: ");
                            string whatsappActualizar = Console.ReadLine();
                            Console.Write("Ingrese el mail: ");
                            string mailActualizar = Console.ReadLine();
                            Console.Write("Ingrese el estado: ");
                            string estadoActualizar = Console.ReadLine();

                            sucursalActualizar.Id = idSucursalActualizar;
                            sucursalActualizar.Descripcion = descripcionActualizar;
                            sucursalActualizar.Direccion = direccionActualizar;
                            sucursalActualizar.Telefono = telefonoActualizar;
                            sucursalActualizar.Whatsapp = whatsappActualizar;
                            sucursalActualizar.Mail = mailActualizar;
                            sucursalActualizar.Estado = estadoActualizar;

                            sucursalServicesUpdate.Update(sucursalActualizar, idSucursalActualizar);
                            Console.WriteLine("Sucursal actualizada correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "4":
                            SucursalRepository sucursalServicesDelete = new SucursalRepository(connectionString);
                            Console.WriteLine("Eliminando sucursal...");
                            Console.Write("Ingrese el ID de la sucursal a eliminar: ");
                            int idSucursalEliminar = int.Parse(Console.ReadLine());
                            sucursalServicesDelete.Delete(idSucursalEliminar);
                            Console.WriteLine("Sucursal eliminada correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "5":
                            salirMenuSucursal = true;
                            break;

                        default:
                            Console.WriteLine("Opción no válida. Por favor, ingrese una opción válida. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            else if (opcionMenu == "4")
            {
                Console.WriteLine("Menú Productos Seleccione una opción");
                Console.WriteLine("1. Agregar Producto");
                Console.WriteLine("2. Listar Productos");
                Console.WriteLine("3. Actualizar Producto");
                Console.WriteLine("4. Eliminar Producto");
                Console.WriteLine("5. Salir");

                bool salirMenuProductos = false;
                while (!salirMenuProductos)
                {
                    Console.Write("Ingrese la opción deseada: ");
                    string opcionProductos = Console.ReadLine();

                    switch (opcionProductos)
                    {
                        case "1":
                            ProductosRepository productoServicesCreate = new ProductosRepository(connectionString);
                            Console.WriteLine("Creando nuevo producto...");
                            Console.Write("Ingrese la descripción: ");
                            string descripcionProductoCreate = Console.ReadLine();
                            Console.Write("Ingrese la cantidad mínima: ");
                            int cantidadMinimaCreate = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese la cantidad en stock: ");
                            int cantidadStockCreate = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el precio de compra: ");
                            decimal precioCompraCreate = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese el precio de venta: ");
                            decimal precioVentaCreate = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese la categoría: ");
                            string categoriaCreate = Console.ReadLine();
                            Console.Write("Ingrese la marca: ");
                            string marcaCreate = Console.ReadLine();
                            Console.Write("Ingrese el estado: ");
                            string estadoProductoCreate = Console.ReadLine();

                            var nuevoProducto = new ProductosModel
                            {
                                Descripcion = descripcionProductoCreate,
                                CantidadMinima = cantidadMinimaCreate,
                                CantidadStock = cantidadStockCreate,
                                PrecioCompra = precioCompraCreate,
                                PrecioVenta = precioVentaCreate,
                                Categoria = categoriaCreate,
                                Marca = marcaCreate,
                                Estado = estadoProductoCreate
                            };

                            productoServicesCreate.Create(nuevoProducto);
                            Console.WriteLine("Producto agregado correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "2":
                            ProductosRepository productoServicesSelect = new ProductosRepository(connectionString);
                            Console.WriteLine("Listado de productos:");
                            productoServicesSelect.Select();
                            Console.WriteLine("Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "3":
                            ProductosRepository productoServicesUpdate = new ProductosRepository(connectionString);
                            var productoActualizar = new ProductosModel();
                            Console.WriteLine("Actualizando producto...");
                            Console.Write("Ingrese el ID del producto a actualizar: ");
                            int idProductoActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese la descripción: ");
                            string descripcionProductoActualizar = Console.ReadLine();
                            Console.Write("Ingrese la cantidad mínima: ");
                            int cantidadMinimaActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese la cantidad en stock: ");
                            int cantidadStockActualizar = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese el precio de compra: ");
                            decimal precioCompraActualizar = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese el precio de venta: ");
                            decimal precioVentaActualizar = decimal.Parse(Console.ReadLine());
                            Console.Write("Ingrese la categoría: ");
                            string categoriaActualizar = Console.ReadLine();
                            Console.Write("Ingrese la marca: ");
                            string marcaActualizar = Console.ReadLine();
                            Console.Write("Ingrese el estado: ");
                            string estadoProductoActualizar = Console.ReadLine();

                            productoActualizar.Id = idProductoActualizar;
                            productoActualizar.Descripcion = descripcionProductoActualizar;
                            productoActualizar.CantidadMinima = cantidadMinimaActualizar;
                            productoActualizar.CantidadStock = cantidadStockActualizar;
                            productoActualizar.PrecioCompra = precioCompraActualizar;
                            productoActualizar.PrecioVenta = precioVentaActualizar;
                            productoActualizar.Categoria = categoriaActualizar;
                            productoActualizar.Marca = marcaActualizar;
                            productoActualizar.Estado = estadoProductoActualizar;

                            productoServicesUpdate.Update(productoActualizar);
                            Console.WriteLine("Producto actualizado correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "4":
                            ProductosRepository productoServicesDelete = new ProductosRepository(connectionString);
                            Console.WriteLine("Eliminando producto...");
                            Console.Write("Ingrese el ID del producto a eliminar: ");
                            int idProductoEliminar = int.Parse(Console.ReadLine());
                            productoServicesDelete.Delete(idProductoEliminar);
                            Console.WriteLine("Producto eliminado correctamente. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "5":
                            salirMenuProductos = true;
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
