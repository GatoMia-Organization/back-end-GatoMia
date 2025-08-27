using Google.Cloud.Firestore;
using BackEndGatoMia.Models;
using BackEndGatoMia.Repositories;

public class FirebaseUserRepository : IUserRepository
{
    private readonly FirestoreDb _firestoreDb;

    public FirebaseUserRepository(FirestoreDb firestoreDb)
    {
        _firestoreDb = firestoreDb;
    }

    public async Task AddUserAsync(User user)
    {
        CollectionReference usersCollectionRef = _firestoreDb.Collection("users");

        await usersCollectionRef.AddAsync(user);
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        DocumentReference usersRef = _firestoreDb.Collection("users").Document(userId);
        DocumentSnapshot snapshot = await usersRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            return snapshot.ConvertTo<User>();
        }

        return null;
    }

    public async Task UpdateUserAsync(User user)
    {
        DocumentReference usersRef = _firestoreDb.Collection("users").Document(user.Id);
        await usersRef.SetAsync(user);
    }

    public async Task DeleteUserAsync(string userId)
    {
        DocumentReference usersRef = _firestoreDb.Collection("users").Document(userId);
        await usersRef.DeleteAsync();
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        CollectionReference usersRef = _firestoreDb.Collection("users");
        Query query = usersRef.WhereEqualTo("email", email);

        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

        if (querySnapshot.Documents.Count > 0)
        {
            DocumentSnapshot userSnapshot = querySnapshot.Documents[0];
            return userSnapshot.ConvertTo<User>();
        }
        return null;
    }

}