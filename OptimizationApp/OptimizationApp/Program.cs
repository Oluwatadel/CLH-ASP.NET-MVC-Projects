// See https://aka.ms/new-console-template for more information
using OptimizationApp.Infra;
using OptimizationApp.Model;
using System.Diagnostics;

Random rand = new Random();
var context = new ApplicationDBContext();
//var batch = 0;
//for (int i=0; i<1000000; i++)
//{
//    var id= rand.Next();
//    var student = new Student
//    {
//        LastName = $"test_lastname_{id}",
//        FirstName = $"test_lastname_{id}",
//        MatricNo = id.ToString(),
//    };
//    context.Students.Add(student);
//    if(i%1000 == 0)
//    {
//        context.SaveChanges();
//        batch++;
//        Console.WriteLine($"Done uploading batch {batch}");
//    }
//}
//context.SaveChanges();

var stopWatch = new Stopwatch();
stopWatch.Start();
var lastNames = context.Students.Select(p=> new { p.LastName } ).Take(100000).ToList();

foreach(var name in lastNames)
{
    Console.WriteLine(name.LastName);
}
stopWatch.Stop();
Console.WriteLine($"Total time {stopWatch.ElapsedMilliseconds}");


