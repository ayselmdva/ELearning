using System.ComponentModel.DataAnnotations;

namespace elearning.View_Models
{
    public class LoginVM
    {
        public int Id { get; set; }
        public string UserNameOrUser { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
