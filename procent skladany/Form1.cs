using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        

        public class ZmienneGlobalne
        {
            public static int wplata, lat, kapitalizacje;
            public static double procent, oprocentowanie;
        }
        
        public Form1()
        {
            InitializeComponent();
        }

       

        
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label5.Text = Convert.ToString(hScrollBar1.Value);
            ZmienneGlobalne.wplata=hScrollBar1.Value;
            
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            label6.Text = Convert.ToString(hScrollBar2.Value*0.01);
            ZmienneGlobalne.oprocentowanie=hScrollBar2.Value*0.01;
            
            
        }

        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            label7.Text = Convert.ToString(hScrollBar3.Value);
            ZmienneGlobalne.lat = hScrollBar3.Value;
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label8.Text = Convert.ToString(comboBox1.SelectedItem);
            ZmienneGlobalne.kapitalizacje =Convert.ToInt32(comboBox1.Text) ; 
             
        }

        


        private void button1_Click(object sender, EventArgs e)
        {
            ZmienneGlobalne.procent=ZmienneGlobalne.oprocentowanie/ZmienneGlobalne.kapitalizacje;
            label10.Text = Convert.ToString(ZmienneGlobalne.wplata*((Math.Pow(1+ZmienneGlobalne.procent,ZmienneGlobalne.lat*ZmienneGlobalne.kapitalizacje)-1)/ZmienneGlobalne.procent)*(1+ZmienneGlobalne.procent));
            
            CreateGraph(zedGraphControl1);
            zedGraphControl1.Refresh();
            
        }


       

        // Respond to form 'Load' event

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the graph

            CreateGraph(zedGraphControl1);
       
        }

        // Build the Chart

        private void CreateGraph(ZedGraphControl zgc)
        {
            // get a reference to the GraphPane

            GraphPane myPane = zgc.GraphPane;

            // Set the Titles

            myPane.Title.Text = "Wykres prezentujący wzrost kapitału";
            myPane.XAxis.Title.Text = "oś lat";
            myPane.YAxis.Title.Text = "oś kapitału w złotych";

            // Make up some data arrays based on the Sine function

            double x, y1;
            PointPairList list1 = new PointPairList();
            for (int i = 0; i <= 20; i++)
            {
                x = i ;
                y1 = ZmienneGlobalne.wplata * ((Math.Pow(1 + ZmienneGlobalne.procent, x * ZmienneGlobalne.kapitalizacje) - 1) / ZmienneGlobalne.procent) * (1 + ZmienneGlobalne.procent);
                list1.Add(x, y1);
                x = i + 1 ;
            }

            // Generate a red curve with diamond

            // symbols, and "Porsche" in the legend

            LineItem myCurve = myPane.AddCurve("Twój kapitał po n lat",
                  list1, Color.Red, SymbolType.Diamond);

            // Tell ZedGraph to refigure the

            // axes since the data have changed

            zgc.AxisChange();
        }


        




    }
}
