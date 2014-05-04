using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateMethodPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            PdfExport obj = new PdfExport();
            obj.ExportData();

            ExcelExport obj1 = new ExcelExport();
            obj1.ExportData();
            Console.ReadKey();
        }
    }
    public abstract class Export
    {
        //Making the methods virtual makes the Template pattern more flexible, in that Virtual methods can be overridden.
        //However for making a stricter implementation of the Template method, these methods should be made non virtual
        //This is also called NVI pattern
        public virtual void ExportData()
        {
            //Allows child classes to provide a different implementation of the alogorithm.
            //For e.g. GetData and Export Data are implemented differently in different child classes
            GetData();
            LoadData();
            ExportSpecData();
        }
        public virtual void GetData()
        {
            Console.WriteLine("Getting data from abstract class Export");
        }
        public void LoadData()
        {
            Console.WriteLine("Loading data from abstract class Export");
        }

        public abstract void ExportSpecData();

    }
    public class PdfExport:Export
    {
        public override void GetData()
        {
            Console.WriteLine("Getting data from the child pdfExport class");
        }
        
        public override void ExportSpecData()
        {
            Console.WriteLine("Exporting data to a pdf file from the child pdfExport class");
        }
    }
    public class ExcelExport:Export
    {
        public override void ExportSpecData()
        {
            Console.WriteLine("Exporting data to an excel file from the child ExcelExport class");
        }
    }
}
