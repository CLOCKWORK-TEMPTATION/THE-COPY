
# Project Blueprint

## 1. Overview

The project is a full-stack web application built with .NET, consisting of a Blazor WebAssembly client and an ASP.NET Core server. The backend is designed with a "Code-First" approach, utilizing PostgreSQL for relational data and MongoDB for non-relational data. The application follows a clean architecture with a clear separation of concerns between the presentation, application, domain, and infrastructure layers.

## 2. Implemented Features

### 2.1. Solution Structure

The solution is organized into three projects:

*   `TheCopy.Server`: The ASP.NET Core backend application.
*   `TheCopy.Client`: The Blazor WebAssembly frontend application.
*   `TheCopy.Shared`: A shared library for DTOs and other common code.

### 2.2. Database and Services

*   **Database Entities:**
    *   `User`: Represents the user of the application.
    *   `Project`: Represents a user's project.
    *   `Script`: Represents a script within a project.
*   **Database Context:** `ApplicationDbContext` is configured for Entity Framework Core with PostgreSQL, defining the relationships between the entities.
*   **MongoDB Service:** A `MongoService` is implemented to manage the connection to a MongoDB database.
*   **API Endpoint:** A verification endpoint `api/Scripts/verify-db` is available to check the database connectivity status.
*   **CORS:** Cross-Origin Resource Sharing is configured to allow requests from the Blazor client application.

## 3. Current Changes: Database Connectivity & Entity Setup

The following steps were taken to implement the database layer in `TheCopy.Server`:

1.  **Created Database Entities (PostgreSQL):**
    *   Created `User.cs`, `Project.cs`, and `Script.cs` in `TheCopy.Server/Entities` to define the database schema using a "Code-First" approach.

2.  **Set Up Entity Framework Core (PostgreSQL):**
    *   Created `Data/ApplicationDbContext.cs` inheriting from `DbContext`.
    *   Included `DbSet` properties for `User`, `Project`, and `Script`.
    *   Overrode `OnModelCreating` to configure relationships and constraints.

3.  **Set Up MongoDB Service:**
    *   Created `Services/MongoService.cs` to manage the MongoDB connection and expose a `GetCollection` method.

4.  **Registered Services in Program.cs:**
    *   Registered `ApplicationDbContext` for PostgreSQL.
    *   Registered `MongoService` as a singleton.
    *   Configured a CORS policy to allow requests from the client application.

5.  **Created Verification Endpoint:**
    *   Updated `ScriptsController` with an endpoint to verify the connectivity of both PostgreSQL and MongoDB.
