using SistemadeRestaurante.Model;
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

namespace SistemadeRestaurante.View
{
    public partial class Categoria : Visualizador
    {
        public Categoria()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            string query = "select * from category";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);

            ClasseMae.CarregarDados(query,guna2DataGridView1,lb);

        }

        private void Categoria_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            AdicionarCategoria Cat = new AdicionarCategoria();
            Cat.ShowDialog();
            GetData();
        }

        public override void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name =="dgvedit")
            {
                AdicionarCategoria ad = new AdicionarCategoria();
                ad.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                ad.txtNome.Text= Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);
                ad.ShowDialog();
                GetData();
            }

           else  if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                string query = "Delete from category where catID='" + id+"'";
                Hashtable ht = new Hashtable();
                ClasseMae.SQL(query,ht);

                MessageBox.Show("Eliminado Com Sucesso");
                GetData();
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }
    }
}
