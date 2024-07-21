using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace System.Interview.TinyUrl.ByHash.DataLayer;

[Index(nameof(Hash), IsUnique = true)]
public class UrlLinkEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(4)]
    public string Hash { get; set; }

    public string Url { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}