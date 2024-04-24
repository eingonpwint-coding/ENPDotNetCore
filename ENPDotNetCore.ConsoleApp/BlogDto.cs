using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENPDotNetCore.ConsoleApp;

//need to map C# object and database 
[Table("Tbl_Blog")]
public class BlogDto
{
    //need to assign primary key 
    [Key]
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
    public string BlogAuthor { get; set;}
    public string BlogContent { get; set;}

}
