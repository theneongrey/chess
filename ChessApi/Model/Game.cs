using System.ComponentModel.DataAnnotations.Schema;

namespace ChessApi.Model
{
    public class Game
    {
        public Guid Id { get; set; }
        public string? FullAllGebraicNotationState { get; set; }

        public User Player1 { get; set; }
        public User Player2 { get; set; }
    }
}
