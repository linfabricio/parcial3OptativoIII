using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OptativoIII_Parcial.Modelos
{
    public class ClienteModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El apellido debe tener al menos 3 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "La cédula debe tener al menos 3 caracteres.")]
        public string Documento { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "El celular debe ser numérico y tener 10 dígitos.")]
        public string Celular { get; set; }

        public int IdBanco { get; set; }
        public string Direccion { get; set; }
        public string Mail { get; set; }
        public string Estado { get; set; }
    }
}
