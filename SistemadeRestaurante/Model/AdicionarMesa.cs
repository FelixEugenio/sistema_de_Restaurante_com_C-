using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace SistemadeRestaurante.Model
{
    public partial class AdicionarMesa : Form
    {
        public AdicionarMesa()
        {
            InitializeComponent();
        }

        public int id = 0;
        private void AdicionarMesa_Load(object sender, EventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string query = "";

            if (id == 0) // insere
            {
                query = "insert into tables Values(@Name)";
            }
            else // actualiza
            {
                query = "update tables set tname = @Name where tid =@id";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            if (txtNome.Text == "")
            {
                MessageBox.Show("Campo Vazio");
                return;
            }

            ht.Add("@Name", txtNome.Text);

            if (ClasseMae.SQL(query, ht) > 0)
            {
                Cadastrado cd = new Cadastrado();
                cd.ShowDialog();
                id = 0;
                txtNome.Text = "";
                txtNome.Focus();
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
