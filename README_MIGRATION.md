# TheCopy - MERN to .NET 10 Migration

## 📋 Project Overview

This is a **Clean Architecture** .NET 10 LTS solution created to migrate a full-stack MERN application (Next.js + Express) to a modern, scalable .NET ecosystem ready for microservices architecture.

**🎯 .NET 10 LTS - Long Term Support until 2028**

---

## 🏗️ Solution Structure

```
TheCopy/
├── TheCopy.sln                        # Solution file
│
├── TheCopy.Shared/                    # 📦 Shared Library (DTOs, Enums, Interfaces)
│   ├── TheCopy.Shared.csproj          # Class library project
│   └── Class1.cs                      # Placeholder (delete when adding real models)
│
├── TheCopy.Server/                    # 🧠 Backend API (ASP.NET Core Web API)
│   ├── TheCopy.Server.csproj          # Web API project with NuGet packages
│   ├── Program.cs                     # Application entry point
│   ├── appsettings.json               # Configuration (connection strings, API keys)
│   ├── appsettings.Development.json   # Development-specific settings
│   └── Properties/
│       └── launchSettings.json        # Launch profiles (HTTP/HTTPS)
│
└── TheCopy.Client/                    # 🎨 Frontend (Blazor WebAssembly)
    ├── TheCopy.Client.csproj          # Blazor WASM project
    ├── Program.cs                     # Client entry point
    ├── App.razor                      # Root component with router
    ├── _Imports.razor                 # Global using statements
    ├── Layout/
    │   └── MainLayout.razor           # Main layout component
    ├── Pages/
    │   └── Home.razor                 # Home page
    ├── wwwroot/
    │   ├── index.html                 # HTML shell
    │   └── css/
    │       └── app.css                # Global styles
    └── Properties/
        └── launchSettings.json        # Client launch profiles
```

---

## 📦 NuGet Packages Installed

### TheCopy.Server (Backend)

| Package | Version | Purpose |
|---------|---------|---------|
| `Npgsql.EntityFrameworkCore.PostgreSQL` | 10.0.0 | PostgreSQL ORM (for Neon DB) |
| `MongoDB.Driver` | 3.0.0 | MongoDB client |
| `StackExchange.Redis` | 2.8.16 | Redis caching & pub/sub |
| `Microsoft.EntityFrameworkCore.Design` | 10.0.0 | EF Core migrations & tooling |
| `Swashbuckle.AspNetCore` | 7.0.0 | OpenAPI/Swagger documentation |

### TheCopy.Client (Frontend)

| Package | Version | Purpose |
|---------|---------|---------|
| `Microsoft.AspNetCore.Components.WebAssembly` | 10.0.0 | Blazor WebAssembly runtime |
| `Microsoft.AspNetCore.Components.WebAssembly.DevServer` | 10.0.0 | Development server |

---

## 🔧 Configuration

### Connection Strings (`TheCopy.Server/appsettings.json`)

```json
{
  "ConnectionStrings": {
    "PostgresConnection": "YOUR_NEON_POSTGRES_URL_HERE",
    "MongoDbConnection": "YOUR_MONGODB_URI_HERE",
    "RedisConnection": "localhost:6379"
  },
  "AI": {
    "GeminiApiKey": "YOUR_GEMINI_KEY_HERE"
  }
}
```

**Action Required**: Replace the placeholder values with your actual connection strings.

---

## 🚀 How to Run

