using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace SistemadeRestaurante
{
    class ClasseMae
    {

        public static readonly string string_de_Conexao = "Data Source=ROSA\\SQLEXPRESS;Initial Catalog=db_restaurant;Integrated Security=true;";
        public static SqlConnection Conexao = new SqlConnection(string_de_Conexao);

        // methodo para validar o Usuario
        public static bool ValididarUsuario(string usuario, string senha)
        {
            bool eValido = false;

            string consulta = @"select * from Tbl_Usuario where usuario='" + usuario + "' and senha='" + senha + "'";
            SqlCommand comando = new SqlCommand(consulta, Conexao);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                eValido = true;
                USER = dt.Rows[0]["Nome_usuario"].ToString();
            }

            return eValido;
        }

        // propriedade do usuario

        public static string user;

        public static string USER
        {
            get { return  user; }
            private set { user = value; }
        }

        // Metodo para operacao de  crud 

        public static int SQL(string query, Hashtable ht)
        {
            int res = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(query, Conexao);
                cmd.CommandType = CommandType.Text;

                foreach (DictionaryEntry item in ht)
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);

                }

                if (Conexao.State == ConnectionState.Closed) { Conexao.Open(); }
                res = cmd.ExecuteNonQuery();
                if (Conexao.State == ConnectionState.Closed) { Conexao.Close(); }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Conexao.Close();
            }

            return res;
        }

        // para carregar dados da basededados

        public static void CarregarDados(string query,DataGridView gv,ListBox lb)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, Conexao);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0;i<lb.Items.Count;i++)
                {
                    string colNam1 = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[colNam1].DataPropertyName = dt.Columns[i].ToString();
                  
                }

                gv.DataSource = dt;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Conexao.Close();
            }
        }


        // Para todas as Combobox
        public static void CBFILL(string query,ComboBox cb)
        {
            SqlCommand cmd = new SqlCommand(query,Conexao);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cb.DisplayMember = "name";
            cb.ValueMember = "id";
            cb.DataSource = dt;
            cb.SelectedIndex = -1;

        }
    }
}
