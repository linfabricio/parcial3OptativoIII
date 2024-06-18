using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptativoIII_Parcial.Modelos
{
    public class DetalleFacturaModel
    {
        public int Id { get; set; }

        public int IdFactura { get; set; }

        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad de producto es obligatoria.")]
        public int CantidadProducto { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio.")]
        public decimal Subtotal { get; set; }
    }
}
