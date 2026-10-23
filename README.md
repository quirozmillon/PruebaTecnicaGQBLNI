# PruebaGDQM — Instrucciones de ejecución

Guía para compilar y ejecutar la API (.NET 10 + SQLite + Swagger).

---

## 1. Requisitos previos

- **.NET SDK 10.0** o superior.
- **Visual Studio 2026** con la carga de trabajo *ASP.NET and web development* (opcional si se usa la CLI).
- **EF Core CLI** (solo si se van a aplicar migraciones):

```bash
dotnet tool install --global dotnet-ef
```

Verificar la instalación:

```bash
dotnet --version
```

---

## 2. Configurar la cadena de conexión

Editar `Presentacion/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=C:\\PruebaTecnicaGQBLNI\\Presentacion\\Sqlite\\Prueba.db"
  }
}
```

> ⚠️ La ruta es **absoluta**. Si clonas el proyecto en otra carpeta o sistema operativo, ajústala:
>
> - Windows: `Data Source=C:\\ruta\\al\\repo\\Presentacion\\Sqlite\\Prueba.db`
> - Linux/macOS: `Data Source=/ruta/al/repo/Presentacion/Sqlite/Prueba.db`
> - Relativa (recomendado): `Data Source=Sqlite/Prueba.db`

---

## 3. Restaurar y compilar

Desde la raíz del repositorio:

```bash
dotnet restore PruebaGDQM.slnx
dotnet build PruebaGDQM.slnx
```

---

## 4. Preparar la base de datos

El repositorio ya incluye `Presentacion/Sqlite/Prueba.db`. Si no existe o necesitas recrearla:

```bash
dotnet ef database update --project Datos --startup-project Presentacion
```

Para recrearla desde cero, elimina `Presentacion/Sqlite/Prueba.db*` y vuelve a ejecutar el comando.

---

## 5. Ejecutar la API

### Opción A: Visual Studio 2026

1. Abrir `PruebaGDQM.slnx`.
2. Establecer **Presentacion** como proyecto de inicio.
3. Seleccionar el perfil `http` (o `https`) en la barra de herramientas.
4. Presionar **F5** (con depuración) o **Ctrl + F5** (sin depuración).
5. El navegador se abre automáticamente en `http://localhost:8000/swagger`.

### Opción B: línea de comandos

```bash
dotnet run --project Presentacion
```

La API queda disponible en `http://localhost:8000`.

Para usar otro puerto sin modificar `launchSettings.json`:

```bash
dotnet run --project Presentacion --urls http://localhost:5050
```

Si ejecutas fuera del perfil de desarrollo, habilita Swagger explícitamente:

```bash
# PowerShell
$env:ASPNETCORE_ENVIRONMENT="Development"; dotnet run --project Presentacion

# Bash
ASPNETCORE_ENVIRONMENT=Development dotnet run --project Presentacion
```

---

## 6. URLs de acceso

| Perfil | API | Swagger |
|---|---|---|
| `http` | `http://localhost:8000` | `http://localhost:8000/swagger` |
| `https` | `https://localhost:7065` y `http://localhost:8000` | `https://localhost:7065/swagger` |

- Contrato OpenAPI (JSON): `http://localhost:8000/swagger/v1/swagger.json`
- Swagger solo está habilitado en entorno `Development` (los perfiles de `launchSettings.json` ya lo configuran).

---

## 7. Ejecutar las pruebas unitarias 

```bash
dotnet test NegocioPruebaUnitaria/NegocioPruebaUnitaria.csproj
```

---

