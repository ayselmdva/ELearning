using System.ComponentModel.DataAnnotations.Schema;

namespace elearning.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Teacher? Teacher { get; set; }
        public int TeacherId { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
