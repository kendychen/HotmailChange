using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotmail_Change_V1;
namespace Hotmail_Change_V1._0
{
    public partial class TickByStatus : Form
    {
        public TickByStatus()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ConfigInfo.keyword = textBoxKeyword.Text;
            this.Hide();
        }
    }
}
