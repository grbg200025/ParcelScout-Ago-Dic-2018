using ParcelScout.Nucleo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParcelScout.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() {
            return View();
        }

        public ActionResult GestionUsuarios() {
            return View();
        }

        public ActionResult Registro() {
            return View();
        }

        public ActionResult RegistrarUsuario(string nombre, string cuenta, string correo, string password, string permiso) {
            ActionResult action = null;

            if (Usuario.Guardar(nombre, cuenta, correo, password, permiso))
            {
                action = Content("true");
            }
            else {
                action = Content("false");
            }

            return action;
        }

        public ActionResult ObtenerPorId(int id) {
            Usuario u = new Usuario();
            try
            {
                u = Usuario.ObtenerPorId(id);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
            return Json(u, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerTodos()
        {
            try
            {
                IList<Usuario> usuarios = Usuario.ObtenerTodos();
                return Json(new { data = usuarios }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EditarRegistro(int id) {
            ViewBag.IdUsuario = id;
            return PartialView("~/Views/Usuario/EditarRegistro.cshtml");
        }

        [HttpPost]
        public ActionResult ComprobarPwdViejo(string correo, string contrasenaVieja) {
            ActionResult action = null;

            Usuario u = Usuario.ObtenerPorLogin(correo, contrasenaVieja);

            if (u != null) {
                action = Content("true");
            } else {
                action = Content("false");
            }

            return action;
        }

        [HttpPost]
        public ActionResult GuardarCambios(int id, string nombre, string cuenta, string correo,
                                                string contrasena, string permiso) {
            ActionResult action = null;

            try {
                if (Usuario.GuardarCambios(id, nombre, cuenta, correo, contrasena, permiso)) {
                    action = Content("true");
                } else {
                    action = Content("false");
                }                
            } catch (Exception) {
                action = Content("false");
            }

            return action;
        }

        public ActionResult Delete(int id)
        {
            ActionResult action = null;

            if (Usuario.Delete(id))
            {
                action = Content("true");
            }
            else
            {
                action = Content("false");
            }

            return action;
        }

    }
}