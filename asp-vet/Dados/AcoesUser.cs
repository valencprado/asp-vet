using asp_vet.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace asp_vet.Dados
{
    public class AcoesUser
    {
        conexao con = new conexao();

        public void inserirTipo(ModelUser cmTipo)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbTipoUsuario values (default, @usuario", con.MyConectarBD());
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = cmTipo.usuario;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
        public DataTable consultaTipo()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbTipoUsuario");
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable tipo = new DataTable();
            da.Fill(tipo);
            con.MyDesconectarBD();
            return tipo;
        }
    }
}