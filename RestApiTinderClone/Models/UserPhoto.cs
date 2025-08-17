using System.ComponentModel.DataAnnotations;

namespace RestApiTinderClone.Models
{
    public class UserPhoto
    {
        [Key]
        public int Id { get; set; }

        public User Owner {  get; set; }

        public string ImageAddresses { get; set; }
    }
}
