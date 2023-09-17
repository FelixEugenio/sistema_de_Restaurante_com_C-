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
    public partial class AdicionarFuncionarios : Form
    {
        public AdicionarFuncionarios()
        {
            InitializeComponent();
        }

        public int id = 0;

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string query = "";

            if (id == 0) // insere
            {
                query = "insert into staff Values(@Name,@Telefone,@Role)";
            }
            else // actualiza
            {
                query = "update staff set sName = @Name,sPhone = @Telefone,sRole = @Role where staffID =@id";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Role", cbRole.Text);
            ht.Add("@Telefone", txtPhone.Text);
            ht.Add("@Name", txtNome.Text);

            if (ClasseMae.SQL(query, ht) > 0)
            {
                MessageBox.Show("Salvado com sucesso");
                id = 0;
                txtNome.Text = "";
                txtNome.Focus();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
