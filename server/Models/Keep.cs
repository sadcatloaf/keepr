using System.ComponentModel.DataAnnotations;

namespace keepr.Models;


public class Keep
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public int Views { get; set; }
    public string CreatorId { get; set; }
    public Account Creator { get; set; }
    public int Kept { get; set; }
    public int VaultKeepId { get; set; }
}

// = DateTime.UtcNow; --> formatted datetime



