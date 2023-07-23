using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotmail_Change_V1._0
{
    public partial class Form2 : Form
    {
        public Form2(string key)
        {
            InitializeComponent();
            textBox1.Text = key;
        }

        private void button1_Click(object sender, EventArgs e)
        {
             
            try
            {
                Clipboard.SetText(textBox1.Text);
                MessageBox.Show("Đã copy key thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }
            catch
            {
                MessageBox.Show("copy key không thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
