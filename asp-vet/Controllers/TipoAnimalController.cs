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
    public class TipoAnimalController : Controller
    {
        AcoesTipoAnimal aTP = new AcoesTipoAnimal();


       public void CarregaTipoAnimal() 
        {

            List<SelectListItem> tipoAnimal = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdVeterinaria;User=root;pwd=Figure.09")) 
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from TipoAnimal;", con);
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
            ViewBag.tipoAnimal = new SelectList(tipoAnimal, "Value", "Text");
        }

        public ActionResult CadTipoAnimal() 
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
        public ActionResult CadTipoAnimal(ModelTipoAnimal cmTipoAnimal)
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                CarregaTipoAnimal();
                cmTipoAnimal.codTipoAnimal = Request["CodTipoAnimal"];
                aTP.InserirTipoAnimal(cmTipoAnimal);
                ViewBag.msgCad = "Cadastro efetuado com sucesso!";
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }
        }

        public ActionResult ConsTipoAnimal()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                GridView dgv = new GridView();
                dgv.DataSource = aTP.ConsultarTipoAnimal();
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

        public ActionResult Index()
        {
            return View();
        }
    }
}