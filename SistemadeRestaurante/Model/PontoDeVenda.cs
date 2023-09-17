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
using System.Collections;
using System.IO;
using System.Drawing.Printing;



namespace SistemadeRestaurante.Model
{
    public partial class PontoDeVenda : Form
    {
        public PontoDeVenda()
        {
            InitializeComponent();
        }

        public int MainId = 0;
        public string TipoDeEntrega;
        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void PontoDeVenda_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AdicionarCategoria();
            ProdutoP.Controls.Clear();
            CarregarProdutos();
          
        }

        public void AdicionarCategoria()
        {
            string query = "select * from category";
            SqlCommand cmd = new SqlCommand(query,ClasseMae.Conexao);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            categoriaP.Controls.Clear();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.FillColor = Color.FromArgb(50,55,89);
                    b.Size = new Size(134,45);
                    b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    b.Text = row["catName"].ToString();
                   

                    categoriaP.Controls.Add(b);
                }

            } 
        }

        

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdicionarItems(string id,String prodID,string nome , string cat ,string price,Image pImage)
        {
            var w = new UcProduto()
            {
                PNome = nome,
                PPreco = price,
                PCategoria = cat,
                PImage = pImage,
                id = Convert.ToInt32(prodID),
            };

            ProdutoP.Controls.Add(w);

            w.OnSelect += (ss, ee) =>
            {
            var wdg = (UcProduto)ss;

            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                if (Convert.ToInt32(item.Cells["prodID"].Value) == wdg.id)
                {
                    item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) * 
                        double.Parse(item.Cells["dgvPrice"].Value.ToString());

                        return;
                 }
                    
                }

                guna2DataGridView1.Rows.Add(new object[] {0, 0, wdg.id, wdg.PNome, 1, wdg.PPreco ,wdg.PPreco});
                CalcularTotal();

            };
        }

      

        private void CarregarProdutos()
        {
            string query = "select * from products inner join category on catID = CategoryID";
            SqlCommand cmd = new SqlCommand(query, ClasseMae.Conexao);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {

                Byte[] imagearray = (byte[])item["pImage"];
                byte[] imagebyarray = imagearray;

                AdicionarItems("0",item["pID"].ToString(), item["pName"].ToString(), item["CategoryID"].ToString(), item["pPrice"].ToString(),
                    Image.FromStream(new MemoryStream(imagearray)));
            }
        }

        private void ucProduto6_Load(object sender, EventArgs e)
        {

        }

        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            foreach ( var item in ProdutoP.Controls)
            {
                var pro = (UcProduto)item;
                pro.Visible = pro.PNome.ToLower().Contains(txtPesquisar.Text.Trim().ToLower());
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //

            int contador = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                contador++;
                row.Cells[0].Value = contador;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            
            lblTotal.Text = "0";

            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                lblTotal.Text = Convert.ToString(double.Parse(lblTotal.Text) + double.Parse(guna2DataGridView1.Rows[i].Cells[5].Value.ToString()));
            }

    */
            
        }


        private void CalcularTotal()
        {
            double total = 0;
            lblTotal.Text = "";

            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                total += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            }

            lblTotal.Text = total.ToString("N2");
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            lblMesa.Text = "";
            lblGarconete.Text = "";
            lblMesa.Visible = false;
            lblGarconete.Visible = false;
            guna2DataGridView1.Rows.Clear();
            MainId = 0;
            lblTotal.Text = "00";
        }

        private void btnEntrega_Click(object sender, EventArgs e)
        {
            lblMesa.Text = "";
            lblGarconete.Text = "";
            lblMesa.Visible = false;
            lblGarconete.Visible = false;
            TipoDeEntrega = "Entrega";
        }

        private void btntake_Click(object sender, EventArgs e)
        {
            lblMesa.Text = "";
            lblGarconete.Text = "";
            lblMesa.Visible = false;
            lblGarconete.Visible = false;
            TipoDeEntrega = "Take Away";
        }

        private void btnComer_Click(object sender, EventArgs e)
        {
            TipoDeEntrega = "Comer Aqui";
            MesaSelecionada frm = new MesaSelecionada();
            frm.ShowDialog();

            if (frm.NomeTabela != "")
            {
                lblMesa.Text = frm.NomeTabela;
                lblMesa.Visible = true;
            }
            else
            {
                lblMesa.Text = "";
                lblMesa.Visible = false;
            }

            GarconeteSelecionado fr = new GarconeteSelecionado();
            fr.ShowDialog();

            if (fr.NomeGarcon != "")
            {
                lblGarconete.Text = fr.NomeGarcon;
                lblGarconete.Visible = true;
            }
            else
            {
                lblGarconete.Text = "";
                lblGarconete.Visible = false;
            }



        }

        private void btncozinha_Click(object sender, EventArgs e)
        {
            string query = "";
            string query2 = "";

            int detalhesId = 0;

            if (MainId == 0)
            {
                query = @" insert into tblMain values (@aData,@aTime,@NomeTabela,@NomeGarcon,@status,@TipoPedido,@total,@recebido,@troco);

                                                Select SCOPE_IDENTITY()";
            }
            else
            {
                query = @" update tblMain set status = @status, total = @total,received=@recebido,change=@troco where MainID =@ID)";
            }

            
            SqlCommand cmd = new SqlCommand(query,ClasseMae.Conexao);
            cmd.Parameters.AddWithValue("@ID", MainId);
            cmd.Parameters.AddWithValue("@aData",Convert.ToDateTime( DateTime.Now.Date));
            cmd.Parameters.AddWithValue("@aTime", DateTime.Now.ToShortTimeString());
            cmd.Parameters.AddWithValue("@NomeTabela", lblMesa.Text);
            cmd.Parameters.AddWithValue("@NomeGarcon", lblGarconete.Text);
            cmd.Parameters.AddWithValue("@status", "Pendente");
            cmd.Parameters.AddWithValue("@TipoPedido", TipoDeEntrega);
            cmd.Parameters.AddWithValue("@total", Convert.ToDouble(lblTotal.Text));
            cmd.Parameters.AddWithValue("@recebido", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@troco", Convert.ToDouble(0));


            if (ClasseMae.Conexao.State == ConnectionState.Closed) { ClasseMae.Conexao.Open(); }
            if (MainId == 0) { MainId = Convert.ToInt32(cmd.ExecuteScalar()); } else { cmd.ExecuteNonQuery();}
            if (ClasseMae.Conexao.State == ConnectionState.Open) { ClasseMae.Conexao.Close(); }

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                detalhesId = Convert.ToInt32(row.Cells["dgvid"].Value.ToString());

                if (detalhesId == 0)
                {
                    query2 = @" insert into tblDetails values(@MainID,@prodID,@Qty,@preco,@montante)";
                }
                else
                {
                    query2 = @" update tblDetails set proID = @prodID, qty = @Qty,price = @preco,amount=@montante where DetailID = @ID";
                }

                SqlCommand cmd2 = new SqlCommand(query2, ClasseMae.Conexao);
                cmd2.Parameters.AddWithValue("@ID", detalhesId);
                cmd2.Parameters.AddWithValue("@MainID", MainId);
                cmd2.Parameters.AddWithValue("@prodID",Convert.ToInt32( row.Cells["prodID"].Value));
                cmd2.Parameters.AddWithValue("@Qty", Convert.ToInt32( row.Cells["dgvQty"].Value));
                cmd2.Parameters.AddWithValue("@preco", Convert.ToInt32( row.Cells["dgvPrice"].Value));
                cmd2.Parameters.AddWithValue("@montante", Convert.ToInt32( row.Cells["dgvAmount"].Value));

                if (ClasseMae.Conexao.State == ConnectionState.Closed) { ClasseMae.Conexao.Open(); }
                cmd2.ExecuteNonQuery();
                if (ClasseMae.Conexao.State == ConnectionState.Open) { ClasseMae.Conexao.Close(); }

            }

            MessageBox.Show("Salvo Com Sucesso");
            MainId = 0;
            detalhesId = 0;
            lblMesa.Text = "";
            lblGarconete.Text = "";
            lblMesa.Visible = false;
            lblGarconete.Visible = false;
            lblTotal.Text = "00";



        }

        public int id = 0;
        private void btnListar_Click(object sender, EventArgs e)
        {
            ListaDeContas lc = new ListaDeContas();
            lc.ShowDialog();

            if (lc.MainID >0)
            {
                id = lc.MainID;
                CarregarEntradas();
            }
        }

        private void CarregarEntradas()
        {
            string qry2 = @"select * from tblMain m
                 inner join tblDetails d on m.MainID = d.MainID
                 inner join products p on p.pID=d.proID
                  where m.MainID =" + id + "";

            SqlCommand cmd2 = new SqlCommand(qry2, ClasseMae.Conexao);
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);

            if (dt2.Rows[0]["orderType"].ToString() == "Delivery")
            {
                btnEntrega.Checked = true;
                lblGarconete.Visible = false;
                lblMesa.Visible = false;
            }

            else if (dt2.Rows[0]["orderType"].ToString() == "Take Away")
            {
                btnEntrega.Checked = true;
                lblGarconete.Visible = false;
                lblMesa.Visible = false;
            }

            else
            {
               btnComer.Checked = true;
                lblGarconete.Visible = true;
                lblMesa.Visible = true;
                
            }

            guna2DataGridView1.Rows.Clear();

            foreach (DataRow item in dt2.Rows )
            {

                lblMesa.Text = item["TableName"].ToString();
                lblGarconete.Text = item["WaiterName"].ToString();

                string detailid = item["DetailID"].ToString();
                string proName = item["pName"].ToString();
                string proID = item["proID"].ToString();
                string qty = item["qty"].ToString();
                string price = item["price"].ToString();
                string amount = item["amount"].ToString();
                

                object[] obj = {0,detailid,prodID,proName,qty,price,amount };
                guna2DataGridView1.Rows.Add(obj);

            }

            CalcularTotal();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            Caixa ck = new Caixa();
            ck.MainID = id;
            ck.amt = Convert.ToDouble(lblTotal.Text);
            ck.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            string texto = "Esta e a primiera frase";
            Font letra = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
            SolidBrush cor = new SolidBrush(Color.Black);
            Point ponto = new Point(100, 100);

            e.Graphics.DrawString(texto, letra, cor, ponto);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }
    }
}
