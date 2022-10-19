namespace Maistas.Models;

public class Restaurant : User
{
    public string Title { get; set; }
    public string PhoneNumber { get; set; }
    public string Website { get; set; }
    public DateTime WorkTime { get; set; }
    public double MinimumOrderPrice { get; set; }
    
    
}