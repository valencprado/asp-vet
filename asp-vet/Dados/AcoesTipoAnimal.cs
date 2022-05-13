using asp_vet.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace asp_vet.Dados
{
    public class AcoesTipoAnimal
    {
        conexao con = new conexao();

        public void InserirTipoAnimal(ModelTipoAnimal cmTipoA) 
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbTipoAnimal values (default, @tipoAnimal)", con.MyConectarBD());
            cmd.Parameters.Add("@tipoAnimal", MySqlDbType.VarChar).Value = cmTipoA.TipoAnimal;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable ConsultarTipoAnimal()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbTipoAnimal", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable TipoA = new DataTable();
            da.Fill(TipoA);
            con.MyDesconectarBD();
            return TipoA;
        }

    }
}