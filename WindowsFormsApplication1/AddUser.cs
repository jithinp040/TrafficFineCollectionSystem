using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
/* Intha form la pudusa oru user with password ready pandrathuku use pandrom */
namespace WindowsFormsApplication1
{
    public partial class AddUser : Form
    {
        string nwusr, psw, cpsw;//Username,password,confirm password
        public AddUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//Add User Button
        {
            try
            {
                string serv = "localhost", database = "namefield", uid = "root", pasw = "121417181";
                string constring;
                constring = "SERVER=" + serv + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + pasw + ";";
                MySqlConnection connection = new MySqlConnection(constring);
                connection.Open();
                string query = "SELECT name,pass FROM admins where admins.name='" + nwusr + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute a SQL command to read ,which uses class "MySqlDataReader" as a command for ExecuteReader using cmd
                MySqlDataReader read = cmd.ExecuteReader();
                if (!read.HasRows)
                {
                    read.Close();
                    if (psw == null) { MessageBox.Show("please type a password"); }//Password type pannala na
                    else if ((psw != "") && (cpsw == null)) { MessageBox.Show("please retype the password"); }//Confirm password type pannala na
                    else if (cpsw == psw)
                    {
                        cmd.CommandText = "INSERT INTO admins (name,pass) VALUES ('" + nwusr + "','" + psw + "');";
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("User added successfully");
                        Welcome f2 = new Welcome();
                        this.Visible = false;
                        f2.ShowDialog();
                    }
                    else//Rendu password match aagala na
                    {
                        MessageBox.Show("passwords do not match");
                    }
                }
                else//Intha name already iruntha
                {
                    MessageBox.Show("Username already exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This is the error occured\n" + ex);
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Usrname
        {
            nwusr = textBox1.Text;
            button1.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)//Password
        {
            psw = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Confirm password
        {
            cpsw = textBox3.Text;
        }

        private void button2_Click(object sender, EventArgs e)//Back button
        {
            Welcome f2 = new Welcome();
            this.Visible = false;
            f2.ShowDialog();
        }

        private void Form5_KeyDown(object sender, KeyEventArgs e)//Shortcut keys
        {   if (e.KeyCode == Keys.Escape) { button2.PerformClick(); }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

    }
}
