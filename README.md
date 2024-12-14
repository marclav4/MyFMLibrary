# MyFMLibrary
This project illustrates in a very minimalistic way, the use of a database to store a list of users' favourite stations.
The Radio Browser API is used to fetch stations, and .Net Core Identity is used to store and manage the users.
Authorization is granted via JWT tokens.

## Deploy locally
- Visual Studio 2022 is needed to work on the solution. 
- .Net Core 8 and EF Core are needed on the machine that will run the project.
- SQL Server Express LocalDB was used with the current configuration.
- Apply migrations by running "dotnet ef database update" on the Package Manager console.
- Run the project.
