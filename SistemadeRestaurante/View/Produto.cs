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
using SistemadeRestaurante.Model;

namespace SistemadeRestaurante.View
{
    public partial class Produto : Visualizador
    {
        public Produto()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            string query = "select pID,pName,pPrice,CategoryID,c.catName from products p inner join category c on c.catID = p.CategoryID;";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPrice);
            lb.Items.Add(dgvCategory);
            lb.Items.Add(dgvCatid);

            ClasseMae.CarregarDados(query, guna2DataGridView1, lb);

        }
        private void Produto_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                AdicionarProduto ad = new AdicionarProduto();
                ad.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                ad.txtNome.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);
                ad.ShowDialog();
                GetData();
            }

            else if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                string query = "Delete from products where pID='" + id + "'";
                Hashtable ht = new Hashtable();
                ClasseMae.SQL(query, ht);

                MessageBox.Show("Eliminado Com Sucesso");
                GetData();
            }
        }

        private void txtPesquisar_TextChanged_1(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            AdicionarProduto Cat = new AdicionarProduto();
            Cat.ShowDialog();
            GetData();
        }
    }
}
