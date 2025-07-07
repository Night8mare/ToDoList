# ToDoList

This is a simple C# ToDoList application using **Entity Framework Core** and **SQL Server**. It allows users to:
- Sign up and sign in (passwords are hashed and securely stored)
- Create, edit, delete, and complete tasks
- Store and manage tasks associated with each user
---
## Tech Stack

- C# (.NET Core)
- Entity Framework Core
- SQL Server
- Console App (can be upgraded to MVC in future)
- Password hashing using `Rfc2898DeriveBytes`

---

## Project Structure

- `Program.cs` — The main entry point
- `AuthService.cs` — Handles Sign In / Sign Up logic
- `Chat.cs` - Handles Sign Input
- `OperationOption.cs` — Handles task creation and management
- `PasswordService.cs` — Responsible for hashing and verifying passwords
- `ApplicationDbContext.cs` — The EF Core DbContext
- `UserInfo` and `TasksInfo` models — Represent the database structure (only inside code)
- `SqlServer/ToDoList.bak` — Backup file of the SQL Server database

---

## How to Run

1. **Clone the repo**:
   ```bash
   git clone https://github.com/Night8mare/ToDoList

2. **Restore the Database**:

    Open SQL Server Management Studio (SSMS)

    Right-click on Databases > Restore Database

    Select Device, then browse and add the .bak file from Database/ToDoList.bak

    Click OK to restore the database

3. **Scaffold Database**:

   dotnet ef dbcontext scaffold "Server=YOUR_SERVER_NAME;Database=ECommerce;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --context ApplicationDbContext --use-database-names

5. **Update Connection String**:

    Go to ApplicationDbContext.cs

    Update the options.UseSqlServer(...) with your local SQL Server instance

6. **Build and Run**:

    Open the solution in Visual Studio or VS Code

    Run the app:

    dotnet run
