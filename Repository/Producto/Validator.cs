using System;
using OptativoIII_Parcial.Modelos;

namespace OptativoIII_Parcial.Validaciones
{
    public class ValidatorProducto
    {
        public static void ValidateProductosModel(ProductosModel producto)
        {
            ValidateCamposObligatorios(producto);
            ValidateCantidadMinima(producto.CantidadMinima);
            ValidatePrecioCompra(producto.PrecioCompra);
            ValidatePrecioVenta(producto.PrecioVenta);
            ValidateCantidadStock(producto.CantidadStock);
        }

        private static void ValidateCamposObligatorios(ProductosModel producto)
        {
            if (string.IsNullOrEmpty(producto.Descripcion))
            {
                throw new Exception("La descripción es obligatoria.");
            }

            if (producto.CantidadMinima <= 1)
            {
                throw new Exception("La cantidad mínima debe ser mayor a 1.");
            }

            if (producto.PrecioCompra <= 0 || producto.PrecioCompra != Math.Floor(producto.PrecioCompra))
            {
                throw new Exception("El precio de compra debe ser un entero positivo.");
            }

            if (producto.PrecioVenta <= 0 || producto.PrecioVenta != Math.Floor(producto.PrecioVenta))
            {
                throw new Exception("El precio de venta debe ser un entero positivo.");
            }
        }

        private static void ValidateCantidadMinima(int cantidadMinima)
        {
            if (cantidadMinima <= 1)
            {
                throw new Exception("La cantidad mínima debe ser mayor a 1.");
            }
        }

        private static void ValidatePrecioCompra(decimal precioCompra)
        {
            if (precioCompra <= 0 || precioCompra != Math.Floor(precioCompra))
            {
                throw new Exception("El precio de compra debe ser un entero positivo.");
            }
        }

        private static void ValidatePrecioVenta(decimal precioVenta)
        {
            if (precioVenta <= 0 || precioVenta != Math.Floor(precioVenta))
            {
                throw new Exception("El precio de venta debe ser un entero positivo.");
            }
        }

        private static void ValidateCantidadStock(int cantidadStock)
        {
            if (cantidadStock < 0)
            {
                throw new Exception("La cantidad en stock no puede ser negativa.");
            }
        }
    }
}
