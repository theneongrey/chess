using System.ComponentModel.DataAnnotations.Schema;

namespace ChessApi.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
