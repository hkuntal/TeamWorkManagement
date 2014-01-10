using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiscellenaousRnD.InterFaceTesting;
using log4net;
using System.IO;

namespace MiscellenaousRnD
{
    public partial class Form1 : Form
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(Form1));
        private static readonly ILog log = LogManager.GetLogger("Hariom1");
        public Form1()
        {
            InitializeComponent();
            //XmlConfigurator.Configure();
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.xml"));
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Create a set of objects first

            //Test the Hashset example
            //HashSet<int> set = new HashSet<int>();
            //set.Add();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InterFaceTesting.TestClass obj = new TestClass();
            InterFaceTesting.I1 obj1 = (I1) obj;
            
            obj.M1();
            obj.M2();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnLog4Net_Click(object sender, EventArgs e)
        {
            OOPSConcepts.Program.Main(new string[1]);
            MessageBox.Show(OOPSConcepts.Program.DoSomething());
            log.Info("Entering application.");
            log.Error("Trying a sample Error");
            log.Info("Exiting application.");
        }
    }
}
