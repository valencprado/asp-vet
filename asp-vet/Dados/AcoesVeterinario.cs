using asp_vet.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace asp_vet.Dados
{
    public class AcoesVeterinario
    {
        conexao con = new conexao();

        public void InserirVeterinario(ModelVeterinario cmVet)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbVeterinario values (default, @nomeVet)", con.MyConectarBD());
            cmd.Parameters.Add("@nomeVet", MySqlDbType.VarChar).Value = cmVet.nomeVet;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable ConsultarVeterinario()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbVeterinario", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Vet = new DataTable();
            da.Fill(Vet);
            con.MyDesconectarBD();
            return Vet;
        }
    }
}