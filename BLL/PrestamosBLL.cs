using System;
using System.Collections.Generic;
using System.Text;
//Añadí estos using
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using rPrestamosDetallado_Moras.DAL;
using rPrestamosDetallado_Moras.Entidades;

namespace rPrestamosDetallado_Moras.BLL
{
    public class PrestamosBLL
    {
        //=====================================================[ GUARDAR ]=====================================================
        public static bool Guardar(Prestamos prestamos)
        {
            if (!Existe(prestamos.PrestamoId))
                return Insertar(prestamos);
            else
                return Modificar(prestamos);
        }
        //=====================================================[ INSERTAR ]=====================================================
        private static bool Insertar(Prestamos prestamos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Prestamos.Add(prestamos);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        //=====================================================[ MODIFICAR ]=====================================================
        public static bool Modificar(Prestamos prestamos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                //-------------------------------------------[ REGISTRO DETALLADO ]-------------------------------------------------
                contexto.Database.ExecuteSqlRaw($"Delete FROM MorasDetalle Where PrestamoId={prestamos.PrestamoId}");

                foreach (var item in prestamos.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }
                //------------------------------------------------------------------------------------------------------------------

                contexto.Entry(prestamos).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        //=====================================================[ ELIMINAR ]=====================================================
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var prestamos = contexto.Prestamos.Find(id);
                if (prestamos != null)
                {
                    contexto.Prestamos.Remove(prestamos);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        //=====================================================[ BUSCAR ]=====================================================
        public static Prestamos Buscar(int id)
        {
            //-------------------[ REGISTRO DETALLADO ] -------------------
            Prestamos prestamos = new Prestamos();
            Contexto contexto = new Contexto();
            try
            {
                prestamos = contexto.Prestamos.Include(x => x.Detalle)
                    .Where(x => x.PrestamoId == id)
                    .SingleOrDefault();
            //-------------------------------------------------------------
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return prestamos;
        }
        //=====================================================[ GET LIST ]===================================================== 
        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            List<Prestamos> lista = new List<Prestamos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Prestamos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
        //=====================================================[ EXISTE ]===================================================== 
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Prestamos.Any(d => d.PrestamoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado;
        }
    }
}
