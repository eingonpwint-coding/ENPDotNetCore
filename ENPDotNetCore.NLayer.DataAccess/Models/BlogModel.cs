using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENPDotNetCore.NLayer.DataAccess.Models;

//need to map C# object and database 
[Table("Tbl_Blog")]
public class BlogModel
{
    //need to assign primary key 
    //if not include ?, not even go to the break point
    //bcz of not null in the database
    [Key]
    public int BlogId { get; set; }

    public string? BlogTitle { get; set; }

    public string? BlogAuthor { get; set; }

    public string? BlogContent { get; set; }

}

public struct BlogStruct
{
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
}

public record BlogEntity(int BlogId, string BlogTitle, string BlogAuthor, string BlogContent);
