using PrinterUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace POSPrintExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var ByterValue = obj.Separator();
            ByterValue = PrintExtensions.AddBytes(ByterValue, obj.CharSize.DoubleWidth6());
            ByterValue = PrintExtensions.AddBytes(ByterValue, Encoding.ASCII.GetBytes("Invoice No. : 12345\n"));
            ByterValue = PrintExtensions.AddBytes(ByterValue, "---------Thank You for Comming---------\n");



            ByterValue = PrintExtensions.AddBytes(ByterValue, obj.Separator());
            ByterValue = PrintExtensions.AddBytes(ByterValue, CutPage());

            // PrinterUtility.PrintExtensions.Print(ByterValue, POSPrintExample.Properties.Settings.Default.PrinterName);

            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", ByterValue);
            RawPrinterHelper.SendFileToPrinter(POSPrintExample.Properties.Settings.Default.Printer1, ".\\tmpPrint.print");
            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public byte[] CutPage()
        {
            List<byte> oby = new List<byte>();
            oby.Add(Convert.ToByte(Convert.ToChar(0x1D)));
            oby.Add(Convert.ToByte('V'));
            oby.Add((byte)66);
            oby.Add((byte)3);
            return oby.ToArray();
        }
    }
}
