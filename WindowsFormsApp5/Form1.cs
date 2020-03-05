using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {

        double W3 = 0;
        double W = 0;
        double d = 260;
        double n = 11;
        double E = 210000;
        double h = 10;
        double Gt = 450;
        double t = 275;
        double xi0 = 0;
        double Nset = 0;
        double C2a = 0;
        double stepW = 0.001;
        double Ra;
        int i=0;
        List<List<double>> result= new List<List<double>>();
        
        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }.

        double Calculation()
        {
            List<double> res = new List<double>();
            
            double[] array = { Linear(i), Parabolic(), Exponential(), Sinusoidal() };
            for (int j = 0; j < 4; j++)
            {
                for (int i = 5; i < 10; i += 2)
                {
                    switch (j)
                    {
                        case 0: res.Add(Linear(i));
                            MessageBox.Show(Linear(i).ToString());
                            break;
                            
                    }
                    
                    
                }
                result.Add(res);
            }
            
   
            return 0;
        }
        double Linear(int i)
        {
            //RichTextBox[] arr = { richTextBox2, richTextBox3, richTextBox4 };
            //int index = 0;
            //List<double> res = new List<double>();
            //for (int i = 5; i < 10; i += 2)
            //{

                double tgA = W3 / (4 * t);
                W = Math.Round(W3 - (((i - 1) / 2) * t * tgA), 3);
            //arr[index].Text = W.ToString();

            //index++;
            //    res.Add(W);
            //}
            //result.Add(res);
            MessageBox.Show(W.ToString());
            return W;
            
        }

        double Parabolic()
        {
            double a;
            for (a = 0.1; a < 0.6; a+=0.1)
            {
                W = Math.Round(W3 - (a * Math.Pow((i - 1) / 2, 2)), 3);
                
            }
            return 0;
        }

        double Exponential()
        {
            double a = Math.Log(W3) / (n - 7);

            W = Math.Round(W3 - Math.Exp(a * (i - 3) / 2), 3);
            return 0;
        }

        double Sinusoidal()
        {
            int a;
            for (a = 10; a < 21; a++)
            {
                double b = Math.Asin(W3 / a) / (n - 7);
                W = Math.Round(W3 - a * Math.Sin(b * (i - 1) / 2), 3);
            }
            return 0;
        }



        private void Button1_Click(object sender, EventArgs e)

        {

            Calculation();
             Nset = E * h / Gt * xi0 / 1000;

            if (Nset > 1)
            {
                double Cupr = Gt * Math.Pow(t, 2) / E / h;
                double Rupr = (Math.Pow(Cupr, 2) + Math.Pow(t, 2) / 4) / 2 / Cupr;
                do
                {
                    C2a += stepW;
                    Ra = (Math.Pow(C2a, 2) + Math.Pow(t, 2) / 4) / 2 / C2a;


                }
                while (Ra > Rupr / Nset);
            }
            else
            {
                C2a = Gt * Math.Pow(t, 2) / 12 / E / h;
            }
            richTextBox1.Text += Math.Round(C2a,3).ToString();
            W3 = C2a;
          

        }
        
        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
