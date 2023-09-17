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
    public partial class AdicionarCategoria : Form
    {
        public AdicionarCategoria()
        {
            InitializeComponent();
        }

        public int id = 0;

        private void AdicionarCategoria_Load(object sender, EventArgs e)
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
                query = "insert into category Values(@Name)";
            }
            else // actualiza
            {
                query = "update category set catName = @Name where catID =@id";
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
                MessageBox.Show("Salvado com sucesso");
                id = 0;
                txtNome.Text = "";
                txtNome.Focus();
            }
        }
    }
}
