// See https://aka.ms/new-console-template for more information
using Strategy_Pattern;

Console.WriteLine("Hello, World!");

var staff = new Staff
{
    Country = "Nigeria",
    Salary = 50000
};

if(staff.Country == "Nigeria")
{
    double tax = new NigeriaTaxRule().CalculateTax(staff.Salary);
    Console.WriteLine(tax);
}
else if(staff.Country == "UK")
{
    double tax = new UKTaxStrategy().CalculateTax(staff.Salary);
    Console.WriteLine(tax);
}
