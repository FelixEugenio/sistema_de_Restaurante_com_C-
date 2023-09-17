using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace SistemadeRestaurante.Model
{
    public partial class AdicionarProduto : Form
    {
        public AdicionarProduto()
        {
            InitializeComponent();
        }

        public int id = 0;
        public int cId = 0;

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string query = "";

            if (id == 0) // insere
            {
                query = "insert into products Values(@Name,@Preco,@cod_categoria,@img)";
            }
            else // actualiza
            {
                query = "update products set pName = @Name,pPrice = @Preco,CategoryID = @cod_categoria,pImage = @img where pID =@id";
            }



            Image temp = new Bitmap(txtimage.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imageByteArray = ms.ToArray();


            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txNome.Text);
            ht.Add("@Preco", txPreco.Text);
            ht.Add("@cod_categoria", Convert.ToInt32(cbcategoria.SelectedValue));
            ht.Add("@img", imageByteArray);



            if (ClasseMae.SQL(query, ht) > 0)
            {
                MessageBox.Show("Salvado com sucesso");
                id = 0;
                cId = 0;
                txtNome.Text = "";
                cbcategoria.SelectedIndex = 0;
                cbcategoria.SelectedIndex = -1;
                txtNome.Focus();
            }
        }

        private void AdicionarProduto_Load(object sender, EventArgs e)
        {
            string query = "select catID 'id' , catName 'name' from category";
            ClasseMae.CBFILL(query,cbcategoria);
            if (cId>0)
            {
                cbcategoria.SelectedIndex = cId;
            }

            if (id > 0)
            {
                ParaAtualizarProdutoCarregado();
            }
        }

        
        private void btnProcurar_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        string filePath;
        Byte[] imageByteArray;

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png) |* .png; *.jpg";
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                filePath = ofd.FileName;
                txtimage.Image = new Bitmap(filePath);
            }
        }

        private void ParaAtualizarProdutoCarregado()
        {
            string qry = @"select * from products where pID="+id+"";
            SqlCommand cmd = new SqlCommand(qry,ClasseMae.Conexao);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count>0)
            {
                txNome.Text = dt.Rows[0]["pName"].ToString();
                txPreco.Text = dt.Rows[0]["pPrice"].ToString();

                Byte[] imageArray = (byte[])(dt.Rows[0]["pImage"]);
                byte[] imageByteArray = imageArray;
                txtimage.Image = Image.FromStream(new MemoryStream(imageArray));


            }


        }
    }

}
