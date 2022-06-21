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
    public class AnimalController : Controller
    {
        AcoesAnimal am = new AcoesAnimal();
       

        public void CarregaTipoAnimal()
        {

            List<SelectListItem> tipoAnimal = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdVeterinaria;User=root;pwd=Figure.09"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tipoanimal;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    tipoAnimal.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });

                }
                con.Close();
            }
            ViewBag.tipo = new SelectList(tipoAnimal, "Value", "Text");
        }
        public void CarregaCliente()
        {
            List<SelectListItem> cliente = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdVeterinaria;User=root;pwd=Figure.09"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tbCliente order by nomecliente;", con);
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

        public void CarregaAnimal() 
        {
            List<SelectListItem> animal = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdVeterinaria;User=root;pwd=Figure.09"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbAnimal order by NomeAnimal;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    animal.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.animal = new SelectList(animal, "Value", "Text");
        }
        public ActionResult CadAnimal()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                CarregaTipoAnimal();
                CarregaCliente();

                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }
        }

        [HttpPost]
        public ActionResult CadAnimal(ModelAnimal cmAnimal)
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                CarregaCliente();
                cmAnimal.codCliente = Request["cliente"];
                CarregaAnimal();
                cmAnimal.codAnimal = Request["animal"];
                CarregaTipoAnimal();
                cmAnimal.codTipoAnimal = Request["tipo"];
                am.InserirAnimal(cmAnimal);
                
                ViewBag.msgCad = "Cadastro Efetuado";
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }

        }

      
        

        public ActionResult ConsAnimal()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                GridView dgv = new GridView();
                dgv.DataSource = am.ConsultarAnimal();
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
        // GET: Animal
        public ActionResult Index()
        {
            return View();
        }
    }
}