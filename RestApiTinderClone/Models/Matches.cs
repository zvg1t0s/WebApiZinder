using System.ComponentModel.DataAnnotations;

namespace RestApiTinderClone.Models
{
    public class Matches
    {
        [Key]
        public int Id { get; set; }

        public int LeftId {  get; set; }

        public int RightId { get; set; }

        public bool LeftDecision { get; set; } = false;

        public bool RightDecision { get; set; } = false;
    }
}
