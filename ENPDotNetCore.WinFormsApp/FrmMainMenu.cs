using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENPDotNetCore.WinFormsApp
{
    public partial class FrmMainMenu : Form
    {
        public FrmMainMenu()
        {
            InitializeComponent();
        }

        private void newBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBlog frm = new FrmBlog();
            //frm.ShowDialog(); // the current form need to close, if so other form can be used
            frm.Show();// can use another form
        }

        private void blogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmBlog frm = new FrmBlog();
            //frm.Show();// can click another form
            frm.ShowDialog();//if so the current form close, the other form can be click
        }

        private void blogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBlogList frm = new FrmBlogList();
            frm.ShowDialog();
        }
    }
}
