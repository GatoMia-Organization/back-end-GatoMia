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
        DocumentReference userRef = _firestoreDb.Collection("users").Document(userId);
        DocumentSnapshot snapshot = await userRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            return snapshot.ConvertTo<User>();
        }

        return null;
    }

    public async Task UpdateUserAsync(User user)
    {
        DocumentReference userRef = _firestoreDb.Collection("users").Document(user.Id);
        await userRef.SetAsync(user);
    }

    public async Task DeleteUserAsync(string userId)
    {
        DocumentReference userRef = _firestoreDb.Collection("users").Document(userId);
        await userRef.DeleteAsync();
    }

}