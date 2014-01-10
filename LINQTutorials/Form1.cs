using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQTutorials
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create our database of employees.
            var employees = new List<Employee>
                {
                    new Employee
                        {
                            FirstName = "Joe",
                            LastName = "Bob",
                            Salary = 94000,
                            StartDate = DateTime.Parse("1/4/1992")
                        },
                    new Employee
                        {
                            FirstName = "Jane",
                            LastName = "Doe",
                            Salary = 123000,
                            StartDate = DateTime.Parse("4/12/1998")
                        },
                    new Employee
                        {
                            FirstName = "Milton",
                            LastName = "Waddams",
                            Salary = 1000000,
                            StartDate = DateTime.Parse("12/3/1969")
                        }
                };
        }
    }
}
