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

namespace WindowsFormsApplication1
{
    public partial class Print : Form
    {
        List<Fine> list =new List<Fine>();
        int usid,slno=1,fbil=1;
        string vhno,billno,name,date,total,paid,balance;
        public string Vehino//oru form lendhu vera form la receive pandrathuku use pandrom
        {
            set { vhno = value; }
        }
        public Print()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
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
                    cmd.CommandText = "select count(usrid) as cnt from bill where usrid=" + usid + ";";
                    read = cmd.ExecuteReader();
                    while (read.Read())//while the data doesnt become empty
                    {
                        if (read.GetInt32("cnt") == 1) { fbil = 0; }
                        else { fbil = 1; }
                    }
                    read.Close();
                    cmd.CommandText = "select * from owndet,fines,bill where owndet.usrid=" + usid + " order by billno desc limit 1;";
                    read = cmd.ExecuteReader();
                    if (!read.HasRows) { MessageBox.Show("details not found"); }
                    while (read.Read())//while the data doesnt become empty
                    {//Bill date,no,name etc inga label la setting pandrom

                        name = read.GetString("name");
                        billno = read.GetInt32("billno").ToString();
                        date = read.GetDateTime("date").ToString();
                        total = read.GetInt32("total").ToString();
                        paid = read.GetInt32("paid").ToString();
                        balance = (read.GetInt32("total") - read.GetInt32("paid")).ToString();
                        Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]{
                            new Microsoft.Reporting.WinForms.ReportParameter("Name",name),
                            new Microsoft.Reporting.WinForms.ReportParameter("Date",date),
                            new Microsoft.Reporting.WinForms.ReportParameter("Billno",billno),
                            new Microsoft.Reporting.WinForms.ReportParameter("vehino",vhno),
                            new Microsoft.Reporting.WinForms.ReportParameter("Total",total),
                            new Microsoft.Reporting.WinForms.ReportParameter("Paid",paid),
                            new Microsoft.Reporting.WinForms.ReportParameter("Balance",balance),
                    };
                        reportViewer1.LocalReport.SetParameters(para);
                        if (fbil == 1)
                        {
                            if ((read.GetInt32("total") - read.GetInt32("paid")) != 0)
                            {
                                list.Add(new Fine() { Slno = slno, Offense = "Arrears", TotalAmt = (read.GetInt32("total") - read.GetInt32("paid")).ToString() });
                                slno++;
                            }
                        }
                        else
                        {
                            if (read.GetInt32("ot") > 0)//Overtake paniruntha,ithu run aagum
                            {
                                list.Add(new Fine() { Slno = slno, Offense = "OverTake", Times = read.GetInt32("ot"), FineAmt = 300, TotalAmt = (read.GetInt32("ot") * 300).ToString() });
                                slno++;
                            }
                            if (read.GetInt32("dd") > 0)//Drink and drive paniruntha
                            {
                                list.Add(new Fine() { Slno = slno, Offense = "Drink and Drive", Times = read.GetInt32("dd"), FineAmt = 500, TotalAmt = (read.GetInt32("dd") * 500).ToString() });
                                slno++;
                            }
                            if (read.GetInt32("ht") > 0)
                            {
                                list.Add(new Fine() { Slno = slno, Offense = "Helmet", Times = read.GetInt32("ht"), FineAmt = 100, TotalAmt = (read.GetInt32("ht") * 100).ToString() });
                                slno++;
                            }
                            if (read.GetInt32("ow") > 0)
                            {
                                list.Add(new Fine() { Slno = slno, Offense = "One Way", Times = read.GetInt32("ow"), FineAmt = 200, TotalAmt = (read.GetInt32("ow") * 200).ToString() });
                                slno++;
                            }
                        }
                    }
                }
                FineBindingSource.DataSource = list;
                this.reportViewer1.RefreshReport();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("The error occured is" + ex);
                this.Close();
            }
        }

    }
}
