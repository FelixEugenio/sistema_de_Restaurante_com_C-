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

namespace SistemadeRestaurante.View
{
    public partial class Cozinha : Form
    {
        public Cozinha()
        {
            InitializeComponent();
        }

        private void Cozinha_Load(object sender, EventArgs e)
        {
            PegaPedidos(); 
        }

        private void PegaPedidos()
        {
            flowLayoutPanel1.Controls.Clear();
            string query = @" select * from tblMain where status = 'Pendente'";
            SqlCommand cmd = new SqlCommand(query,ClasseMae.Conexao);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            FlowLayoutPanel p1;

            for (int i=0;i<dt.Rows.Count ;i++)
            {
                p1 = new FlowLayoutPanel();
                p1.AutoSize = true;
                p1.Width = 230;
                p1.Height = 350;
                p1.FlowDirection = FlowDirection.TopDown;
                p1.BorderStyle = BorderStyle.FixedSingle;
                p1.Margin = new Padding(10,10,10,10);

             FlowLayoutPanel   p2 = new FlowLayoutPanel();
                p2 = new FlowLayoutPanel();
                p2.BackColor = Color.FromArgb(50,55,89);
                p2.AutoSize = true;
                p2.Width = 230;
                p2.Height = 125;
                p2.FlowDirection = FlowDirection.TopDown;
                p2.Margin = new Padding(0, 0, 0, 0);

                Label lbl = new Label();
                lbl.ForeColor = Color.White;
                lbl.Margin = new Padding(10,10,3,0);
                lbl.AutoSize = true;

                Label lbl1 = new Label();
                lbl1.ForeColor = Color.White;
                lbl1.Margin = new Padding(10, 5, 3, 0);
                lbl1.AutoSize = true;

                Label lbl2 = new Label();
                lbl2.ForeColor = Color.White;
                lbl2.Margin = new Padding(10, 5, 3, 0);
                lbl2.AutoSize = true;

                Label lbl3 = new Label();
                lbl3.ForeColor = Color.White;
                lbl3.Margin = new Padding(10, 5, 3, 0);
                lbl3.AutoSize = true;

                lbl.Text = "Mesa :"+dt.Rows[i]["TableName"].ToString();
                lbl1.Text = "Nome do Garcon :" + dt.Rows[i]["WaiterName"].ToString();
                lbl2.Text = "Hora do Pedido :" + dt.Rows[i]["aTime"].ToString();
                lbl3.Text = "Tipo de Pedido :" + dt.Rows[i]["orderType"].ToString();

                p2.Controls.Add(lbl);
                p2.Controls.Add(lbl1);
                p2.Controls.Add(lbl2);
                p2.Controls.Add(lbl3);

                p1.Controls.Add(p2);
                

                // adicionar os produtos no pedido
                int mid = 0;
                mid = Convert.ToInt32(dt.Rows[i]["MainID"].ToString());

                string query2 = @"select * from tblMain m
                 inner join tblDetails d on m.MainID = d.MainID
                 inner join products p on p.pID=d.proID
                  where m.MainID ="+mid+"";

                

                SqlCommand cmd2 = new SqlCommand(query2, ClasseMae.Conexao);
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);

                for (int j =0;j<dt2.Rows.Count;j++)
                {
                    Label lb5 = new Label();
                    lb5.ForeColor = Color.White;
                    lb5.Margin = new Padding(10,5,3,0);
                    lb5.AutoSize = true;

                    int no = j + 1;
                    lb5.Text = "" + "" + dt2.Rows[j]["pName"].ToString()+""+dt2.Rows[j]["qty"].ToString();
                    p1.Controls.Add(lb5);
                }

                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                b.AutoRoundedCorners = true;
                b.Size = new Size(100,35);
                b.FillColor = Color.FromArgb(241,85,126);
                b.Margin = new Padding(30,5,3,10);
                b.Text = "Completar";
                b.Tag = dt.Rows[i]["MainID"].ToString();

                b.Click += new EventHandler(b_click);
                p1.Controls.Add(b);

                flowLayoutPanel1.Controls.Add(p1);

            }
            
            }

        private void b_click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as Guna.UI2.WinForms.Guna2Button).Tag.ToString());
            

            string qry1 = @" update tblMain set status = 'Completo' where MainID=@ID";
            Hashtable ht = new Hashtable();
            ht.Add("@ID",id);

            if (ClasseMae.SQL(qry1,ht)>0)
            {
                MessageBox.Show("Salvo Com Sucesso");
            }

            PegaPedidos();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

       

       
    }

