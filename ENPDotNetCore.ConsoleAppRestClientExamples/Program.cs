// See https://aka.ms/new-console-template for more information
using ENPDotNetCore.ConsoleAppRestClientExamples;

Console.WriteLine("Hello, World!");
RestClientExample rest = new RestClientExample();
await rest.RunAsync();