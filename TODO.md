# خطة إصلاح مشروع TheCopy - الحل النهائي

## تاريخ: 26 نوفمبر 2025

---

## المشكلة الجذرية

### 1. تعارض إصدارات .NET
- النظام يحتوي على .NET 10.0.100-rc.2 (Release Candidate)
- المشروع يحتاج .NET 10.0.0 Stable
- محاولة تثبيت .NET 10 Stable فشلت بسبب مكتبة ICU مفقودة

### 2. مشكلة المكتبات المفقودة
- .NET المثبت يدوياً يطلب `libicu` ولا يجدها
- البيئة (Google Cloud Workstation) تستخدم Nix package manager
- ICU موجودة في `/nix/store/` لكن .NET لا يراها

### 3. مشكلة المساحة
- القرص المؤقت `/ephemeral` ممتلئ 100% (22GB/22GB)
- `/ephemeral/nix`: 58GB
- `/ephemeral/template_cache`: 2.6GB

---

## الحل الهجين (Hybrid Solution)

**الفكرة**: استخدام Nix shell لتوفير مكتبات ICU والمكتبات الأخرى، مع تثبيت .NET 10 Stable يدوياً في مجلد محلي.

---

## المرحلة 1: تنظيف المساحة ⚠️

### الهدف
تحرير مساحة على القرص المؤقت لتجنب أي مشاكل خلال التثبيت.

### الخطوات

#### 1.1 تنظيف template cache
```bash
sudo rm -rf /ephemeral/template_cache/*
df -h /ephemeral  # للتحقق من المساحة المحررة
```

#### 1.2 حذف التثبيتات القديمة الفاشلة
```bash
# حذف التثبيت اليدوي السابق الذي فشل
rm -rf $HOME/dotnet-stable

# حذف .dotnet القديمة
rm -rf $HOME/.dotnet

# تنظيف NuGet cache (لضمان عدم استخدام حزم RC قديمة)
rm -rf $HOME/.nuget/packages/microsoft.aspnetcore*
rm -rf $HOME/.nuget/packages/microsoft.entityframeworkcore*
rm -rf $HOME/.nuget/packages/npgsql*
```

---

## المرحلة 2: إنشاء البيئة الهجينة (Nix + .NET Stable) 🔧

### الهدف
إنشاء بيئة معزولة تحتوي على جميع المكتبات المطلوبة (ICU, OpenSSL, Zlib, إلخ) وتثبيت .NET 10 Stable داخلها.

### الخطوات

#### 2.1 إنشاء ملف shell.nix
في المجلد الرئيسي للمشروع `/home/user/the-copy/`، أنشئ ملف `shell.nix`:

```nix
{ pkgs ? import <nixpkgs> {} }:

let
  dotnet-deps = with pkgs; [
    zlib zlib.dev openssl icu libgdiplus krb5
  ];

in pkgs.mkShell {
  name = "dotnet-10-stable-env";
  buildInputs = dotnet-deps;
  shellHook = ''
    # ربط المكتبات ديناميكياً ليراها الدوت نت
    export LD_LIBRARY_PATH="${pkgs.lib.makeLibraryPath dotnet-deps}:$LD_LIBRARY_PATH"

    # إعداد مسارات الدوت نت المحلي
    export DOTNET_ROOT="$PWD/.dotnet"
    export PATH="$DOTNET_ROOT:$DOTNET_ROOT/tools:$PATH"
    export DOTNET_CLI_TELEMETRY_OPTOUT=1

    echo "✅ Hybrid Environment Loaded: ICU Linked & .NET 10 Stable Path Set."
  '';
}
```

#### 2.2 تفعيل البيئة وتثبيت .NET 10 Stable
```bash
# الدخول إلى مجلد المشروع
cd /home/user/the-copy

# تفعيل nix-shell (سيحمل المكتبات المطلوبة)
nix-shell

# داخل nix-shell الآن - تحميل سكريبت التثبيت
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh

# تثبيت .NET 10 Stable في المجلد المحلي .dotnet
./dotnet-install.sh --channel 10.0 --quality signed --install-dir .dotnet

# التحقق من التثبيت (يجب أن يظهر 10.0.100 أو 10.0.0)
dotnet --version

# يجب أن يظهر: 10.0.100 (وليس rc.2)
```

---

## المرحلة 3: تحديث ملفات المشروع للإصدار Stable 📝

### الهدف
إزالة جميع الإشارات لنسخ RC واستخدام الإصدارات النهائية 10.0.0 Stable فقط.

### الخطوات

#### 3.1 تحديث TheCopy.Server/TheCopy.Server.csproj
```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="10.0.0" />
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.0">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
</PackageReference>
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="9.0.1" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="7.2.0" />
<PackageReference Include="MongoDB.Driver" Version="3.5.1" />
<PackageReference Include="StackExchange.Redis" Version="2.10.1" />
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
```

**ملاحظة**: Npgsql.EntityFrameworkCore.PostgreSQL 10.0.0 غير متوفرة، نستخدم 9.0.1

#### 3.2 تحديث TheCopy.Client/TheCopy.Client.csproj
```xml
<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="10.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="10.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Components.Authorization" Version="10.0.0" />
```

#### 3.3 تحديث TheCopy.Infrastructure/TheCopy.Infrastructure.csproj
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.0" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="9.0.1" />
<PackageReference Include="Google.Cloud.AIPlatform.V1" Version="3.13.0" />
```

#### 3.4 تحديث TheCopy.Application/TheCopy.Application.csproj
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.0" />
```

---

## المرحلة 4: التنظيف وإعادة البناء 🏗️

### الهدف
بناء المشروع من الصفر باستخدام .NET 10 Stable والحزم المحدثة.

### الخطوات

**⚠️ تأكد من أنك داخل nix-shell قبل تنفيذ هذه الأوامر!**

#### 4.1 تنظيف المشاريع
```bash
cd /home/user/the-copy
dotnet clean --configuration Release
dotnet clean --configuration Debug
```

#### 4.2 استعادة الحزم
```bash
dotnet restore
```

#### 4.3 بناء المشروع
```bash
dotnet build --configuration Release
```

**النتيجة المتوقعة**:
- 0 Errors
- قد تظهر Warnings عن nullable references (يمكن تجاهلها)

---
