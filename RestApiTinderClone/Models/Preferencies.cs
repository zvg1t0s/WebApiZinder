using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestApiTinderClone.Models
{
    public class Preferencies
    {
        [Key]
        public int Id { get; set; }

        public User User {  get; set; }
        
        public int UserId { get; set; }

        public int MinimalAge { get; set; }

        public int MaximalAge { get; set; }
        
        public bool Sex {  get; set; }

        public double Range { get; set; } = 10000;

    }
}
