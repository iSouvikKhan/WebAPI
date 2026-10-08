# WebAPI

<h4>A simple CRUD operation built upon .NET framework by using ASP.NET WebAPI</h4>

A small user management application made of two ASP.NET projects in one Visual Studio solution:

- **API** - an ASP.NET Web API 2 service that stores users in SQL Server through Entity Framework 6 (code first) and exposes create, read, update and delete endpoints.
- **MVC2** - an ASP.NET MVC 5 web front end ("User Management Application") that calls the API with `HttpClient` to list, create, edit and delete users.

## Features

- List all users in a table
- Create a user with client-side form validation (name up to 50 characters, address up to 100 characters, contact number exactly 10 characters)
- Edit an existing user (reuses the Create view)
- Delete a user after a browser confirmation prompt
- REST endpoints for the same operations, usable directly from any HTTP client

## Tech Stack

- C#, .NET Framework 4.7.2 (API) and 4.8 (MVC2)
- ASP.NET Web API 5.2.7
- ASP.NET MVC 5.2.7 with Razor views
- Entity Framework 6.4.4 (code first, migrations)
- SQL Server (via `System.Data.SqlClient`)
- Newtonsoft.Json, Bootstrap 3.4.1, jQuery 3.4.1, jQuery Validation

## Project Structure

```
WebAPI/
├── WebAPI.sln
├── API/                      Web API project
│   ├── App_Start/WebApiConfig.cs   Route: api/{controller}/{id}
│   ├── Controllers/UserController.cs
│   ├── Database/Contexts.cs        DbContext ("WebApiDatabase")
│   ├── Migrations/                 EF code-first migrations
│   ├── Models/User.cs
│   └── App_Data/                   WebApiDatabase .mdf/.ldf files
├── MVC2/                     MVC front end
│   ├── Controllers/UserMVCController.cs
│   ├── Models/UserMVC.cs
│   └── Views/UserMVC/        Index.cshtml, Create.cshtml
└── packages/                 Restored NuGet packages
```

## Data Model

`User`

| Field    | Type   | Notes       |
|----------|--------|-------------|
| UserId   | int    | Primary key |
| Name     | string |             |
| Address  | string |             |
| Contact  | string |             |

## API Endpoints

Base URL when run from Visual Studio: `https://localhost:44391`

| Method | Route            | Description                                   |
|--------|------------------|-----------------------------------------------|
| GET    | `/api/user`      | Get all users                                 |
| GET    | `/api/user/{id}` | Get a user by id                              |
| POST   | `/api/user`      | Add a user (returns 201 Created)              |
| PUT    | `/api/user/{id}` | Update a user; `id` must match `UserId` in the body |
| DELETE | `/api/user/{id}` | Delete a user (returns 404 if not found)      |

Example request body:

```json
{
  "UserId": 1,
  "Name": "Jane Doe",
  "Address": "221B Baker Street",
  "Contact": "9876543210"
}
```

## Prerequisites

- Windows with Visual Studio 2019 or later and the "ASP.NET and web development" workload
- .NET Framework 4.7.2 and 4.8 developer packs
- SQL Server (SQL Server Express or LocalDB)
- IIS Express (installed with Visual Studio)

These are classic .NET Framework projects, so they do not run on Linux or macOS.

## Setup

1. Clone the repository:

   ```
   git clone https://github.com/iSouvikKhan/WebAPI.git
   ```

2. Open `WebAPI/WebAPI.sln` in Visual Studio. NuGet packages are included in `WebAPI/packages`; if anything is missing, restore them (right-click the solution > Restore NuGet Packages).

3. Database: the `Contexts` DbContext uses the connection name `WebApiDatabase`. `API/Web.config` does not define a connection string, so Entity Framework falls back to its default SQL Server connection and a database named `WebApiDatabase`. To use a specific server, add a connection string named `WebApiDatabase` to `API/Web.config`. Then create the schema from the Package Manager Console (default project: `API`):

   ```
   Update-Database
   ```

## Running

The MVC app calls the API at the hardcoded address `https://localhost:44391/api` (see `MVC2/Controllers/UserMVCController.cs`), so the API must be running first.

1. In Visual Studio, right-click the solution > **Set Startup Projects** > **Multiple startup projects**, and set both `API` and `MVC2` to **Start**.
2. Press F5.
   - API: `https://localhost:44391/`
   - MVC front end: `https://localhost:44398/` (opens the user list at `UserMVC/Index`)

If you change the API port, update `baseaddress` in `UserMVCController.cs` to match.
