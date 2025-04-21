namespace WardrobeBackendd.Model;

public class Combine
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public int IsFavorite { get; set; } 
    public int IsPublic { get; set; } 

}