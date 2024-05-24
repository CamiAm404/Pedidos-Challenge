namespace _3._Data.Models;

public class Client : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Boolean IsDeleted { get; set; }

    public List<Order> Orders { get; set; }
}