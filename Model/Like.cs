namespace WardrobeBackendd.Model;

public class Like
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CombineId { get; set; }
    public DateTime CreatedAt { get; set; }
}