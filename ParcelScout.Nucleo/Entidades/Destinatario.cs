using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelScout.Nucleo.Entidades
{
    public class Destinatario : Persistent
    {

        public override int Id { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; } //Calle + Número + Avenida + Colonia + Referencia
        public string CodigoPostal { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Recibe { get; set; } //Nombre adicional de quien recibe el paquete.

        public static Destinatario ObtenerPorId(int id)
        {
            Destinatario d = new Destinatario();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(d.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    d = (crit.UniqueResult<Destinatario>());

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return d;
        }

        public static bool Guardar(string nombre, string domicilio, string codigoPostal,
                                   string ciudad, string estado, string telefono, string correo,
                                   string recibe)
        {
            bool realizado = false;
            try
            {

                Destinatario d = new Destinatario();
                d.Nombre = nombre;
                d.Domicilio = domicilio;
                d.CodigoPostal = codigoPostal;
                d.Ciudad = ciudad;
                d.Estado = estado;
                d.Telefono = telefono;
                d.Correo = correo;
                d.Recibe = recibe;

                d.Save();
                realizado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return realizado;
        }

        public static bool GuardarCambios(int id, string nombre, string domicilio, string codigoPostal,
                                  string ciudad, string estado, string telefono, string correo,
                                  string recibe)
        {
            bool realizado = false;
            try
            {

                Destinatario d = ObtenerPorId(id);
                d.Nombre = nombre;
                d.Domicilio = domicilio;
                d.CodigoPostal = codigoPostal;
                d.Ciudad = ciudad;
                d.Estado = estado;
                d.Telefono = telefono;
                d.Correo = correo;
                d.Recibe = recibe;

                d.Update();
                realizado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return realizado;
        }

        public static bool Delete(int id)
        {
            bool realizado = false;

            try
            {
                Destinatario d = ObtenerPorId(id);
                d.Delete();

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
