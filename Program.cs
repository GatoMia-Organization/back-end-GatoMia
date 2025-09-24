using System.IO;
using Google.Cloud.Firestore;
using BackEndGatoMia.Repositories;
using BackEndGatoMia.Services;

var builder = WebApplication.CreateBuilder(args);

// --- INICIALIZAÇÃO EXPLÍCITA DO FIREBASE ---
string projectId = "gatomiateste"; // <-- COLOQUE SEU ID AQUI!
var credentialPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "serviceAccountKey.json");
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);
var firestoreDb = FirestoreDb.Create(projectId);
builder.Services.AddSingleton(firestoreDb);
// --- FIM DA INICIALIZAÇÃO ---

// Registro de Serviços e Repositórios
builder.Services.AddScoped<IUserRepository, FirebaseUserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Serviços padrão
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();