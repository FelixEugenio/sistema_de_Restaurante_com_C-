using SistemadeRestaurante.Model;
using SistemadeRestaurante.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemadeRestaurante
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        // Metodo para adicionar controles no menu principal

        public  void AddControles(Form f)
        {
            centerpainel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            centerpainel.Controls.Add(f);
            f.Show();
            
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2ControlBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = ClasseMae.USER;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AddControles(new Home());
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            AddControles(new Categoria());
        }

        private void BtnMesa_Click(object sender, EventArgs e)
        {
            AddControles(new Mesas());
        }

        private void btnFuncionario_Click(object sender, EventArgs e)
        {
            AddControles(new Funcionarios());
        }

        private void BtnProduto_Click(object sender, EventArgs e)
        {
            AddControles(new Produto());
        }

        private void btnVenda_Click(object sender, EventArgs e)
        {
            PontoDeVenda po = new PontoDeVenda();
            po.Show();
        }

        private void btnCozinha_Click(object sender, EventArgs e)
        {
            AddControles(new Cozinha());
        }
    }
}
