using asp_vet.Dados;
using asp_vet.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace asp_vet.Controllers
{
    public class ClienteController : Controller
    {
        AcoesCliente ac = new AcoesCliente();
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public void CarregaCliente()
        {
            List<SelectListItem> cliente = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdVeterinaria;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tbCliente;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cliente.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.cliente = new SelectList(cliente, "Value", "Text");

        }
        public ActionResult CadCliente()
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
            public ActionResult CadCliente(ModelCliente cmCliente) 
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                CarregaCliente();
                cmCliente.codCliente = Request["CodCliente"];
                ac.InserirCliente(cmCliente);
                ViewBag.msgCad = "Cadastro efetuado com sucesso!";
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }
        }
        public ActionResult ConsCliente() 
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                GridView dgv = new GridView();
                dgv.DataSource = ac.ConsultarCliente();
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

        }
    }
