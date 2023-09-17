using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using SistemadeRestaurante.Relatorios;
using System.Data.SqlClient;




namespace SistemadeRestaurante.Model
{
    public partial class ListaDeContas : Form
    {
        public ListaDeContas()
        {
            InitializeComponent();
        }

        public int MainID = 0;
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListaDeContas_Load(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void CarregarDados()
        {
            string query = "select MainID,TableName,WaiterName,status,orderType,total from tblMain where status <> 'Pendente';";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvTable);
            lb.Items.Add(dgvWaiter);
            lb.Items.Add(dgvStatus);
            lb.Items.Add(dgvType);
            lb.Items.Add(dgvTotal);

            ClasseMae.CarregarDados(query, guna2DataGridView1, lb);
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int contador = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                contador++;
                row.Cells[0].Value = contador;
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit") { 
}
            {
                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                this.Close();
            }

            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                printDocument1.Print();

                /*
                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                // imprimir 
                string query = @"select * from tblMain m inner join
                               tblDetails d on d.MainID = m.MainID
                               inner join products p on p.pID = d.proID
                               where m.MainID = '"+MainID+"'";


                SqlCommand cmd = new SqlCommand(query,ClasseMae.Conexao);
                ClasseMae.Conexao.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                ClasseMae.Conexao.Close();

                Print frm = new Print();
                ListadeContas ls = new ListadeContas();

                ls.SetDataSource(dt);
                
                frm.Show();
                */

                
                

               
                
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string texto = "Esta e a primiera frase";
            Font letra = new Font("Arial",14,FontStyle.Bold,GraphicsUnit.Pixel);
            SolidBrush cor = new SolidBrush(Color.Black);
            Point ponto = new Point(100,100);

            e.Graphics.DrawString(texto, letra, cor, ponto);
        }
    }
}
