using System;
using System.Text.RegularExpressions;
using OptativoIII_Parcial.Modelos;

namespace OptativoIII_Parcial.Validaciones
{
    public static class ValidatorSucursal
    {
        public static void ValidateSucursalModel(SucursalModel sucursal)
        {
            ValidateDireccion(sucursal.Direccion);
            ValidateMail(sucursal.Mail);
        }

        private static void ValidateDireccion(string direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion) || direccion.Length < 10)
            {
                throw new ArgumentException("La dirección debe tener al menos 10 caracteres de longitud.");
            }
        }

        private static void ValidateMail(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail) || !IsValidEmail(mail))
            {
                throw new ArgumentException("El correo electrónico no es válido.");
            }
        }

        private static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
