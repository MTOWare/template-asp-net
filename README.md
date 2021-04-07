# Add Template
dotnet new -i .

# Create Project with Template
dotnet new mycleanarchitecture -d Evento -n Evento

# Create DbContext with EF & PostgreSQL
Scaffold-DbContext "Server=127.0.0.1;port=5432;user id=postgres;password=<password-here>;database=<Database_Name>;pooling=true" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Data -force

