using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace LearningDesctopApplication
{
    public partial class ReceiptGenerator : Window
    {
        public ReceiptGenerator()
        {
            InitializeComponent();
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "Cash_Deposit_Receipt.pdf";

            Document doc = new Document();
            try
            {
                PdfWriter.GetInstance(doc, new FileStream("PDF/" + filePath, FileMode.Create));
                doc.Open();

                string logoPath = "BOC.jpg";
                if (File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleToFit(200f, 60f);
                    logo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(logo);
                }

                Paragraph bankName = new Paragraph("BANK OF CEYLON\n", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
                bankName.Alignment = Element.ALIGN_CENTER;
                doc.Add(bankName);

                Paragraph details = new Paragraph(
                    "DATE: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\n" +
                    "LOCATION: Colombo\n" +
                    "RECORD NO: 1411\n" +
                    "--------------------------------------------\n",
                    new Font(Font.FontFamily.HELVETICA, 10));
                details.Alignment = Element.ALIGN_LEFT;
                doc.Add(details);
            
                string accountNumber = txtAccountNumber.Text; 
                string accountHolder = txtAccountHolder.Text; 
                string depositorName = txtDepositorName.Text; 
                string contactNumber = txtContactNumber.Text; 
                string depositAmount = txtDepositAmount.Text;

                Paragraph transactionDetails = new Paragraph(
                    $"Account Number: {accountNumber}\n" +
                    $"Account Holder: {accountHolder}\n" +
                    $"Depositor Name: {depositorName}\n" +
                    $"Contact Number: {contactNumber}\n" +
                    $"Deposit Amount: {depositAmount}\n" +
                    "--------------------------------------------\n" +
                    "PLEASE RETAIN RECEIPT FOR YOUR RECORDS\n",
                    new Font(Font.FontFamily.HELVETICA, 10));
                doc.Add(transactionDetails);

                Paragraph footer = new Paragraph(
                    "THANKS FOR USING OUR ATM\n" +
                    "PLEASE ACTIVATE BOC DEBIT CARD FOR INTERNATIONAL USE PRIOR TO DEPARTURE\n" +
                    "CONTACT CALL CENTRE: 94 112204444",
                    new Font(Font.FontFamily.HELVETICA, 9, Font.ITALIC));
                footer.Alignment = Element.ALIGN_CENTER;
                doc.Add(footer);

                LoggingUtility.LogResult("Recipt","Receipt PDF Generated Successfully!");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogException(ex);
            }
            finally
            {
                doc.Close();
            }
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            txtAccountNumber.Text = "";
            txtAccountHolder.Text = "";
            txtDepositorName.Text = "";
            txtContactNumber.Text = "";
            txtDepositAmount.Text = "";
        }
    }
}