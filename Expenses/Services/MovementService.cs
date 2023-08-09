using Expenses.Models.Enums;
using Expenses.Models;
using System.Globalization;

namespace Expenses.Services
{
    public class MovementService
    {
        public ICollection<Movement> Upload()
        {
            string path = @"c:\temp\NU_87348844_01JUL2023_21JUL2023.csv";

            List<Movement> movements = new List<Movement>();

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
                        Movement exp = new Movement(count, description, date, value, identifier);
                        sum += value;
                        count++;
                        movements.Add(exp);
                    }
                }
            }
            catch (IOException e)
            {

            }
            return movements;
        }
    }
}
