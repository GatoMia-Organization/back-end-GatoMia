using Google.Cloud.Firestore;
using BackEndGatoMia.Models;

[FirestoreData]
public class UserType
{
    [FirestoreProperty("id")]
    public int Id { get; set; }

    [FirestoreProperty("name")]
    public string Name { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new HashSet<User>();

    public UserType(){ }
}
