using System;
using System.Collections.Generic;
using System.Text;
//Añadí este using
using System.ComponentModel.DataAnnotations;

namespace rPrestamosDetallado_Moras.Entidades
{
    public class MorasDetalle
    {
        [Key]
        public int Id { get; set; }
        public int MorasId { get; set; }
        public int PrestamoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public float Valor { get; set; }
        public MorasDetalle(int morasId, int prestamoId, DateTime fecha, float valor)
        {
            Id = 0;
            MorasId = morasId;
            PrestamoId = prestamoId;
            Fecha = fecha;
            Valor = valor;
        }
    }
}