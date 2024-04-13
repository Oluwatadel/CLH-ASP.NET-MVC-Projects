using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_Pattern
{
    public class NigeriaTaxRule : ITaxStrategy
    {
        public double CalculateTax(double grossSalary)
        {
            double tax = (10.0 / 100) * grossSalary;
            return tax;
        }
    }
}
