using System.ComponentModel.DataAnnotations;

namespace RestApiTinderClone.Models
{
    public class UserPhoto
    {
        [Key]
        public int Id { get; set; }

        public int UserId {  get; set; } 

        public List<string> ImageAddresses { get; set; } = [];
    }
}
