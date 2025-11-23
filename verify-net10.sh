#!/bin/bash
# Script to verify .NET 10 LTS migration

echo "========================================="
echo "🔍 Checking .NET 10 LTS Migration Status"
echo "========================================="
echo ""

# 1. Check .NET SDK version
echo "1️⃣  .NET SDK Version:"
dotnet --version
echo ""

# 2. Check all .csproj files for TargetFramework
echo "2️⃣  Target Framework in all projects:"
echo ""
for file in $(find . -name "*.csproj" 2>/dev/null); do
    echo "📄 $file"
    grep -A 2 "<TargetFramework>" "$file" | grep "TargetFramework" || echo "   ⚠️  No TargetFramework found"
    echo ""
done

# 3. Check package versions in Server project
echo "3️⃣  NuGet Package Versions (Server):"
echo ""
if [ -f "TheCopy.Server/TheCopy.Server.csproj" ]; then
    grep "PackageReference" "TheCopy.Server/TheCopy.Server.csproj" | head -10
else
    echo "   ⚠️  TheCopy.Server.csproj not found"
fi
echo ""

# 4. Check package versions in Client project
echo "4️⃣  NuGet Package Versions (Client):"
echo ""
if [ -f "TheCopy.Client/TheCopy.Client.csproj" ]; then
    grep "PackageReference" "TheCopy.Client/TheCopy.Client.csproj"
else
    echo "   ⚠️  TheCopy.Client.csproj not found"
fi
echo ""

# 5. Try to restore and build
echo "5️⃣  Testing restore and build:"
echo ""
dotnet restore 2>&1 | grep -E "(Restored|error|warning)" | head -5
echo ""
dotnet build --no-restore 2>&1 | grep -E "(Build succeeded|Build FAILED|error|warning)" | head -10
echo ""

echo "========================================="
echo "✅ Verification Complete!"
echo "========================================="
