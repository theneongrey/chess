using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChessApi.Model
{
    [Index(nameof(Name), IsUnique = true)]
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
