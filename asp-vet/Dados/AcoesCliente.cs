using asp_vet.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace asp_vet.Dados
{
    public class AcoesCliente
    {
        conexao con = new conexao();

        public void InserirCliente(ModelCliente cmCli)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbCliente values (default, @nomeCliente, @telCliente, @emailCliente)", con.MyConectarBD());
            cmd.Parameters.Add("@nomeCliente", MySqlDbType.VarChar).Value = cmCli.nomeCliente;
            cmd.Parameters.Add("@telCliente", MySqlDbType.VarChar).Value = cmCli.telCliente;
            cmd.Parameters.Add("@emailCliente", MySqlDbType.VarChar).Value = cmCli.emailCliente;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable ConsultarCliente()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbCliente", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Cli = new DataTable();
            da.Fill(Cli);
            con.MyDesconectarBD();
            return Cli;
        }
    }
}