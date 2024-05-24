using System.ComponentModel.DataAnnotations;
using _3._Data.Models;

namespace _3._Data;

public class Order : BaseModel
{
    [Required]
    public int Amount {get;set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
}