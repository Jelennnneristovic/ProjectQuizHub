QuizHub
QuizHub is a web application that allows users to create, participate in, and manage quizzes. It offers features for both administrators and users, including quiz creation, participation, result tracking, and leaderboard viewing.

---

Technologies
• Frontend: Angular (VS Code)
• Backend: C# .NET (ASP.NET Core Web API, Visual Studio)
• Database: SQL Server
• Authentication: JWT Token

---

Getting Started

1. Clone the Repository
   git clone <URL_REPOSITORY>
   cd <PROJECT_FOLDER>
2. Backend Setup (C# .NET)
3. Open Visual Studio and load the backend project (.sln file).
4. Ensure the connection string in appsettings.json points to your SQL Server instance:
5. "ConnectionStrings": {
6. "DefaultConnection": "Server=localhost;Database=QuizHubDb;Trusted_Connection=True;"
7. }
8. Create the database in SQL Server Management Studio (SSMS):
9. CREATE DATABASE QuizHubDb;
10. Apply database migrations:
11. dotnet ef migrations add InitialCreate
12. dotnet ef database update
13. Run the backend:
    o In Visual Studio: Press F5 or click Run.
    o Or via command line:
    o dotnet run
    The backend will be accessible at https://localhost:5247.
14. Frontend Setup (Angular)
15. Open VS Code and navigate to the frontend project folder.
16. Install dependencies:
17. npm install
18. Ensure the API URL in src/environments/environment.ts is set correctly:
19. export const environment = {
20. production: false,
21. apiUrl: 'https://localhost:5247/api'
22. imageUrl: 'https://localhost:5247'
23.
24. };
25. Run the frontend:
26. ng serve
    The frontend will be accessible at http://localhost:4200.

---

Features
User
• Register and log in
• View, filter, and search quizzes
• Start and complete quizzes
• View personal results and leaderboard
Admin
• Create, update, and delete quizzes
• Manage quiz questions and categories
• Assign difficulty levels
• View all users' results and leaderboard

---

Notes
• Ensure the backend is running before starting the frontend.
• JWT authentication is implemented; login credentials are required to access certain features.
• To reset the database:
• dotnet ef database drop
dotnet ef database update
