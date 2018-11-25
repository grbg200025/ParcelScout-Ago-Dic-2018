using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelScout.Nucleo.Entidades
{
    public class Cliente : Persistent
    {
        public override int Id { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string RFC { get; set; }

        public static IList<Cliente> ObtenerTodos()
        {
            IList<Cliente> clientes;
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(new Cliente().GetType());

                    clientes = crit.List<Cliente>();
                    session.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return clientes;
        }

        public static Cliente ObtenerPorId(int id)
        {
            Cliente c = new Cliente();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(c.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    c = (crit.UniqueResult<Cliente>());

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return c;
        }

        public static bool Guardar(string nombre, string domicilio, string telefono,
                                    string correo, string rfc)
        {
            bool realizado = false;
            try
            {

                Cliente c = new Cliente();
                c.Nombre = nombre;
                c.Domicilio = domicilio;
                c.Telefono = telefono;
                c.Correo = correo;
                c.RFC = rfc;

                c.Save();
                realizado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return realizado;
        }

        public static bool GuardarCambios(int id, string nombre, string domicilio,
                                        string telefono, string correo, string rfc)
        {
            bool realizado = false;

            try
            {

                Cliente c = ObtenerPorId(id);
                c.Nombre = nombre;
                c.Domicilio = domicilio;
                c.Telefono = telefono;
                c.Correo = correo;
                c.RFC = rfc;

                c.Update();

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
                Cliente c = ObtenerPorId(id);
                c.Delete();

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