### Prerequisites
- .NET 10 SDK installed ([Download](https://dotnet.microsoft.com/download/dotnet/10.0))
- PostgreSQL/Neon DB instance
- MongoDB instance
- Redis instance (optional for development)

### Running the Backend (API)

```bash
cd TheCopy.Server
dotnet restore
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:7000`
- HTTP: `http://localhost:5000`
- Swagger UI: `https://localhost:7000/swagger` (in Development mode)

### Running the Frontend (Blazor)

```bash
cd TheCopy.Client
dotnet restore
dotnet run
```

The client will be available at:
- HTTPS: `https://localhost:7001`
- HTTP: `http://localhost:5001`

### Running the Entire Solution

From the root directory:

```bash
dotnet restore
dotnet build
```

Then run each project in separate terminals or use Visual Studio/Rider to run multiple startup projects.

---

## 🔄 Migration Mapping

### Technology Stack

| MERN Stack | .NET 9 Ecosystem |
|------------|------------------|
| Next.js | Blazor WebAssembly |
| React Components | Razor Components (.razor) |
| Express.js | ASP.NET Core Web API |
| Node.js | .NET Runtime |
| TypeScript | C# 13 |
| npm/yarn | NuGet |
| Drizzle ORM | Entity Framework Core |
| MongoDB Driver (Node) | MongoDB.Driver (.NET) |
| Redis Client (Node) | StackExchange.Redis |
| BullMQ | Hangfire (to be added) |
| Zod Validation | FluentValidation (to be added) |

### Project References

- `TheCopy.Server` → references → `TheCopy.Shared` ✅
- `TheCopy.Client` → references → `TheCopy.Shared` ✅

This allows **type sharing** between frontend and backend!

---

## 📝 Next Steps

### Phase 1: Domain Modeling (Current - Ready to Start)
- [ ] Delete `Class1.cs` from `TheCopy.Shared`
- [ ] Create `Models/` folder in `TheCopy.Shared`
- [ ] Create `Enums/` folder in `TheCopy.Shared`
- [ ] Migrate TypeScript interfaces → C# records/classes
- [ ] Migrate TypeScript enums → C# enums

### Phase 2: Backend Implementation
- [ ] Add FluentValidation package
- [ ] Create `Data/` folder with DbContext
- [ ] Create `Controllers/` for API endpoints
- [ ] Configure Entity Framework Core
- [ ] Configure MongoDB
- [ ] Configure Redis
- [ ] Implement authentication/authorization

### Phase 3: Frontend Implementation
- [ ] Create layout components
- [ ] Migrate Next.js pages → Blazor pages
- [ ] Integrate TailwindCSS (via CDN or npm)
- [ ] Integrate Three.js (via JSInterop)
- [ ] Integrate GSAP (via JSInterop)
- [ ] Create HTTP service wrappers

### Phase 4: Advanced Features
- [ ] Add Hangfire for background jobs
- [ ] Implement real-time features (SignalR)
- [ ] Add caching strategies
- [ ] Implement API versioning
- [ ] Add health checks
- [ ] Configure CORS policies

### Phase 5: DevOps & Deployment
- [ ] Docker containerization
- [ ] CI/CD pipeline
- [ ] Kubernetes manifests (for microservices)
- [ ] Monitoring & logging (Application Insights)

---

## 🎯 CLI Commands Reference

The following commands were used to create this structure (for reference):

```bash
# Create solution
dotnet new sln -n TheCopy

# Create Shared library
dotnet new classlib -o TheCopy.Shared
dotnet sln add TheCopy.Shared/TheCopy.Shared.csproj

# Create Server API
dotnet new webapi -o TheCopy.Server
dotnet sln add TheCopy.Server/TheCopy.Server.csproj
dotnet add TheCopy.Server reference TheCopy.Shared

# Create Client (Blazor WASM)
dotnet new blazorwasm -o TheCopy.Client
dotnet sln add TheCopy.Client/TheCopy.Client.csproj
dotnet add TheCopy.Client reference TheCopy.Shared

# Install packages (Server)
cd TheCopy.Server
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package MongoDB.Driver
dotnet add package StackExchange.Redis
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## 🔒 Security Notes

⚠️ **Important**: Before deploying to production:

1. **Never commit secrets to Git**
   - Add `appsettings.json` to `.gitignore` (excluding `appsettings.Development.json`)
   - Use environment variables or Azure Key Vault for production secrets

2. **Use User Secrets for local development**
   ```bash
   cd TheCopy.Server
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:PostgresConnection" "your-connection-string"
   ```

3. **Enable HTTPS in production**
   - Already configured in `launchSettings.json`
   - Ensure SSL certificates are properly configured

---

## 📚 Additional Resources

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Clean Architecture Guide](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Microservices with .NET](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/)

---

## 🤝 Contributing

When adding new features:

1. **Add DTOs to TheCopy.Shared** first
2. **Implement API endpoints** in TheCopy.Server
3. **Create Blazor pages/components** in TheCopy.Client
4. **Test end-to-end** before committing

---

**Status**: ✅ Infrastructure Complete - Ready for Domain Modeling

**Last Updated**: 2025-11-23
