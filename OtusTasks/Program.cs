// See https://aka.ms/new-console-template for more information
//Parallel

using OtusTasks;
using System.Diagnostics;

var stopwatch = Stopwatch.StartNew();
var dir = "../../../";
//var dir = "p:\\axes\\";

var dct = SpaceFinder.GetCharCountByFile(dir, "*.txt");

Console.WriteLine($"Elapsed time = {stopwatch.ElapsedMilliseconds} ms"); // таски

stopwatch = Stopwatch.StartNew();
var dct2 = SpaceFinder.GetCharCountByFileWithoutTask(dir, "*.txt");
stopwatch.Stop();
Console.WriteLine($"Elapsed time = {stopwatch.ElapsedMilliseconds} ms"); // без тасков

Console.WriteLine("---");

//foreach (var file in dct)
//{
//    Console.WriteLine(file);
//}





