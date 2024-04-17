using System;
using OptativoIII_Parcial.Modelos;

namespace OptativoIII_Parcial.Validaciones
{
    public class ValidatorCliente
    {
        public static void ValidateClienteModel(ClienteModel cliente)
        {
            if (string.IsNullOrEmpty(cliente.Nombre))
            {
                throw new Exception("El nombre es obligatorio.");
            }

            if (cliente.Nombre.Length < 3)
            {
                throw new Exception("El nombre debe tener al menos 3 caracteres.");
            }

            if (string.IsNullOrEmpty(cliente.Apellido))
            {
                throw new Exception("El apellido es obligatorio.");
            }

            if (cliente.Apellido.Length < 3)
            {
                throw new Exception("El apellido debe tener al menos 3 caracteres.");
            }

            if (string.IsNullOrEmpty(cliente.Documento))
            {
                throw new Exception("La cédula es obligatoria.");
            }
        }
    }
}
