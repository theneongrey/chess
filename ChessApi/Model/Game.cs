using System.ComponentModel.DataAnnotations;

namespace ChessApi.Model
{
    public class Game
    {
        public Guid Id { get; set; }

        [Required] 
        public string? FullAllGebraicNotationState { get; set; }

        [Required]
        public User? Player1 { get; set; }

        [Required] 
        public User? Player2 { get; set; }
    }
}
