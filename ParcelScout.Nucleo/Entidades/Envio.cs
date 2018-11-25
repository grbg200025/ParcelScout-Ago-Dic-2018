using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelScout.Nucleo.Entidades
{
    public class Envio : Persistent
    {

        public override int Id { get; set; }
        public string Folio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Usuario Empleado { get; set; }
        public double Peso { get; set; }
        public string TipoContenido { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public string NoRastreo { get; set; }
        public Cliente Cliente { get; set; }
        public Destinatario Destinatario { get; set; }
        public Estado Estado { get; set; }

        // IList<Ubicacion> Ubicaciones;

        public static IList<Envio> ObtenerTodos()
        {
            IList<Envio> envios;
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(new Envio().GetType());


                    //crit.SetResultTransformer(Transformers.AliasToBean<Envio>());

                    envios = crit.List<Envio>();
                    session.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return envios;
        }

        public static Envio ObtenerPorId(int id)
        {
            Envio e = new Envio();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(e.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    e = (crit.UniqueResult<Envio>());

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return e;
        }

        public static bool Guardar(Usuario empleado, double peso, string tipoContenido, string descripcion,
                                    double precio, string noRastreo, Cliente cliente, Destinatario destinatario)
        {
            bool realizado = false;

            try
            {

                Envio e = new Envio();

                e.FechaCreacion = DateTime.Now;
                e.Empleado = empleado;
                e.Peso = peso;
                e.TipoContenido = tipoContenido;
                e.Descripcion = descripcion;
                e.Precio = precio;
                e.NoRastreo = noRastreo;
                e.Cliente = cliente;
                e.Destinatario = destinatario;
                e.Estado = Estado.EN_PROCESO;

                e.Save();

                e.Folio = GenerarFolio(e.Id, e.FechaCreacion);
                e.NoRastreo = e.Folio;

                e.Update();

                realizado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return realizado;
        }

        public static string GenerarFolio(int id, DateTime date)
        {
            string folio = "";

            //CUATRO DIGITOS DEL AÑO
            folio += date.Year;

            //NUMERO DE 6 DIGITOS CON 0s A LA IZQUIERDA
            int numeroFolio = id % 1000000;
            int digitosNoFolio = numeroFolio.ToString().Length;

            for (int i = 0; i < 6 - digitosNoFolio; i++)
            {
                folio += "0";
            }

            folio += numeroFolio;

            //LETRA CUANDO SE PASE EL LIMITE
            char[] letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            if ((id / 1000000) > 0)
            {
                folio = letras[(id / 1000000) - 1] + folio;
            }

            return folio;
        }

        public static bool Delete(int id)
        {
            bool realizado = false;

            try
            {
                Envio e = ObtenerPorId(id);
                e.Delete();

                realizado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return realizado;
        }

    }
}
