using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsAppGlobalClient.ServiceReferenceHoliday;

namespace WindowsAppGlobalClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetService_Click(object sender, EventArgs e)
        {
            HolidaySoapClient webService = new HolidaySoapClient();

            Holiday[] feriados = webService.GetAllHolidays((int)numericUpDown1.Value);

            listBox1.Items.Clear();

            foreach (Holiday item in feriados)
            {
                listBox1.Items.Add(item.Date.ToShortDateString() + " : " + item.Name + " : " + item.Type + " : " + item.Description);
            }

            webService.Close();
        }
    }
}
