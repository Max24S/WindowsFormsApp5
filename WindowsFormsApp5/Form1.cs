using ClassLibraryRool;
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
        double h = 0; // 10
        double Gt = 0;    // 450
        double d = 0; // 260
        double t = 0;   // 275
        double xi0 = 0;  // 0
        int n = 0;  // 11
        double E = 210000;
        double Nset = 0;
        double C2a = 0;
        double stepW = 0.001;
        double Ra;
        double B = 2000;
        double vi = 0.3;
        double Lb = 1000.0;
        int index = 0;
       
        List<List<double>> result= new List<List<double>>();
        List<double> Xost = new List<double>();
        List<double> Aost = new List<double>();

        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void Calculation()
        {
          
            for (int j = 0; j < 4; j++)
            {
                List<double> res = new List<double>();
                for (int i = 5; i < 10; i += 2)
                {
                    switch (j)
                    {
                        case 0: res.Add(Linear(i));
                          
                            break;
                        case 1:
                            res.Add(Parabolic(i));
                          
                            break;
                        case 2:
                            res.Add(Exponential(i));
                         
                            break;
                        case 3:
                            res.Add(Sinusoidal(i));
                       
                            break;
                    }
                    
                    
                }
                result.Add(res);

                double [] array = {0, 0, W3, 0, res[0], 0, res[1], 0, res[2], 0, 0 };

                Pravka prav = new Pravka(n, d, h, B, t, Gt, E, vi, xi0, array);

                Xost.Add( prav._XiOst);

                Aost.Add (((1 / Xost[j]) - Math.Sqrt((Math.Pow((1.0 / Xost[j]), 2)) - ((Math.Pow(Lb, 2)) / 4))));

      

               
            }
            index = Xost.IndexOf(Xost.ToArray().Min());
            RichTextBox[] arr = { richTextBox2, richTextBox3, richTextBox4 };
            int Richindex = 0;
            foreach (double Elem in result[index]) 
            {
                arr[Richindex].Text = Elem.ToString();
            
                Richindex++;
            }

         
            richTextBox5.Text = Xost[index].ToString();



        }
        double Linear(int i)
        {
                double tgA = W3 / (4 * t);
                W = Math.Round(W3 - (((i - 1) / 2) * t * tgA), 3);
           
            return W;
        }

        double Parabolic(int i)
        {
            double a;
            for (a = 0.1; a < 0.6; a+=0.1)
            {
                W = Math.Round(W3 - (a * Math.Pow((i - 1) / 2, 2)), 3);
                
            }
            return W;
        }

        double Exponential(int i)
        {
            double a = Math.Log(W3) / (n - 7);

            W = Math.Round(W3 - Math.Exp(a * (i - 3) / 2), 3);
            return W;
        }

        double Sinusoidal(int i)
        {
            int a;
            for (a = 10; a < 21; a++)
            {
                double b = Math.Asin(W3 / a) / (n - 7);
                W = Math.Round(W3 - a * Math.Sin(b * (i - 1) / 2), 3);
            }
            return W;
        }
               
        private void Button1_Click(object sender, EventArgs e)

        {
            h= double.Parse(textBox1.Text);
            Gt = double.Parse(textBox2.Text);
            d = double.Parse(textBox3.Text);
            t = double.Parse(textBox4.Text);
            xi0 = double.Parse(textBox5.Text);
            n = int.Parse(textBox6.Text);

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
            Calculation();
                                
        }
                
        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
