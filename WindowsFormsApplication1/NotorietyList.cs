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
/* Intha form la most wanted list display pannuvom ,click panna avanoda details varum */
namespace WindowsFormsApplication1
{
    public partial class NotorietyList : Form
    {
        string vhno;
        public string vno//vandi no a vera form ku send pandrom
        {
            get { return vhno; }
        }
        MySqlDataAdapter notoriety;//Data vangurom
        DataSet DS = new DataSet();//Naraya type of data vangikom,array,linked list etc,inga namba datagrid view kaaga ulla data vangurom
        public NotorietyList()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)//Athu start aaga paathu ithu kulla irukura code work aagum
        {
            string serv = "localhost", database = "namefield", uid = "root", pasw = "121417181";
            string constring;
            constring = "SERVER=" + serv + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + pasw + ";";
            MySqlConnection connection = new MySqlConnection(constring);
            connection.Open();
            //Keela irukura line vechi than namba table a fill panna porom,first line la string a send pandrom
            notoriety = new MySqlDataAdapter("select owndet.name,vehdet.vhno,(ot+ht+dd+ow) as crimes from vehdet left outer join owndet on vehdet.usrid=owndet.usrid left outer join fines on vehdet.usrid=fines.usrid order by crimes desc;", constring);
            notoriety.Fill(DS);//Intha result a namba dataset la store pandrom
            dataGridView1.DataSource = DS.Tables[0];//Antha datagridview ku intha dataset la irukuradha set pandrom
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           vhno= dataGridView1.Rows[e.RowIndex].Cells["vhno"].Value.ToString();//Namba click panna row lendu ,vandi no mattum vaanguvom
           Fulldetails f8 = new Fulldetails();
           f8.Vehno = vno;//Vandi no a inga than adutha form ku send pandrom
           f8.ShowDialog();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Welcome f2 = new Welcome();
            this.Visible = false;
            f2.ShowDialog();
        }

        private void Form6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { button1.PerformClick(); }
        }
    }
}
