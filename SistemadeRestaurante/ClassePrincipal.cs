using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SistemadeRestaurante
{
    class ClassePrincipal
    {
        public static readonly string string_de_Conexao = "Data Source=ROSA\\SQLEXPRESS;Initial Catalog=db_restaurante;Persist Security Info-True;Password=;";
        public static SqlConnection Conexao = new SqlConnection(string_de_Conexao);

        // methodo para validar o Usuario
        public static bool ValididarUsuario(string usuario,string senha)
        {
            bool eValido = false;

            string consulta = @"select * from Tbl_Usuario where usuario='" + usuario + "' and senha='" + senha + "'";
            SqlCommand comando = new SqlCommand(consulta,Conexao);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            if (dt.Rows.Count>0)
            {
                eValido = true;
            }

            return eValido;
        }

    }
}
