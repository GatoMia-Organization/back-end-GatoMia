namespace BackEndGatoMia.Models.DTOs
{
    // DTO seguro para retornar dados do usuário ao cliente
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
    }
}