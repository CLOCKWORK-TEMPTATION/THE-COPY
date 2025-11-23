# 🚀 Quick Start Guide

## Step 1: Update Connection Strings

Open `TheCopy.Server/appsettings.json` and replace the placeholders:

```json
{
  "ConnectionStrings": {
    "PostgresConnection": "Host=your-neon-host;Database=thecopy;Username=user;Password=pass",
    "MongoDbConnection": "mongodb+srv://user:pass@cluster.mongodb.net/thecopy",
    "RedisConnection": "your-redis-host:6379"
  },
  "AI": {
    "GeminiApiKey": "your-actual-gemini-api-key"
  }
}
```

## Step 2: Verify .NET 10 Installation

```bash
dotnet --version
# Should output: 10.0.x
```

If not installed, download from: https://dotnet.microsoft.com/download/dotnet/10.0

## Step 3: Restore NuGet Packages

```bash
dotnet restore
```

## Step 4: Build the Solution

```bash
dotnet build
```

## Step 5: Run the Server

```bash
cd TheCopy.Server
dotnet run
```

Visit: `https://localhost:7000/swagger`

## Step 6: Run the Client (New Terminal)

```bash
cd TheCopy.Client
dotnet run
```

Visit: `https://localhost:7001`

## Troubleshooting

### "Unable to find package"
```bash
dotnet restore --force
```

### "Certificate not trusted"
```bash
dotnet dev-certs https --trust
```

### "Port already in use"
Edit `Properties/launchSettings.json` and change the ports.

---

**You're ready to start migrating your MERN app to .NET 10 LTS!** 🎉

**Note:** .NET 10 has **Long Term Support (LTS) until 2028** - perfect for production applications!
