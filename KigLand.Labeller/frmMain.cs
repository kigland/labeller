using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KigLand.Labeller
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnColourLabeller_Click(object sender, EventArgs e)
        {
            var form = new frmColourLabeller();
            startForm(form);
        }

        private void startForm(Form form, bool closeAfterTheForm=true)
        {
            form.Show();
            this.Hide();
            if (closeAfterTheForm) {
                form.FormClosed += (o, e) =>
                {
                    this.Close();
                };
            }
        }
    }
}
