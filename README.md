# Technical Test API

API desarrollada como parte de una prueba técnica, utilizando **.NET 10**, **Minimal APIs**, **Entity Framework Core**, **SQLite**, **CQRS** y **FluentValidation**.

---

## 🚀 Tecnologías utilizadas

- **.NET SDK:** 10.0
- **ASP.NET Core Minimal API**
- **Entity Framework Core**
- **SQLite**
- **FluentValidation**
- **CQRS (Commands / Queries)**
- **Seguridad por API Key**
- **Postman** (colección incluida)
- **Git**

> Las dependencias del proyecto se gestionan mediante **NuGet** y están definidas en el archivo `.csproj`.

---
## 📌 Requisitos

- .NET SDK **10** instalado  
  https://dotnet.microsoft.com/download

---

## ▶️ Cómo ejecutar el proyecto

Desde la raíz del proyecto:

```bash
dotnet restore
dotnet build
dotnet run
```

---

## 📦 Estructura del proyecto

- `Controllers/` – Endpoints de la API
- `Domain/` – Entidades y dominio
- `Application/` – Lógica de negocio (CQRS, Handlers)
- `Infrastructure/` – Acceso a datos (EF Core, repositorios)
- `Security/` – Middleware de autenticación por API Key
- `Validations/` – Reglas de validación con FluentValidation
- `Migrations/` – Migraciones de base de datos
- `appsettings.json` – Configuración de la aplicación
- `Postman/` – Colección de pruebas Postman
- `Properties/` – Configuración de la aplicación (launchSettings.json, etc.)

---

> Todos los endpoints requieren el header `X-API-KEY` con el valor configurado en `appsettings.json`.

---

## 📝 Notas adicionales

- Los requerimientos fueron cumplidos.
- La base de datos SQLite se crea automáticamente al iniciar la aplicación.
- Las migraciones se aplican automáticamente al iniciar la aplicación.
- La colección de Postman está disponible en la carpeta `Postman/` para probar los endpoints.
