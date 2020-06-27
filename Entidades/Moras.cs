using System;
using System.Collections.Generic;
using System.Text;
//Añadí este using
using System.ComponentModel.DataAnnotations;
//REGISTRO DETALLADO
using System.ComponentModel.DataAnnotations.Schema;

namespace rPrestamosDetallado_Moras.Entidades
{
    public class Moras
    {
        [Key]
        public int MoraId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public double Total { get; set; }

        //REGISTRO DETALLADO
        [ForeignKey("PrestamoId")]
        public List<MorasDetalle> Detalle { get; set; } = new List<MorasDetalle>();
    }
}
