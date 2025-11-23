# TheCopy.NET - MERN to .NET 9 Migration Guide

## 📋 Migration Overview

This document outlines the complete migration from a MERN stack (Next.js + Express + TypeScript) to the .NET 9 ecosystem using Clean Architecture principles.

---

## 🏗️ Solution Structure

```
TheCopy.NET/
├── src/
│   ├── TheCopy.Shared/          # Shared library (DTOs, Enums, Validators, Interfaces)
│   │   ├── Models/               # Data Transfer Objects (DTOs)
│   │   ├── Enums/                # Shared enumerations
│   │   ├── Validators/           # FluentValidation validators
│   │   └── Interfaces/           # Shared interfaces
│   │
│   ├── TheCopy.Server/          # ASP.NET Core Web API (Backend)
│   │   ├── Controllers/          # API endpoints
│   │   ├── Services/             # Business logic
│   │   ├── Data/                 # EF Core DbContext, MongoDB, Redis
│   │   ├── Configuration/        # Dependency injection, middleware
│   │   └── Program.cs           # Application entry point
│   │
│   └── TheCopy.Client/          # Blazor WebAssembly (Frontend)
│       ├── Pages/                # Razor pages/routes
│       ├── Components/           # Reusable Blazor components
│       ├── Services/             # HTTP client services
│       └── wwwroot/              # Static assets
│
└── TheCopy.NET.sln              # Solution file
```

---

## 🔄 Technology Mapping

### Frontend Migration

| MERN Stack | .NET 9 Ecosystem | Purpose |
|------------|------------------|---------|
| Next.js | Blazor WebAssembly | UI Framework |
| TypeScript | C# 13 | Type-safe language |
| TailwindCSS | TailwindCSS / MudBlazor | Styling |
| Three.js | Three.js (via JSInterop) | 3D rendering |
| GSAP | GSAP (via JSInterop) | Animations |

### Backend Migration

| MERN Stack | .NET 9 Ecosystem | Purpose |
|------------|------------------|---------|
| Node.js + Express | ASP.NET Core Web API | HTTP server |
| Drizzle ORM (Postgres) | Entity Framework Core | PostgreSQL ORM |
| MongoDB Driver | MongoDB.Driver | MongoDB client |
| Redis Client | StackExchange.Redis | Caching layer |
| BullMQ | Hangfire | Background jobs |
| Zod | FluentValidation | Schema validation |

---

## 📦 NuGet Packages Installed

### TheCopy.Shared
- `FluentValidation` (11.9.0) - Request validation
- `System.Text.Json` (9.0.0) - JSON serialization

### TheCopy.Server
- `Microsoft.EntityFrameworkCore.Design` (9.0.0)
- `Npgsql.EntityFrameworkCore.PostgreSQL` (9.0.0)
- `MongoDB.Driver` (2.29.0)
- `StackExchange.Redis` (2.8.16)
- `Hangfire.AspNetCore` (1.8.14)
- `Hangfire.PostgreSql` (1.20.9)
- `FluentValidation.AspNetCore` (11.3.0)
- `Swashbuckle.AspNetCore` (6.8.1) - API documentation

### TheCopy.Client
- `Microsoft.AspNetCore.Components.WebAssembly` (9.0.0)
- `Microsoft.AspNetCore.Components.WebAssembly.DevServer` (9.0.0)
- `MudBlazor` (7.8.0) - Material Design components

---

## 🎯 Migration Principles

### 1. Type Safety Across Stack
- **Shared DTOs**: Both frontend (Blazor) and backend (API) reference `TheCopy.Shared`
- **Single Source of Truth**: Define models once, use everywhere
- **Compile-Time Safety**: Catch type mismatches before runtime

### 2. Naming Conventions

#### TypeScript → C# Conversion Rules

| TypeScript | C# | Example |
|------------|-----|---------|
| `interface User` | `public record UserDto` | Use records for immutability |
| `camelCase` | `PascalCase` | `userId` → `UserId` |
| `enum Status` | `public enum Status` | - |
| `type UnionType` | `enum` or `class hierarchy` | Depends on use case |

#### JSON Compatibility
Use `[JsonPropertyName]` attributes to maintain compatibility with legacy systems:

```csharp
public record UserDto(
    [property: JsonPropertyName("userId")] Guid UserId,  // Serializes as "userId"
    [property: JsonPropertyName("email")] string Email
);
```

### 3. Validation Strategy

**From Zod (TypeScript) to FluentValidation (C#)**

TypeScript (Zod):
```typescript
const userSchema = z.object({
  email: z.string().email().max(255),
  username: z.string().min(3).max(50)
});
```

C# (FluentValidation):
```csharp
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);

        RuleFor(x => x.Username)
            .MinimumLength(3)
            .MaximumLength(50);
    }
}
```

---

## 🗄️ Database Configuration

### PostgreSQL (via EF Core)
Connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Postgres": "Host=localhost;Database=thecopy;Username=postgres;Password=postgres"
  }
}
```

### MongoDB
Connection string:
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017/thecopy"
  }
}
```

### Redis
Connection string:
```json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379"
  }
}
```

---

## 🚀 Next Steps

