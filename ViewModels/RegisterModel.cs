using System.ComponentModel.DataAnnotations;

namespace blogapi.ViewModels
{
    public class RegisterModel
    {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }
    }
}