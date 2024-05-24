using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

string jsonStr = await File.ReadAllTextAsync("Birds.json");
var model = JsonConvert.DeserializeObject<MainDto>(jsonStr);

foreach(var item in model.Tbl_Bird)
{
    Console.WriteLine(item.BirdMyanmarName);
}
//Console.WriteLine(jsonStr);
Console.ReadLine();
//Json to C# ?? Newtonsoft.json
public class MainDto
{
    public Bird[] Tbl_Bird { get; set; }
}

public class Bird
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}
