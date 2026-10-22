using System.ComponentModel.DataAnnotations;

namespace InternTrack.Models;

public class InternshipApplication
{
    [Key]
public int ApplicationId { get; set; }

    [Required, StringLength(100)]
    public string CompanyName { get; set; } = "";

    [Required, StringLength(150)]
    public string JobRole { get; set; } = "";

    [Url]
    public string? ApplicationUrl { get; set; }

    public string? Location { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime ApplicationDate { get; set; } = DateTime.Today;

    [DataType(DataType.Date)]
    public DateTime? Deadline { get; set; }

    [Required]
    public string Status { get; set; } = "Saved";

    public string WorkMode { get; set; } = "Remote";

    [Range(0, 10000000)]
    public decimal? Stipend { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}