using MySql.Data.MySqlClient;
using asp_vet.Dados;
using asp_vet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace asp_vet.Controllers
{
    public class HomeController : Controller
    {
        AcoesLogin acL = new AcoesLogin();
        AcoesUser acTipo = new AcoesUser();

        public void carregaTipoUsu()
        {
            List<SelectListItem> tipoUsu = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdVeterinaria;User=root;pwd=Figure.09"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbTipoUsuario;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tipoUsu.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.tipoUsu = new SelectList(tipoUsu, "Value", "Text");
        }

        public void carregaLogin()
        {
            List<SelectListItem> login = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdVeterinaria;User=root;pwd=Figure.09"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbLogin;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    login.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.login = new SelectList(login, "Value", "Text");
        }

        public ActionResult Index()
        {
            Session["tipo"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(ModelLogin cm)
        {
            acL.TestarUsuario(cm);

            if (cm.usuario != null)
            {
                FormsAuthentication.SetAuthCookie(cm.usuario, false);
                Session["usu"] = cm.usuario;
                Session["tipo"] = cm.codTipoUsuario;

                if (Session["usu"] != null && Session["tipo"].ToString() == "1")
                {
                    return RedirectToAction("MenuAdm", "Home");
                }
                else if (Session["usu"] != null && Session["tipo"].ToString() == "2")
                {
                    return RedirectToAction("MenuUser", "Home");

                }
                else
                {
                    ViewBag.msgLogar = "Usuário e/ou senha incorreto(s)";
                    return View();

                }
               
            }

            else
            {
                ViewBag.msgLogar = "Usuário e/ou senha incorreto(s)";
                return View();
            }
        }

        public ActionResult SemAcesso()
        {
            Response.Write("<script>alert('Sem Acesso - Faça o login no sistema')</script>");
            return View();
        }

        public ActionResult Logout()
        {
            Session["usu"] = null;
            Session["tipo"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CadTipo()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }
        }

        [HttpPost]
        public ActionResult CadTipo(ModelUser cmTipo)
        {
            acTipo.inserirTipo(cmTipo);
            ViewBag.msgCad = "Cadastro Efetuado";
            return View();
        }

        public ActionResult ConsCadTipo()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                GridView dgv = new GridView();
                dgv.DataSource = acTipo.consultaTipo();
                dgv.DataBind();
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                dgv.RenderControl(htw);
                ViewBag.GridViewString = sw.ToString();
                return View();
            }
            else
            {
                return RedirectToAction("SemAcesso", "Home");
            }
        }

        public ActionResult CadLogin()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                carregaTipoUsu();
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }
        }
        [HttpPost]
        public ActionResult CadLogin(ModelLogin cmLogin)
        {
            carregaTipoUsu();
            if (cmLogin.senha == cmLogin.confirmaSenha)
            {

                cmLogin.codTipoUsuario = Request["tipoUsu"];
                acL.inserirLogin(cmLogin);
                Response.Write("<script>alert('Cadastro efetuado com sucesso!')</script>");
            }
            else
            {
                Response.Write("<script>alert('As senhas não estão iguais.')</script>");
            }
            return View();
        }

        public ActionResult MenuAdm()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }


        }
        public ActionResult MenuUser()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "2")
            {
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }


        }
    }

    
}