using Google.Cloud.Firestore;
using BackEndGatoMia.Models;
using BackEndGatoMia.Repositories;

public class FirebaseUserRepository : IUserRepository
{
    private readonly FirestoreDb _firestoreDb;
    private readonly CollectionReference _usersCollection;

    public FirebaseUserRepository(FirestoreDb firestoreDb)
    {
        _firestoreDb = firestoreDb;
        _usersCollection = _firestoreDb.Collection("users");
    }

    public async Task<User> AddUserAsync(User user)
    {
        DocumentReference docRef = await _usersCollection.AddAsync(user);
        user.Id = docRef.Id;
        return user;
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        DocumentReference docRef = _usersCollection.Document(userId);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        return snapshot.Exists ? snapshot.ConvertTo<User>() : null;
    }
    
    public async Task<User> GetUserByEmailAsync(string email)
    {
        Query query = _usersCollection.WhereEqualTo("email", email).Limit(1);
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
        return querySnapshot.Documents.Count > 0 ? querySnapshot.Documents[0].ConvertTo<User>() : null;
    }

    public async Task UpdateUserAsync(User user)
    {
        DocumentReference docRef = _usersCollection.Document(user.Id);
        await docRef.SetAsync(user, SetOptions.MergeAll);
    }

    public async Task DeleteUserAsync(string userId)
    {
        DocumentReference docRef = _usersCollection.Document(userId);
        await docRef.DeleteAsync();
    }
}