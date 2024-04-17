using System;
using System.Text.RegularExpressions;
using OptativoIII_Parcial.Modelos;

namespace OptativoIII_Parcial.Validaciones
{
    public class ValidatorFactura
    {
        public static void ValidateFacturaModel(FacturaModel factura)
        {
            ValidateNroFactura(factura.NroFactura);
            ValidateTotales(factura.Total, factura.TotalIva5, factura.TotalIva10, factura.TotalIva);
            ValidateTotalLetras(factura.TotalLetras);
        }

        private static void ValidateNroFactura(string nroFactura)
        {
            if (string.IsNullOrEmpty(nroFactura))
            {
                throw new Exception("El número de factura es obligatorio.");
            }

            if (!EsPatronNroFacturaValido(nroFactura))
            {
                throw new Exception("El número de factura no cumple con el formato requerido.");
            }
        }

        private static bool EsPatronNroFacturaValido(string nroFactura)
        {
            string patron = @"^\d{3}-\d{3}-\d{6}$";

            // Verificar si el número de factura coincide con el patrón
            return Regex.IsMatch(nroFactura, patron);
        }


        private static void ValidateTotales(decimal total, decimal totalIva5, decimal totalIva10, decimal totalIva)
        {
            if (total <= 0 || totalIva5 <= 0 || totalIva10 <= 0 || totalIva <= 0)
            {
                throw new Exception("Los totales deben ser mayores que cero.");
            }
        }

        private static void ValidateTotalLetras(string totalLetras)
        {
            if (string.IsNullOrEmpty(totalLetras))
            {
                throw new Exception("El total en letras es obligatorio.");
            }

            if (totalLetras.Length < 6)
            {
                throw new Exception("El total en letras debe tener al menos 6 caracteres.");
            }
        }
    }
}
