using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice4.Models;

public class Message
{
    [Key]
    public int id { get; set; }
    [ForeignKey("User")]
    public int sender_id { get; set; }
    public string sender_name { get; set; }
    [ForeignKey("User")]
    public int recipient_id { get; set; }
    public string recipient_name { get; set; }
    public string header { get; set; }
    public string text { get; set; }
    [Column(TypeName = "timestamp")]
    public DateTime date_sent { get; set; }
    public int status { get; set; }

}