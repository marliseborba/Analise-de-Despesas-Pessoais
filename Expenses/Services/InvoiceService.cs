using Expenses.Models;
using Expenses.Models.Enums;
using System.Globalization;

namespace Expenses.Services
{
    public class InvoiceService
    {
        public static Invoice Upload()
        {
            string path = @"c:\temp\NU_87348844_01JUL2023_21JUL2023.csv";
            Invoice invoice = new Invoice();

            List<Expense> expenses = new List<Expense>();

            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    int count = 0;
                    double sum = 0;
                    string headerLine = sr.ReadLine();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] attributes = line.Split(",");
                        DateTime date = DateTime.ParseExact(attributes[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        double value = double.Parse(attributes[1], CultureInfo.InvariantCulture);
                        string identifier = attributes[2];
                        string description = attributes[3];
                        Expense exp = new Expense(count, description, date, value, identifier, invoice);
                        sum += value;
                        count++;
                        expenses.Add(exp);
                    }
                    invoice.Id = 1;
                    invoice.DtInitial = new DateTime(2023, 07, 01);
                    invoice.DtFinal = new DateTime(2023, 07, 30);
                    invoice.Total = sum;
                    invoice.Expenses = expenses;
                    invoice.InvoiceType = InvoiceType.CREDIT;
                    invoice.InvoiceStatus = InvoiceStatus.PAID;
                }
            } 
            catch (IOException e)
            { 
            
            }
            return invoice;
        }
    }
}
