using Google.Cloud.Firestore;
using BackEndGatoMia.Models.Enums;

namespace BackEndGatoMia.Models
{
    [FirestoreData]
    public class User
    {
        public User() { }

        [FirestoreDocumentId]
        public string Id { get; set; } = string.Empty;

        [FirestoreProperty("name")]
        public string Name { get; set; } = string.Empty;

        [FirestoreProperty("email")]
        public string Email { get; set; } = string.Empty;

        [FirestoreProperty("passwordHas")]
        public string PasswordHash { get; set; } = string.Empty;

        [FirestoreProperty("phone")]
        public string? Phone { get; set; }

        [FirestoreProperty("dateRegistration")]
        public DateTime DateRegistration { get; set; } = DateTime.UtcNow;

        [FirestoreProperty("isActive")]
        public bool IsActive { get; set; } = true;

        [FirestoreProperty("role")]
        public UserRole Role { get; set; } = UserRole.User;
    }
}
