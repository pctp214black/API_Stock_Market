namespace API.Models;

public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; }="";
    public string Content { get; set; }="";
    public DateTime CreatedOn { get; set; }=DateTime.Now;

    //El símbolo ? se utiliza para indicar que un tipo de dato puede ser nullable (nulo). 
    //Puede contener un valor o puede ser null.
    public int? StockId { get; set; }
    public Stock? Stock { get; set; }

}