using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecKevinKeizoDesk
{
    public partial class CriarUsuario : Form
    { // CONEXÃO COM O BANCO DE DADOS
        SqlConnection conn = new SqlConnection(@"workstation id=PortalKevin.mssql.somee.com;packet size=4096;user id=KevinKeizo_SQLLogin_1;pwd=in2fk1p7oi;data source=PortalKevin.mssql.somee.com;persist security info=False;initial catalog=PortalKevin");
        //CRIAR STRING DE COMANDO
        SqlCommand comando = new SqlCommand();
        //DATA READER
        SqlDataReader dr;
        private string password;
        public CriarUsuario()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CriarUsuario_Load(object sender, EventArgs e)
        {
            comando.Connection = conn;
            CARREGARLISTA();
            CARREGARLISTAEMAIL();
            CARREGARLISTASENHA();
            CARREGARLISTAASPNETUSERS();
        }
        private void CARREGARLISTA()
        {
            
            conn.Open();
            comando.CommandText = "select * from AspNetUsers";
            dr = comando.ExecuteReader();
            if (dr.HasRows)
            {
                int valor;
                while (dr.Read())
                {
                    label8.Text = (dr[0].ToString());
                    valor = Convert.ToInt32(label8.Text);
                    valor = int.Parse(label8.Text) + 1;
                    label8.Text = valor.ToString();
                }
            }
            conn.Close();

        }
        private void CARREGARLISTAEMAIL()
        {

            conn.Open();
            comando.CommandText = "select * from AspNetUsers";
            dr = comando.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    listBox4.Items.Add(dr[1].ToString());

                }
            }
            conn.Close();

        }
        private void CARREGARLISTASENHA()
        {

            conn.Open();
            comando.CommandText = "select * from AspNetUsers";
            dr = comando.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    listBox5.Items.Add (dr[3].ToString());
                }
            }
            conn.Close();

        }
        private void CARREGARLISTAASPNETUSERS()
        {

            conn.Open();
            comando.CommandText = "select * from AspNetUsers";
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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Criptografia()
        {
            password = textBox5.Text;
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);
            password = BitConverter.ToString(hash).Replace("-", "");
            label3.Text = password.ToString().ToLower();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" & textBox4.Text == "" & textBox5.Text == "" )
            {
                MessageBox.Show("Preencher Campos. ", "Informação",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    Criptografia();
                    conn.Open();
                    comando.CommandText = "insert into AspNetUsers([Id],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName]) values ('" +label8.Text+ "','" + textBox4.Text + "','" + 0 + "','" + label3.Text + "','" + label9.Text + "','" + "" + "','" + 0 + "','" + 0 + "','" + "" + "','" + 1 + "','" + 0 + "','" + textBox4.Text + "')";
                    comando.ExecuteNonQuery();
                    conn.Close();
                    textBox4.Clear();
                    textBox5.Clear();
                    label2.Visible = true;
                    label3.Visible = true;
                    MessageBox.Show("Registro " + textBox1.Text + " Criado com sucesso!.", "Informação",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    label2.Visible = false;
                    label3.Visible = false;
                    listBox4.Items.Clear();
                    listBox5.Items.Clear();
                    CARREGARLISTA();
                    CARREGARLISTASENHA();
                    CARREGARLISTAEMAIL();
                    comboBox1.Items.Clear();
                    CARREGARLISTAASPNETUSERS();

                }
                catch
                {
                    MessageBox.Show("Dados Invalidos. ", "Informação",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


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
                    comando.CommandText = "SELECT * FROM AspNetUsers DELETE AspNetUsers where [Email] = '" + comboBox1.Text + "' ";
                    comando.ExecuteNonQuery();
                    conn.Close();
                    comboBox1.Items.Clear();
                    comboBox1.Text = "";
                    CARREGARLISTAASPNETUSERS();
                    MessageBox.Show("Usuário Deletado. ", "Informação",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listBox4.Items.Clear();
                    listBox5.Items.Clear();
                    CARREGARLISTA();
                    CARREGARLISTASENHA();
                    CARREGARLISTAEMAIL();
                }
            }
            catch
            {
                MessageBox.Show("Dados Invalidos. ", "Informação",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
