using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


using examen_app.Models;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;


namespace examen_app.Controllers
{
    public class LoginController : Controller
    {
        static string connection = "Data Source=(ALEJANDRA);Initial Catalog= examen;Integrated Security=true";

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(User InUser)
        {
            InUser.Password = ConvertirSha256(InUser.Password);

            using (SqlConnection cn = new SqlConnection(connection))
            {

                SqlCommand cmd = new SqlCommand("validar_usuario", cn);
                cmd.Parameters.AddWithValue("email", InUser.Email);
                cmd.Parameters.AddWithValue("password", InUser.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                InUser.IdUser = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            if (InUser.IdUser != 0)
            {
                HttpContext.Session.SetString("Usuario", InUser.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();

            }
            
        }

        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }


    }
}
