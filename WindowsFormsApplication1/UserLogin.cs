
/* DISCLAIMER: this is not meant for the lecturer ,it is only written for my team mates.I will be using tonglish here.
 * Hope you will Understand */

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
/*Run pannathum vara modhal form intha form than ithuku per form1,ithula namba user name and password vaanguvom apram login pannvom
 * 
 Apram ore oru vishayam ,ella form kum ithayae type pandrathuku enaku bore adikudhu,modhal form la explain pannathu,
 marupidiyum type panna maaten , dhaivaseithu nyabakam vechikonga*/
namespace WindowsFormsApplication1
{   
    public partial class UserLogin : Form
    {
        string usrnme,pass;//Ithula namba username password a yum vaanguvom
        public UserLogin()//First ithu than run aagum program start panna paathu
        {
            InitializeComponent();//Ithu Form desginer.cs indra file la iruku
        }

        private void button1_Click(object sender, EventArgs e)//Login button click panna ithula irukura code run aagum
        {
            try//error vandhuchuna,ithu stuck aagama exit aaguradhuku helo pannum
            {
                string serv = "localhost", database = "namefield", uid = "root", pasw = "121417181";//Ithelam database la irukuradhu,firstum,thirdum same aa vae irukatum
                //Secondum,fourthum database poruthu change pannanum
                string constring;
                //Keela irukura line than database ku connect panna help pannum
                constring = "SERVER=" + serv + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + pasw + ";";
                MySqlConnection connection = new MySqlConnection(constring);//Ithula mela irukura  line a intha line vazhiya database ku send pannuvom
                connection.Open();//Connection on panna soldrom
                string query = "SELECT name,pass FROM admins where admins.name='" + usrnme + "';";//Ithu than DBMS query
                MySqlCommand cmd = new MySqlCommand(query, connection);//Itha database ku send pandra line ithu
                MySqlDataReader read = cmd.ExecuteReader();//Intha line apram athuku keela ulla lines fulla DBMS la "select" statement ku usepandrom
                if (!read.HasRows) { MessageBox.Show("Username not found"); }//Ethavadhu data iruka nu check pandrom,illa na intha line run aagum
                while (read.Read())//Data iruntha intha line run aagum
                {
                        if (pass == read.GetString("pass"))//Password check pandrom,crt a iruntha ithu kulla pogum
                        {
                            MessageBox.Show("Details Found:");
                            Welcome f2 = new Welcome();//Adutha form a open pandra thuku intha moonu line use pandrom
                            this.Visible = false;
                            f2.ShowDialog();

                        }
                        else//Password crt illa nu soldrathuku
                        {
                            MessageBox.Show("password incorrect");
                        }
                }
                connection.Close();//Connection close pandrathuku use pandrom
            }
            catch (Exception ex)//Mela irukura lines la edhavadhu prachana iruntha ithu run aagum 
            {
                MessageBox.Show("The error occured is"+ex);
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Username box la edhavadhu type panna enna nadakum endrathu ithula iruku
        {
            usrnme = textBox1.Text;//variable ku set pandrom
            button1.Enabled = true;
            //Login button a enable pandrom,illati edhume type pannama click pannuvaanga
        }

        private void textBox2_TextChanged(object sender, EventArgs e)//Password box la edhavadhu type panna enna nadakum endrathu ithula iruku
        {
            pass = textBox2.Text;//variable ku set pandrom
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)//Shortcut keys podurathuku kaaga use pandran
        {

            if (e.KeyCode == Keys.Escape)//Enter press panna enna nadakum endrathuku intha pandrom
            { 
                this.Close(); 
            }
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        


    }
}
