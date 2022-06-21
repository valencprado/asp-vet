using asp_vet.Dados;
using asp_vet.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_vet.Controllers
{
    public class AtendimentoController : Controller
    {
        public void CarregaVet()
        {
            List<SelectListItem> vet = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdVeterinaria;User=root;pwd=Figure.09"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tbveterinario order by nomevet;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    vet.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.vet = new SelectList(vet, "Value", "Text");

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
            AcoesAtendimento acAtend = new AcoesAtendimento();

        public ActionResult CadAtendimento() // abre a tela
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                CarregaAnimal();
                CarregaVet();

            }
            return View();
        }

        [HttpPost]
        public ActionResult CadAtendimento(ModelAtendimento cmAtend) // cadastra no banco
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1") 
            {
                CarregaAnimal();
                CarregaVet();
                acAtend.TestarAgenda(cmAtend);

            if (cmAtend.confAgendamento == "0")
            {
                ViewBag.msg = "Horário Indisponível! Tente outro.";
            }
            else
            {
                    CarregaAnimal();
                    CarregaVet();
                    cmAtend.codVet = Request["veterinario"];
                cmAtend.codAnimal = Request["animal"];
                acAtend.InserirAtendimento(cmAtend);
                ViewBag.msg = "Cadastro Realizado com sucesso!";
            }

            }
            else
            {
                return RedirectToAction("SemAcesso", "Home");
            }
            return View();
        }
    }
}