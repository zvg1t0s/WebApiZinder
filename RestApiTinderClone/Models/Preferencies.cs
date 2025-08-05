using System.ComponentModel.DataAnnotations;

namespace RestApiTinderClone.Models
{
    public class Preferencies
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public int MinimalAge { get; set; }

        public int MaximalAge { get; set; }
        [Required]
        public bool Sex {  get; set; }

        public double Range { get; set; } = 10000;

    }
}
