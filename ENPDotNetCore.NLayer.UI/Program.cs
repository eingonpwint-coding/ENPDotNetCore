// See https://aka.ms/new-console-template for more information
using ENPDotNetCore.NLayer.BusinessLogic.Services;
using ENPDotNetCore.NLayer.DataAccess.Models;
using Newtonsoft.Json;
using System.Xml;

Console.WriteLine("Hello, World!");

BL_BLog bL_BLog = new BL_BLog();
var lst = bL_BLog.GetBlogs();
Console.WriteLine(lst);

List<BlogEntity> blogEntities = lst.Select(x =>
        new BlogEntity(x.BlogId, x.BlogTitle!, x.BlogAuthor!, x.BlogContent!))
    .ToList();

var jsonStr = JsonConvert.SerializeObject(lst);
Console.WriteLine(jsonStr);
Console.WriteLine(blogEntities.ToString());
foreach (var blogEntity in blogEntities)
{
    Console.WriteLine(blogEntity);
}
foreach (var item in lst)
{
    Console.WriteLine(item);
}
Console.ReadLine();