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
    public partial class Caixa : Form
    {
        public Caixa()
        {
            InitializeComponent();
        }

        public double amt;
        public int MainID = 0;

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            double amt = 0;
            double recebido = 0;
            double troco = 0;

            double.TryParse(guna2TextBox1.Text, out amt);
            double.TryParse(guna2TextBox2.Text, out recebido);

            troco = Math.Abs(amt - recebido);

            guna2TextBox3.Text = troco.ToString();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            
            string query = @" Update tblMain set total=@total,received=@rec,change = @change,status = 'Pago' where MainID = @id";
            Hashtable ht = new Hashtable();
            ht.Add("@id", MainID);
            ht.Add("@total",guna2TextBox1.Text);
            ht.Add("@rec", guna2TextBox2.Text);
            ht.Add("@change", guna2TextBox3.Text);

            if (ClasseMae.SQL(query,ht)>0)
            {
                MessageBox.Show("Pedido Pago com Sucesso");
            }


        }

        private void Caixa_Load(object sender, EventArgs e)
        {
            guna2TextBox1.Text = amt.ToString();
        }
    }
}
