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
using System.Text.RegularExpressions;
namespace WindowsFormsApplication1
/* Intha form la bill pay pandrathuku use pandrom,vandi no vangitu,avanoda fine amount get pandrom apram print screen ku send pandrom */
{
    public partial class Payment : Form
    {
        public string Vehno//Oru form data va innoru form ku send pandrathuku intha code use pandrom
        {
            get { return vhno; }
        }
        string vhno;
        int usid, total, paid=0,topay,flag=0;
        public Payment()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)//Back Button
        {
            Welcome f2 = new Welcome();
            this.Visible = false;
            f2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)//Search Button
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
                        usid = read.GetInt16("usrid");
                    }
                    read.Close();
                    cmd.CommandText = "select total,paid from bill where usrid=" + usid;
                    read = cmd.ExecuteReader();
                    if (!read.HasRows) { MessageBox.Show("No arrears found"); return; }
                    while (read.Read())
                    {
                        total = read.GetInt32("total");
                        topay= read.GetInt32("paid"); 
                        total -= topay;
                        textBox6.Text = Convert.ToString(total);
                    }
                    read.Close();
                    cmd.CommandText = "select * from owndet where usrid=" + usid;
                    read = cmd.ExecuteReader();
                    while (read.Read())//while the data doesnt become empty
                    {
                        textBox2.Text = read.GetString("name");
                        textBox3.Text = read.GetString("phone");
                        textBox5.Text = read.GetString("fthname");
                        textBox4.Text = read.GetString("address");
                    }
                    read.Close();
                    button2.Enabled = true;
                    //connection.Close();
                }
                connection.Close();
                textBox7.Focus();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("The error occured is" + ex);
                this.Close();
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)//Evalo amount pay pandrom indra textbox 
        {
            if (textBox7.Text == "") { return; }
            if ((Regex.IsMatch(textBox7.Text, "[A-Z]"))||(Regex.IsMatch(textBox7.Text, "[a-z]"))) { textBox7.Text = ""; return; }
            paid = Convert.ToInt32(textBox7.Text);
        }

        private void button2_Click(object sender, EventArgs e)//Print button 
        {
            try{
            string serv = "localhost", database = "namefield", uid = "root", pasw = "121417181";
                string constring;
                constring = "SERVER=" + serv + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + pasw + ";";
                MySqlConnection connection = new MySqlConnection(constring);
                connection.Open();
                string query = "select * from bill where usrid='" + usid + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute a SQL command to read ,which uses class "MySqlDataReader" as a command for ExecuteReader using cmd
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    if (read.GetInt32("paid") == 0)
                        flag = 1;
                    else
                        flag = 0;
                }
                read.Close();
                if (flag == 1)
                {
                    cmd.CommandText = "update bill set paid=" + paid + " where usrid=" + usid + ";";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = "insert into bill (usrid,total,paid) values (" + usid + "," + total + "," + paid + ");";
                    cmd.ExecuteNonQuery();
                }
                Print f9 = new Print();
                f9.Vehino = vhno;//Intha edathula send pandrom
                f9.ShowDialog();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("The error occured is" + ex);
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Vandi no
        {
            vhno = textBox1.Text;
        }


        private void Form4_KeyDown(object sender, KeyEventArgs e)//Shortcut keys
        {
            if ((e.KeyCode == Keys.Enter) && (button2.Enabled == true)) { button2.PerformClick(); }//Print button
            if ((e.KeyCode == Keys.Escape) && (button2.Enabled == true))//Reload form
            {
                Payment f4 = new Payment();
                this.Visible = false;
                f4.ShowDialog();
            }
            if ((e.KeyCode == Keys.Escape) && (button2.Enabled == false)) { button3.PerformClick(); }//Back Button
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

    }
}
