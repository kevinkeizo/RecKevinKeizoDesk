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

namespace RecKevinKeizoDesk
{
    public partial class CriarCategoria : Form
    { // CONEXÃO COM O BANCO DE DADOS
        SqlConnection conn = new SqlConnection(@"workstation id=PortalKevin.mssql.somee.com;packet size=4096;user id=KevinKeizo_SQLLogin_1;pwd=in2fk1p7oi;data source=PortalKevin.mssql.somee.com;persist security info=False;initial catalog=PortalKevin");
        //CRIAR STRING DE COMANDO
        SqlCommand comando = new SqlCommand();
        //DATA READER
        SqlDataReader dr;
        public CriarCategoria()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CARREGARLISTACategorias()
        {

            conn.Open();
            comando.CommandText = "select * from Categories";
            dr = comando.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[1].ToString());
                }
            }
            conn.Close();

        }
        private void CARREGARCategorias()
        {

            conn.Open();
            comando.CommandText = "select * from Categories";
            dr = comando.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    listBox1.Items.Add(dr[1].ToString());
                }
            }
            conn.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                comando.CommandText = "insert into Categories([Name]) values ('" + textBox1.Text + "')";
                comando.ExecuteNonQuery();
                conn.Close();
                textBox1.Clear();
                MessageBox.Show("Categoria Registrada com sucesso!. ", "Informação",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                listBox1.Items.Clear();
                CARREGARCategorias();
                comboBox1.Items.Clear();
                CARREGARLISTACategorias();
            }
            catch
            {
                MessageBox.Show("Dados Invalidos. ", "Informação",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CriarCategoria_Load(object sender, EventArgs e)
        {
            comando.Connection = conn;
            CARREGARLISTACategorias();
            CARREGARCategorias();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "")
                {

                    MessageBox.Show("Busque o Usuário. ", "Informação",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                }
                else
                {
                    conn.Open();
                    comando.CommandText = "SELECT * FROM Categories DELETE Categories where [Name] = '" + comboBox1.Text + "' ";
                    comando.ExecuteNonQuery();
                    conn.Close();
                    comboBox1.Items.Clear();
                    comboBox1.Text = "";
                    MessageBox.Show("Categoria Deletado. ", "Informação",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("ALERTA Todas as Notícias Relacionada a essa Categoria será Excluida. ", "Informação",
                   MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    listBox1.Items.Clear();
                    CARREGARCategorias();
                    comboBox1.Items.Clear();
                    CARREGARLISTACategorias();
                }
            }
            catch
            {
                MessageBox.Show("Dados Invalidos. ", "Informação",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
