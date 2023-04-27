using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Librerias base de Datos 
using System.Configuration;
using System.Data.SqlClient;

//Libreria mover el form
using System.Runtime.InteropServices;

namespace Login
{
    public partial class frmLogin : Form
    {
        //Base de datos
        public void login()
        {
            try
            {
                //Llamar a Cnn
                string cnn = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
                //Usar Sql
                using (SqlConnection conexion = new SqlConnection(cnn))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT usuario, Password FROM Users WHERE usuario='" + txtUser.Text + "' AND Password='" + txtPassword.Text + "'", conexion))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            string user = dr.GetString(0);
                            string password = dr.GetString(1);
                            MessageBox.Show("User: " + user + ", Password: " + password);
                            MessageBox.Show("Inicio de sesión exitoso");

                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al iniciar sesión: " + ex.Message);
            }
        }

        //Fin de Base de datos

        public frmLogin()
        {
            InitializeComponent();
        }

        //Mover el Form sin borde
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if(txtUser.Text == "USUARIO"){
                txtUser.Text = "";
                txtUser.ForeColor = Color.LightGray;
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text==""){
                txtUser.Text = "USUARIO";
                txtUser.ForeColor= Color.LightGray;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "CONTRASEÑA")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.LightGray;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "CONTRASEÑA";
                txtPassword.ForeColor = Color.LightGray;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            //Mover el form sin bordes
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //Mover el form sin bordes(Panel 2)
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            login();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
