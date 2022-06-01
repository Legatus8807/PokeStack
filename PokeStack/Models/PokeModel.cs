using System.ComponentModel.DataAnnotations;

namespace PokeStack.Models
{
    public class PokeModel
    {
        [Key]
        public long Id { get; set; }

        public string pokeName { get; set; } = string.Empty;

        public string type1 { get; set; } = string.Empty;

        public string? type2 { get; set; }

        public int hp { get; set; }

        public int atk { get; set; }

        public int def { get; set; }

        public int spA { get; set; }

        public int spD { get; set; }

        public int spe { get; set; }

        public string? imageUrl { get; set; }
    }
}
