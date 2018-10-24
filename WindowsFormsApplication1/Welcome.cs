using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Username password type panni login pannathum intha form varum,ithula intha software vechi enna enna laam pannalam nu iruku
namespace WindowsFormsApplication1
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)//Intha moonu line than oru form lendhu adutha form ku poguradhuku use pandrom
        {
            UpdateDetails f3 = new UpdateDetails();
            this.Visible = false;//Intha form a close pannum
            f3.ShowDialog();//Adutha form a open pannum
        }
        //Ellathukum same work than
        private void button3_Click(object sender, EventArgs e)
        {
            Payment f4 = new Payment();
            this.Visible = false;
            f4.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)//Exit pandrathuku use pandrom
        {

            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddUser f5 = new AddUser();
            this.Visible = false;
            f5.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NotorietyList f6 = new NotorietyList();
            this.Visible = false;
            f6.ShowDialog();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)//Shortcuts
        {

            if ((e.KeyCode == Keys.D2) || (e.KeyCode == Keys.NumPad2))//Numpad la 2 illa keyboard la 2 press panna enna nadakum
            {
                button1.PerformClick();
            }

            if ((e.KeyCode == Keys.D3) || (e.KeyCode == Keys.NumPad3))//Numpad la 3 illa keyboard la 3 press panna enna nadakum
            {
                button2.PerformClick();
            }

            if ((e.KeyCode == Keys.D4) || (e.KeyCode == Keys.NumPad4))//Numpad la 4 illa keyboard la 4 press panna enna nadakum
            {
                button3.PerformClick();
            }

            if ((e.KeyCode == Keys.D1) || (e.KeyCode == Keys.NumPad1))//Numpad la 1 illa keyboard la 1 press panna enna nadakum
            {
                button5.PerformClick();
            }

            if (e.KeyCode == Keys.Escape)
            {
                button4.PerformClick();
            }
        }


    }
}
