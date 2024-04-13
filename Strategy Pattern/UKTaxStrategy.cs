using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_Pattern
{
    public class UKTaxStrategy : ITaxStrategy
    {
        public double CalculateTax(double grossSalary)
        {
            var tax = (20 / 100) * grossSalary;
            return tax;
        }
    }
}
