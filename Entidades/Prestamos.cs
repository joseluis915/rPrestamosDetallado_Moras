using System;
using System.Collections.Generic;
using System.Text;
//Añadí este using
using System.ComponentModel.DataAnnotations;
//REGISTRO DETALLADO
using System.ComponentModel.DataAnnotations.Schema;

namespace rPrestamosDetallado_Moras.Entidades
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public double Monto { get; set; }

        //REGISTRO DETALLADO
        [ForeignKey("PrestamoId")]
        public List<MorasDetalle> Detalle { get; set; } = new List<MorasDetalle>();
    }
}
