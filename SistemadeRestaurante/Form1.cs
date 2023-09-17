using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SistemadeRestaurante
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            if (ClasseMae.ValididarUsuario(txtUsuario.Text, txtSenha.Text))
            {
                Sucesso sc = new Sucesso();
                sc.ShowDialog();
                this.Hide();
                MenuPrincipal menu = new MenuPrincipal();
                menu.Show();
            }
            else
            {
                Erro er = new Erro();
                er.ShowDialog();

            }



            // Vou Criar Conexao com a base de dados 
    /*
            if (txtUsuario.Text =="")
            {
                MessageBox.Show("Campo Vazio");
            }
            else if (txtSenha.Text=="")
            {
                MessageBox.Show("Campo Vazio");
            }
            else
            {
                try
                {
                    SqlConnection Conexao = new SqlConnection("Data Source=ROSA\\SQLEXPRESS;Initial Catalog=db_restaurante;Integrated Security=true;");
                    SqlCommand cmd = new SqlCommand("select * from Tbl_Usuario where usuario =@usuario and senha=@senha",Conexao);
                    cmd.Parameters.AddWithValue("@usuario",txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    if (dt.Rows.Count>0)
                    {
                        MessageBox.Show("Login Feito Com sucesso");
                        this.Hide();
                        MenuPrincipal menu = new MenuPrincipal();
                        menu.Show();
                    }
                    else
                    {
                        MessageBox.Show("usuario ou senha incorrectos");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(""+ ex);
                }


            }

            */
        }
    }
}
