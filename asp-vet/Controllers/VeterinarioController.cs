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
    public class VeterinarioController : Controller
    {
        AcoesVeterinario av = new AcoesVeterinario();

        public void CarregaVet() 
        {
            List<SelectListItem> vet = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdVeterinaria;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbVeterinario", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    vet.Add(new SelectListItem 
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()

                    });

                    con.Close();
                }
                ViewBag.vet = new SelectList(vet, "Value", "Text");
                        
                    
            }
        
        }

        public ActionResult CadVet()
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

        public ActionResult CadVet(ModelVeterinario cmVet) 
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                CarregaVet();
                cmVet.codVet = Request["CodVet"];
                av.InserirVeterinario(cmVet);
                ViewBag.msgCad = "Cadastro realizado com sucesso!";
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }
        }

        public ActionResult ConsVet()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                GridView dgv = new GridView();
                dgv.DataSource = av.ConsultarVeterinario();
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
        // GET: Veterinario
        public ActionResult Index()
        {
            return View();
        }
    }
}