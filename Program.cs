using System.IO;
using Google.Cloud.Firestore;
using BackEndGatoMia.Repositories;
using BackEndGatoMia.Services;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


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


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          // Diga ao seu backend para aceitar requisições desta origem:
                          policy.WithOrigins("http://localhost:5173") 
                                .AllowAnyHeader() // Permite qualquer cabeçalho (como o 'Authorization')
                                .AllowAnyMethod(); // Permite qualquer método (GET, POST, PUT, etc.)
                      });
});


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

// app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();