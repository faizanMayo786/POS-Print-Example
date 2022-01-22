using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace POSPrintExample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            Console.WriteLine(args);
            string Parameter = args[0].ToLower().ToString().Replace(POSPrintExample.Properties.Settings.Default.ReplaceText.ToLower(), "");
            string Ext = POSPrintExample.Properties.Settings.Default.PostExtn;

            MessageBox.Show(Parameter);
            string url = POSPrintExample.Properties.Settings.Default.UrlPost + "/" + Parameter + "." + Ext;
            MessageBox.Show(url);
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            string result;

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            RawPrinterHelper.SendStringToPrinter(POSPrintExample.Properties.Settings.Default.PrinterName, Encoding.ASCII.GetBytes(result).ToString());


        }
    }
}
