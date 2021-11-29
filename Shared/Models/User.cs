namespace SETraining.Shared.Models;

public abstract class User
{
    public User(string name)
    {
        Name = name; 
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Content>? Contents { get; set; } = null!;
    public ICollection<HistoryEntry>? History { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public ICollection<Content>? Favorites { get; set; }

}