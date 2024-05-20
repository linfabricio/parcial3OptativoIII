using System;
using System.ComponentModel.DataAnnotations;

namespace OptativoIII_Parcial.Modelos
{
    public class FacturaModel
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        public int IdSucursal { get; set; }

        [RegularExpression(@"^\d{3}-\d{3}-\d{6}$", ErrorMessage = "El formato de la factura debe ser XXX-XXX-XXXXXX.")]
        public string NroFactura { get; set; }

        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El total de IVA al 5% es obligatorio.")]
        public decimal TotalIva5 { get; set; }

        [Required(ErrorMessage = "El total de IVA al 10% es obligatorio.")]
        public decimal TotalIva10 { get; set; }

        [Required(ErrorMessage = "El total de IVA es obligatorio.")]
        public decimal TotalIva { get; set; }

        [Required(ErrorMessage = "El total en letras es obligatorio.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "El total en letras debe tener al menos 6 caracteres.")]
        public string TotalLetras { get; set; }

        public string Sucursal { get; set; }
    }
}
