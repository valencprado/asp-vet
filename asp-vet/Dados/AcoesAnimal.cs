using asp_vet.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace asp_vet.Dados
{
    public class AcoesAnimal
    {
        conexao con = new conexao();

        public void InserirAnimal(ModelAnimal cmA)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbAnimal values (default, @nomeAnimal, @codTipoAnimal, @codCliente)", con.MyConectarBD());
            cmd.Parameters.Add("@nomeAnimal", MySqlDbType.VarChar).Value = cmA.nomeAnimal;
            cmd.Parameters.Add("@codTipoAnimal", MySqlDbType.VarChar).Value = cmA.codTipoAnimal;
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cmA.codCliente;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable ConsultarAnimal()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbAnimal", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Animal = new DataTable();
            da.Fill(Animal);
            con.MyDesconectarBD();
            return Animal;
        }
    }
}