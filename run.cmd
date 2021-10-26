cd ./RBUkraine.PL/
dotnet ef database update
dotnet build
start "" "https://localhost:5001"
dotnet run