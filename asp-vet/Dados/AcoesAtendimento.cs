using asp_vet.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_vet.Dados
{
    public class AcoesAtendimento
    {
        conexao con = new conexao();

        public void TestarAgenda(ModelAtendimento agenda) 
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbAtendimento where dataAtend = @data and horaAtend = @hora", con.MyConectarBD());

            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = agenda.dataAtendimento;
            cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = agenda.horaAtendimento;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    agenda.confAgendamento = "0";
                }

            }

            else
            {
                agenda.confAgendamento = "1";
            }

            con.MyDesconectarBD();
        }
        public void InserirAtendimento(ModelAtendimento cm)
        {

            MySqlCommand cmd = new MySqlCommand("insert into tbAtendimento(codAtendimento, dataAtendimento, horaAtendimento, codVet, codAnimal, Diag) values (default, @data, @hora, @codVet, @codAnimal, @diag)", con.MyConectarBD());
            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = cm.dataAtendimento;
            cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = cm.horaAtendimento;
            cmd.Parameters.Add("@codVet", MySqlDbType.VarChar).Value = cm.codVet;
            cmd.Parameters.Add("@codAnimal", MySqlDbType.VarChar).Value = cm.codAnimal;
            cmd.Parameters.Add("@diag", MySqlDbType.VarChar).Value = cm.Diag;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
    }
}