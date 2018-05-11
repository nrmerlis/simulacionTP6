using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simulacion_odontologia_tp6
{
    public partial class Form1 : Form
    {

       

        double t;
        double tpll;
        
        double tf;

        double ia;

        int cpa;

        double[] tc;

        double[] pec;
        double[] pto;
        double[] ppa;

        double[] sto;
        double[] ste;

        int[] nt;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private void init(int n)
        {
            t = 0;
            tpll = 0;
            tf = 10;

            cpa = 0;

            tc = new double[n];
            pec = new double[n];
            pto = new double[n];
            ppa = new double[n];

            sto = new double[n];
            ste = new double[n];

            nt = new int[n];

            for (int i=0;i<n;i++)
            {
                tc[i] = 0;
                pec[i] = 0;
                pto[i] = 0;
                ppa[i] = 0;

                sto[i] = 0;
                ste[i] = 0;

                nt[i] = 0;
            }
            
        }

        private double getIa(double r)
        {
            double y= ((19.0 - 4.6186) * Math.Sqrt(r)) + 4.6186;
            if (y>=5 && y<=19)
            {
                return y;
            }
          return getIa(r);
        }
        private double getTaC(double r)
        {
            double y = ((39.399 - 29.495) *r) + 29.495;
            if (y >= 30 && y <= 40)
            {
                return double.Parse(y.ToString("0.00"));
            }
            return getTaC(r);
        }
        private double getTaA(double r)
        {
            double y = ((59.495 - 54.547) * r) + 54.547;
            if (y >= 55 && y <= 60)
            {
                return double.Parse(y.ToString("0.00"));
            }
            return getTaA(r);
        }
        //Devuelve random entre 0 y 1
        private double getRandom()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            double a = rand.NextDouble();
            return double.Parse(rand.NextDouble().ToString("0.00"));
        }
        public bool surrended()
        {
            bool a = getRandom() <= 0.2 ? true  : false;
            if (a)
            {
                cpa++;
            }
            return a;
        }
        public void startSimulation(int n)
        {
            init(n);

            int i;
            double r;
            double tac=0;
            double taa=0;
            do
            {
                t = tpll;
                ia = getIa(getRandom());
                tpll = t + ia;
                i = tc.ToList<Double>().IndexOf(tc.Min());
                if (!surrended())
                {
                    r = getRandom();
                    if (r<=0.65)
                    {
                        tac = getTaC(getRandom());
                        if(t<=tc[i])
                        {
                            tc[i] = t + tac;
                            sto[i] = sto[i] + (t - tc[i]);
                            
                        }
                        else
                        {
                            tc[i] = tc[i] + tac;
                            ste[i] = ste[i] + (tc[i]-t);
                            
                        }
                        nt[i] += 1;
                    }
                    else
                    {
                        taa = getTaA(getRandom());
                        if (t <= tc[i])
                        {
                            tc[i] = t + taa;
                            sto[i] = sto[i] + (t - tc[i]);
                            
                        }
                        else
                        {
                            tc[i] = tc[i] + taa;
                            ste[i] = ste[i] + (tc[i] - t);
                            
                        }
                        nt[i] += 1;
                    }
                }
            } while (t <= tf);
            for (int c=0;c<n;c++)
            {
                pec[i] = ste[i] / nt[i];
                pto[i] = (sto[i] * 100) / t;
                ppa[i] = (ppa[i] * 100) / nt[i];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            startSimulation(int.Parse(numericUpDown1.Value.ToString()));

            for (int c = 0; c < int.Parse(numericUpDown1.Value.ToString()); c++)
            {
            Console.WriteLine("Pec["+c+"]="+pec[c]);
            Console.WriteLine("Pto["+c+"]="+pto[c]);
            Console.WriteLine("Ppa["+c+"]="+ppa[c]);
            }
            
        }
    }
}
