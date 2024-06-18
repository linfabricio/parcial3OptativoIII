using System;
using System.ComponentModel.DataAnnotations;

namespace OptativoIII_Parcial.Modelos
{
    public class ProductosModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La cantidad mínima es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad mínima debe ser mayor a 1.")]
        public int CantidadMinima { get; set; }

        [Required(ErrorMessage = "La cantidad en stock es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad en stock no puede ser negativa.")]
        public int CantidadStock { get; set; }

        [Required(ErrorMessage = "El precio de compra es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio de compra debe ser un entero positivo.")]
        public decimal PrecioCompra { get; set; }

        [Required(ErrorMessage = "El precio de venta es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio de venta debe ser un entero positivo.")]
        public decimal PrecioVenta { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; }
    }
}
