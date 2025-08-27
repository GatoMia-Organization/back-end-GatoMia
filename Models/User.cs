using Google.Cloud.Firestore;

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

        [FirestoreProperty("userTypeId")]
        public int UserTypeId { get; set; } = 3;

        [FirestoreProperty]
        public UserType? UserType { get; set; }
        
    }
}
