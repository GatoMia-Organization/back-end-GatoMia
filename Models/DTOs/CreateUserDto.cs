using System.ComponentModel.DataAnnotations;
using BackEndGatoMia.Models.Enums;

namespace BackEndGatoMia.Models.DTOs
{
    public class CreateUserDto
    {

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Password { get; set; }

        public string? Phone { get; set; } = string.Empty; // Telefone é opcional

    }
}