# LYD CLOTHES 🖤

> Tienda web de ropa masculina minimalista — ASP.NET Core 9 + SQLite

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=flat-square&logo=sqlite&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-9.0-512BD4?style=flat-square)
![License](https://img.shields.io/badge/licencia-MIT-green?style=flat-square)

---

## ✨ Características

- 🛍️ **Catálogo público** — grilla de productos con imágenes, precios y categorías
- 🔐 **Panel admin protegido** — login con autenticación por cookies
- 📦 **CRUD completo** — crear, editar, activar/desactivar y eliminar productos
- 🖼️ **Carga de imágenes** — subida y previsualización de fotos de productos
- 💾 **Base de datos SQLite** — se crea automáticamente al iniciar, sin configuración extra
- 📱 **Diseño responsive** — adaptado a mobile y desktop

---

## 🚀 Instalación y uso

### Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022+ (workload: *ASP.NET and web development*)

### Pasos

```bash
# 1. Clonar el repositorio
git clone https://github.com/TU_USUARIO/lyd-clothes.git
cd lyd-clothes

# 2. Restaurar dependencias
dotnet restore

# 3. Correr la app
dotnet run
```

La base de datos `lyd.db` se crea **automáticamente** en la primera ejecución con 3 productos de ejemplo.

Abrí el navegador en: **http://localhost:5000**

---

## 🗂️ Estructura del proyecto

```
LYDClothes/
├── Controllers/
│   ├── HomeController.cs       # Tienda pública
│   ├── AdminController.cs      # Panel de administración (requiere login)
│   └── AccountController.cs    # Login / Logout
├── Data/
│   └── AppDbContext.cs         # Contexto EF Core + SQLite
├── Models/
│   ├── Product.cs              # Modelo de producto
│   └── LoginViewModel.cs       # ViewModel del formulario de login
├── Views/
│   ├── Shared/_Layout.cshtml   # Navbar y footer
│   ├── Home/Index.cshtml       # Página principal
│   ├── Admin/                  # Panel admin (Index, Create, Edit)
│   └── Account/Login.cshtml    # Página de login
├── wwwroot/
│   ├── css/site.css            # Estilos
│   └── uploads/                # Imágenes subidas (auto-generado)
├── appsettings.json            # Configuración y credenciales admin
└── Program.cs                  # Entry point y configuración de servicios
```

---

## 🔐 Acceso al panel Admin

| | |
|---|---|
| **URL** | `/Admin` → redirige a `/Account/Login` si no estás logueado |
| **Usuario** | `admin` |
| **Contraseña** | `lyd2026` |

> ⚠️ **Antes de publicar en producción**, cambiá las credenciales en `appsettings.json`.

### URLs principales

| Ruta | Descripción |
|---|---|
| `/` | Tienda pública |
| `/Account/Login` | Login del admin |
| `/Admin` | Panel de administración |
| `/Admin/Create` | Crear nuevo producto |
| `/Admin/Edit/{id}` | Editar un producto |

---

## 🛠️ Tecnologías

| Tecnología | Uso |
|---|---|
| ASP.NET Core 9 MVC | Framework principal |
| Entity Framework Core 9 | ORM |
| SQLite | Base de datos |
| Cookie Authentication | Autenticación del admin |
| Bebas Neue + DM Sans | Tipografía |

---

## 🗺️ Roadmap

- [ ] Autenticación con ASP.NET Identity (múltiples usuarios)
- [ ] Carrito de compras con sesión
- [ ] Filtros y búsqueda por categoría
- [ ] Integración con MercadoPago
- [ ] Paginación de productos
- [ ] Panel de estadísticas

---

## 📄 Licencia

MIT — libre para usar y modificar.

---

<div align="center">
  Hecho con 🖤 para <strong>LYD CLOTHES</strong>
</div>
