using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinmaluNames
{
    public partial class SubNames : Form
    {
        private Names names;
        private int number;

        public SubNames(Names names, int number)
        {
            InitializeComponent();
            this.names = names;
            this.number = number;
            switch (number)
            {
                case 4:
                    Label3.Visible = false;
                    Text2.Visible = false;
                    Label1.Text = "이름앞에 입력된 문자열을 붙여줍니다.";
                    Label2.Text = "앞이름";
                    break;
                case 5:
                    Label3.Visible = false;
                    Text2.Visible = false;
                    Label1.Text = "이름뒤에 입력된 문자열을 붙여줍니다.";
                    Label2.Text = "뒷이름";
                    break;
                case 6:
                    Label1.Text = "시작수부터 자리수에 맞게 숫자를 붙여줍니다.";
                    Label2.Text = "자리수";
                    Label3.Text = "시작수";
                    break;
            }
        }

        private void SubNames_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyData)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    Btn1_Click(null, null);
                    break;
            }
        }
        
        private void Btn1_Click(object sender, EventArgs e)
        {
            switch(number)
            {
                case 4:
                    names.addPrefixName(Text1.Text);
                    break;
                case 5:
                    names.addSuffixName(Text1.Text);
                    break;
                case 6:
                    try
                    {
                        int number1 = int.Parse(Text1.Text);
                        int number2 = int.Parse(Text2.Text);
                        names.addNumberName(number1, number2);
                    }
                    catch
                    {
                        MessageBox.Show("숫자가 입력되지 않았습니다.");
                        return;
                    }
                    break;
            }
            Close();
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
