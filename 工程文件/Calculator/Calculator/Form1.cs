using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        StringBuilder intxt = new StringBuilder(10);
        StringBuilder outtxt = new StringBuilder(10);

        StringBuilder inputText = new StringBuilder(10);


        [DllImport("fb3-3.tab.dll", EntryPoint = "Calculation", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Calculation(StringBuilder xintxt, StringBuilder xouttxt);



        public Form1()
        {
            InitializeComponent();
            intxt.Append(".\\in.txt");
            outtxt.Append(".\\out.txt");

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void IN_Click(object sender, EventArgs e)
        {

        }


        private void button17_Click(object sender, EventArgs e)
        {
            //删除一个字符
            if (inputText.Length>0)
            {
                inputText.Remove((inputText.Length - 1), 1);
                INPUT.Text = inputText.ToString();
            }

        }

        private void button19_Click(object sender, EventArgs e)
        {
            //写字符串
            if (!File.Exists(".\\in.txt"))
            {
                FileStream fs1 = new FileStream(".\\in.txt", FileMode.Create, FileAccess.Write);//创建写入文件

                inputText.Append("\r\n");
                StreamWriter sw = new StreamWriter(fs1);
                sw.Write(inputText);
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(".\\in.txt", FileMode.Open);
                inputText.Append("\r\n");
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(inputText);
                sw.Close();
                fs.Close();
            }


            //计算
            Calculation(intxt, outtxt);

            //显示
            string outstr = File.ReadAllText(".\\out.txt");
            OUT.Text = outstr;
        }





        private void 历史记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void INPUT_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            inputText.Append("1");
            INPUT.Text = inputText.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputText.Append("2");
            INPUT.Text = inputText.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            inputText.Append("3");
            INPUT.Text = inputText.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            inputText.Append("4");
            INPUT.Text = inputText.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            inputText.Append("5");
            INPUT.Text = inputText.ToString();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            inputText.Append("6");
            INPUT.Text = inputText.ToString();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            inputText.Append("7");
            INPUT.Text = inputText.ToString();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            inputText.Append("8");
            INPUT.Text = inputText.ToString();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            inputText.Append("9");
            INPUT.Text = inputText.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            inputText.Append("0");
            INPUT.Text = inputText.ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            inputText.Append("+");
            INPUT.Text = inputText.ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            inputText.Append("-");
            INPUT.Text = inputText.ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            inputText.Append("*");
            INPUT.Text = inputText.ToString();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            inputText.Append("/");
            INPUT.Text = inputText.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            inputText.Append(".");
            INPUT.Text = inputText.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            inputText.Append(";");
            INPUT.Text = inputText.ToString();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            inputText.Append("(");
            INPUT.Text = inputText.ToString();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            inputText.Append(")");
            INPUT.Text = inputText.ToString();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            inputText.Append("{");
            INPUT.Text = inputText.ToString();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            inputText.Append("}");
            INPUT.Text = inputText.ToString();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            inputText.Append("\r\n");
            INPUT.Text = inputText.ToString();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            //导入当前显示文字
            inputText.Clear();
            inputText.Append(INPUT.Text);
            
        }

        private void button18_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(".\\in.txt", FileMode.Open);
            fs.SetLength(0);
            inputText.Clear();
            INPUT.Text = "";
            fs.Close();

        }

        private void 计算器_Load(object sender, EventArgs e)
        {

        }
    }
}
