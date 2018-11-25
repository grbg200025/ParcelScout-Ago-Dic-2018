using ParcelScout.Nucleo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParcelScout.Controllers
{
    public class PaqueteController : Controller
    {
        // GET: Paquete
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PruebaGuardado() {
            ActionResult action = null;

            Usuario u = Usuario.ObtenerPorId(1);
            Cliente c = new Cliente();
            c.Nombre = "Iris";
            c.Domicilio = "Golfo de California, Calle: centro cívico.";
            c.Telefono = "6229846544";
            c.RFC = "GANI1234dfad";
            c.Correo = "iris@gmail.com";

            Destinatario d = new Destinatario();
            d.Nombre = "Dulce";
            d.Domicilio = "Somewhere at San vicente";
            d.CodigoPostal = "85477";
            d.Ciudad = "Guaymas";
            d.Estado = "Sonora";
            d.Correo = "dulce@gmail.com";
            d.Recibe = "Leonardo";
            d.Telefono = "45654325";



            if (Envio.Guardar(u, 123, "fragil", "un paquete con cinta roja o algo", 359.50, "", c, d)) {
                action = Content("TRUE");
            } else {
                action = Content("VALIO VERGA");
            }


            return action;
        }


    }
}