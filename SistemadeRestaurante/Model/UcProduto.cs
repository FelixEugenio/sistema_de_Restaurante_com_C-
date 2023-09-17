using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemadeRestaurante.Model
{
   
    public partial class UcProduto : UserControl
    {
        public UcProduto()
        {
            InitializeComponent();
        }

        public event EventHandler OnSelect = null;

        public int id { get; set; }
        public string PPreco { get; set; }

        public string PCategoria { get; set; }



        public string PNome
        {
            get { return lblNome.Text; }
            set { lblNome.Text = value; }
        }

        public Image PImage
        {
            get { return txtImage.Image; }
            set { txtImage.Image = value; }
        }

        private void txtImage_Click(object sender, EventArgs e)
        {
            OnSelect?.Invoke(this, e);
        }
    }
}
