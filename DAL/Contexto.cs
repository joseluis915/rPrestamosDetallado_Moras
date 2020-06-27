using System;
using System.Collections.Generic;
using System.Text;
//Añadí estos using
using rPrestamosDetallado_Moras.Entidades;
using Microsoft.EntityFrameworkCore;

namespace rPrestamosDetallado_Moras.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Prestamos> Prestamos { get; set; }
        public DbSet<Moras> Moras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= DATA\MyBaseDeDatos");
        }
    }
}
