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
//Ithu namba Second form la update info nu kudutha vara form ithu,ithula vandi ku fines set pannuvom
namespace WindowsFormsApplication1
{
    public partial class UpdateDetails : Form
    {
        string vhno;//vandi no
        int ow=0, ot=0, dd=0, ht=0, usid,total;//ow-one way,ot-overtake,ht-helmet,dd-drink and drive,usid- yaar vandi
        public UpdateDetails()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)//Update button click panna ithu run aagum
        {
            try
            {
                //onnume change pannala na keela irukura if statement run aagum
                if ((ot == 0) && (ht == 0) && (dd == 0) && (ow == 0)) { MessageBox.Show("No changes Found"); return ; }
                total = (ow * 200) + (ot * 300) + (dd * 500) + (ht * 100);//Total kanaku podurom
                string serv = "localhost", database = "namefield", uid = "root", pasw = "121417181";
                string constring;
                constring = "SERVER=" + serv + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + pasw + ";";
                MySqlConnection connection = new MySqlConnection(constring);
                connection.Open();
                string query = "select * from fines where usrid='" + usid + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute a SQL command to read ,which uses class "MySqlDataReader" as a command for ExecuteReader using cmd
                MySqlDataReader read = cmd.ExecuteReader();
               
                if (!read.HasRows)
                {
                    read.Close();//Itha pota than vera DBMS lines use panna mudiyum
                    cmd.CommandText = "insert into fines (usrid,ot,ht,dd,ow) values (" + usid + "," + ot + "," + ht + "," + dd + "," + ow + ");";
                    cmd.ExecuteNonQuery();//Ithu insert update delete indra Queries ku mattum use pannuvom

                }
                else//mela irukura maari than work aagum
                {
                    read.Close();
                    cmd.CommandText = "update fines set ot=" + ot + ",ht=" + ht + ",dd=" + dd + ",ow=" + ow + " where usrid=" + usid + ";";
                    cmd.ExecuteNonQuery();
                }
                cmd.CommandText = "insert into bill (total,usrid,paid) values (" + total + "," + usid + ",0);";
                cmd.ExecuteNonQuery();//Ithuvum athe case
                connection.Close();
                MessageBox.Show("details uploaded successfully");

                Welcome f2 = new Welcome();//Form 2 kae porom,ithu enna form nu therla na marupidiyum formm2.cs padithu paakavum
                this.Visible = false;
                f2.ShowDialog();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("The error occured is" + ex);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)//Search button click panna ithu work aagum
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
                    cmd.CommandText = "select * from owndet where usrid="+usid;
                    read=cmd.ExecuteReader();
                    if (!read.HasRows) { MessageBox.Show("details not found"); }
                    while(read.Read())//while the data doesnt become empty
                    {
                        textBox2.Text = read.GetString("name");
                        textBox3.Text = read.GetString("phone");
                        textBox5.Text = read.GetString("fthname");
                        textBox4.Text = read.GetString("address");
                    }
                    read.Close();                 button2.Enabled = true;
                    //connection.Close();
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The error occured is" + ex);
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            vhno = textBox1.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//Checkbox tick panna ithu nadakum
        {
            if (checkBox1.Checked)//Check panniruntha
            {
                numericUpDown1.ReadOnly = false;
            }
            else//Pannala na
            {
                numericUpDown1.ReadOnly = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)//Mela irukura athe kadha than
        {

            if (checkBox2.Checked)
            {
                numericUpDown2.ReadOnly = false;
            }
            else
            {
                numericUpDown2.ReadOnly = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox3.Checked)
            {
                numericUpDown3.ReadOnly = false;
            }
            else
            {
                numericUpDown3.ReadOnly = true;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox4.Checked)
            {
                numericUpDown4.ReadOnly = false;
            }
            else
            {
                numericUpDown4.ReadOnly = true;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)//Number set panna enna nadakum
        {
            ot = Convert.ToInt32(numericUpDown1.Value);//antha box la varadhu number a irukaadhu,athu naala intha convert.to use pandrom,variable la store pandrathuku ore vazhi
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)//Mela irukura athe kadhai
        {
            ht = Convert.ToInt32(numericUpDown2.Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            dd = Convert.ToInt32(numericUpDown3.Value);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            ow = Convert.ToInt32(numericUpDown4.Value);
        }

        private void button3_Click(object sender, EventArgs e)//Back Button click panna
        {
            Welcome f2 = new Welcome();
            this.Visible = false;
            f2.ShowDialog();
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)//Shortcut keys
        {
            if ((e.KeyCode == Keys.Enter) && (button2.Enabled == true)) { button2.PerformClick(); }//Update info button work aagum
            if ((e.KeyCode == Keys.Escape) && (button2.Enabled == true))//Marupidiyum athe form load aagum
            {
                UpdateDetails f3 = new UpdateDetails();
                this.Visible = false;
                f3.ShowDialog();
            }
            if ((e.KeyCode == Keys.Escape) && (button2.Enabled == false)) { button3.PerformClick(); }//Back button work aagum
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