### Phase 1: Complete Type Migration ✅
- [x] Create solution structure
- [x] Define core DTOs (User, Script, Project, MediaAsset)
- [x] Define enums (UserRole, ScriptStatus, ProjectStatus, MediaType)
- [x] Create FluentValidation validators
- [x] Configure project references

### Phase 2: Backend Implementation (TODO)
- [ ] Create EF Core DbContext for PostgreSQL
- [ ] Implement MongoDB repositories
- [ ] Configure Redis caching
- [ ] Set up Hangfire background jobs
- [ ] Create API controllers
- [ ] Implement authentication/authorization (JWT)
- [ ] Add API documentation (Swagger)

### Phase 3: Frontend Implementation (TODO)
- [ ] Create main layout and navigation
- [ ] Implement authentication UI
- [ ] Migrate Director's Studio pages
- [ ] Integrate Three.js via JSInterop
- [ ] Integrate GSAP animations
- [ ] Configure TailwindCSS or use MudBlazor components

### Phase 4: Data Migration (TODO)
- [ ] Export data from MongoDB (legacy)
- [ ] Transform data to match new DTOs
- [ ] Import data into PostgreSQL/MongoDB (.NET)
- [ ] Verify data integrity

### Phase 5: Testing & Deployment (TODO)
- [ ] Unit tests (xUnit)
- [ ] Integration tests
- [ ] End-to-end tests
- [ ] Performance testing
- [ ] Docker containerization
- [ ] CI/CD pipeline setup

---

## 📚 Sample Entities Created

### Core Domain Models

1. **UserDto** - User accounts with roles
2. **ScriptDto** - Scripts/screenplays with versioning
3. **ProjectDto** - Production projects with team members
4. **MediaAssetDto** - Media files (images, videos, 3D models)

### Request Models

- `CreateUserRequest`, `UpdateUserRequest`, `LoginRequest`
- `CreateScriptRequest`, `UpdateScriptRequest`
- `CreateProjectRequest`, `UpdateProjectRequest`
- `UploadMediaRequest`

### Validators

- `CreateUserRequestValidator` - Email, username, password validation
- `LoginRequestValidator` - Authentication validation
- `CreateScriptRequestValidator` - Script creation validation
- `UpdateScriptRequestValidator` - Script update validation
- `CreateProjectRequestValidator` - Project creation validation

---

## 🔧 Development Commands

### Build Solution
```bash
dotnet build TheCopy.NET.sln
```

### Run Server (API)
```bash
cd src/TheCopy.Server
dotnet run
# API will be available at https://localhost:7001
# Swagger UI at https://localhost:7001/swagger
# Hangfire dashboard at https://localhost:7001/hangfire
```

### Run Client (Blazor)
```bash
cd src/TheCopy.Client
dotnet run
# Client will be available at https://localhost:5001
```

### Run Tests (when created)
```bash
dotnet test
```

---

## 🎓 Key Architectural Decisions

### Why Blazor WebAssembly?
- **Type Safety**: Share C# models between frontend and backend
- **Performance**: Compiled to WebAssembly for near-native performance
- **No API Translation Layer**: Direct object serialization/deserialization
- **JSInterop**: Can still use JavaScript libraries (Three.js, GSAP)

### Why Records for DTOs?
- **Immutability**: Records are immutable by default
- **Value Semantics**: Equality based on property values, not reference
- **Concise Syntax**: Less boilerplate than classes
- **Perfect for DTOs**: Data transfer objects shouldn't mutate

### Why FluentValidation?
- **Separation of Concerns**: Validation logic separate from models
- **Reusability**: Validators can be composed and reused
- **Testability**: Easy to unit test validation rules
- **Rich API**: Expressive, readable validation rules

### Why Hangfire vs BullMQ?
- **Native .NET**: No need for separate Node.js process
- **Persistent Jobs**: Uses PostgreSQL for job storage
- **Dashboard**: Built-in UI for monitoring
- **Reliable**: Automatic retries, job prioritization

---

## 📖 Additional Resources

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [FluentValidation](https://docs.fluentvalidation.net/)
- [Hangfire](https://www.hangfire.io/)
- [MudBlazor](https://mudblazor.com/)

---

## ⚠️ Important Notes

### TypeScript Source Files
The original TypeScript source files referenced in the migration plan were not found in the repository:
- `backend/src/db/schema.ts`
- `frontend/src/types/index.ts`
- `frontend/src/app/(main)/directors-studio/shared/schema.ts`

**Sample entities have been created based on common MERN stack patterns.** When the actual TypeScript files are available, the C# models should be updated to match the exact schema.

### Connection Strings
Update connection strings in `src/TheCopy.Server/appsettings.json` to match your environment.

### Security
- Change default passwords and secrets before deployment
- Implement proper authentication/authorization
- Use environment variables for sensitive configuration
- Enable HTTPS in production

---

## 🤝 Contributing

When migrating additional features:
1. Define DTOs in `TheCopy.Shared/Models`
2. Create validators in `TheCopy.Shared/Validators`
3. Implement API endpoints in `TheCopy.Server/Controllers`
4. Create Blazor components in `TheCopy.Client/Components`
5. Test end-to-end

---

**Migration Status**: Phase 1 Complete ✅
**Next Milestone**: Backend Implementation (Phase 2)
