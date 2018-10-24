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
using System.IO;
//Ithula user data varum
namespace WindowsFormsApplication1
{
    public partial class Fulldetails : Form
    {
        string vhno;
        int usid;
        public string Vehno
        {
            set { vhno = value; }
        }
        public Fulldetails()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            try
            {
                string serv = "localhost", database = "namefield", uid = "root", pasw = "121417181";
                string constring;
                constring = "SERVER=" + serv + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + pasw + ";";
                MySqlConnection connection = new MySqlConnection(constring);
                connection.Open();
                string query = "select usrid from vehdet where vhno='" + vhno + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute a SQL command to read ,which uses class "MySqlDataReader" as a command for ExecuteReader using cmd
                MySqlDataReader read = cmd.ExecuteReader();
                if (!read.HasRows) { MessageBox.Show("vehicle not found"); }
                else
                {
                    while (read.Read())
                    {//while the data doesnt become empty
                        usid = read.GetInt32("usrid");
                    }
                    read.Close();
                    cmd.CommandText = "select * from vehdet left outer join owndet on vehdet.usrid=owndet.usrid and vehdet.usrid=" + usid + " left outer join fines on vehdet.usrid=fines.usrid limit 1;";
                    read = cmd.ExecuteReader();
                    if (!read.HasRows) { MessageBox.Show("details not found"); }
                    while (read.Read())//while the data doesnt become empty
                    {
                        label4.Text = vhno;
                        label5.Text = read.GetString("model");
                        label6.Text = read.GetString("color");
                        label7.Text = read.GetString("phone");
                        label8.Text = read.GetString("fthname");
                        label9.Text = read.GetString("name");
                        label13.Text = read.GetString("address");
                        label15.Text = read.GetInt32("ot").ToString();
                        label17.Text = read.GetInt32("ht").ToString();
                        label18.Text = read.GetInt32("dd").ToString();
                        label19.Text = read.GetInt32("ow").ToString();
                    }
                    read.Close();
                    connection.Close();
                    if (File.Exists(@"D:\dbms\" + usid + ".jpg"))
                    {
                        pictureBox1.ImageLocation = @"D:\dbms\" + usid + ".jpg";
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("The message occured is" + ex);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)//OK button
        {
            this.Visible = false;
        }



        private void Form8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { button1.PerformClick(); }
        }
    }
}
