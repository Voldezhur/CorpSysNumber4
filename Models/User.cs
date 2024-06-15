using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Practice4.Models;

public class User
{
    [Key]
    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string name { get; set; }
}