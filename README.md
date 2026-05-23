# StudyRecommendation API

API de recomendación de recursos educativos para estudiantes universitarios. Dada una materia o un tema, la aplicación sugiere videos de YouTube y artículos relevantes usando inteligencia artificial.

---

## ¿Qué hace?

- **Procesa un programa de materia:** a partir del nombre de la materia (o un PDF del programa), genera automáticamente la estructura de unidades y temas usando IA.
- **Busca recursos por tema:** para cualquier tema de estudio, encuentra un video de YouTube y un artículo relacionado.
- **Guarda los resultados:** las búsquedas se guardan para no repetirlas. Si alguien ya buscó ese tema, el resultado se devuelve al instante.
- **Calificación de contenido:** los usuarios pueden valorar (positivo / negativo) los videos y artículos sugeridos.

---

## Requisitos

Antes de poder usar la aplicación, cada persona necesita tener instalado lo siguiente:

### 1. Node.js
Es necesario para instalar Claude Code.

- Descargalo desde [https://nodejs.org](https://nodejs.org) (elegí la versión **LTS**)
- Instalalo normalmente como cualquier programa
- Verificá que quedó bien: abrí una terminal y escribí `node --version`. Tiene que aparecer un número de versión.

### 2. Claude Code CLI
Es la herramienta de IA que usa la aplicación para buscar recursos y generar contenido.

- Una vez instalado Node.js, el script de configuración se encarga de todo (ver sección siguiente)
- Necesitás una cuenta en [https://anthropic.com](https://anthropic.com) para iniciar sesión

---

## Configuración inicial (solo la primera vez)

1. Descomprimí el archivo `.zip` en una carpeta de tu computadora
2. Dentro de la carpeta, hacé **click derecho** sobre el archivo `Setup-Companeros.ps1`
3. Elegí **"Ejecutar con PowerShell"**
4. El script va a instalar Claude Code automáticamente y abrir el navegador para que inicies sesión con tu cuenta de Anthropic
5. Seguí los pasos en pantalla. Cuando termines, ya estás listo para usar la aplicación.

> **Nota:** este paso solo hay que hacerlo una vez. La próxima vez que quieras usar la app, podés ir directo al siguiente punto.

---

## Cómo iniciar la aplicación

1. Abrí la carpeta donde descomprimiste el zip
2. Hacé **doble click** en `Iniciar.bat`
3. Se abre una ventana negra. Esperá unos segundos hasta que aparezca el mensaje con las URLs de acceso
4. Ya podés usar la API desde la aplicación móvil o desde el navegador

Para detener la aplicación, hacé click en la ventana negra y presioná `Ctrl + C`.

---

## Probar la API desde el navegador

Una vez que la aplicación está corriendo, podés ver y probar todos los endpoints disponibles entrando a:

```
http://localhost:5000/scalar/v1
```

Desde ahí podés hacer pruebas sin necesidad de código ni aplicaciones externas.

Si querés acceder desde otro dispositivo en la misma red (por ejemplo, desde el celular), la ventana de inicio te muestra la dirección de red local, algo como:

```
http://192.168.0.XXX:5000/scalar/v1
```

---

## Endpoints disponibles

| Método | Ruta | Descripción |
|--------|------|-------------|
| `POST` | `/api/syllabus/process` | Procesa una materia y devuelve su programa con unidades y temas |
| `GET`  | `/api/subjects` | Lista todas las materias guardadas |
| `GET`  | `/api/subjects/{id}/topics` | Lista los temas de una materia |
| `POST` | `/api/search` | Busca un video y un artículo para un tema dado |
| `POST` | `/api/search/{id}/rate` | Califica un resultado de búsqueda |
| `GET`  | `/api/recommendations/{topicId}` | Obtiene recomendaciones de recursos para un tema |
| `POST` | `/api/feedback` | Envía feedback sobre un recurso |

---

## Para desarrolladores

### Configuración (`appsettings.json`)

El archivo `appsettings.json` dentro de la carpeta del proyecto contiene la configuración de las integraciones externas:

```json
"ExternalApis": {
  "Anthropic": {
    "ApiKey": ""           // API key de Anthropic (fallback si Claude Code no está disponible)
  },
  "YouTube": {
    "ApiKey": "",          // API key de YouTube Data API v3 (opcional)
    "MaxResultsPerTopic": 2
  },
  "ClaudeCode": {
    "ExecutablePath": "claude",
    "TimeoutSeconds": 120
  }
}
```

### Generar el distribuible

Desde la raíz del repositorio:

```powershell
.\Scripts\Publicar.ps1
```

Esto genera la carpeta `Distribuible\` y el archivo `StudyRecommendationAPI-distribuible.zip` listo para compartir.

### Stack tecnológico

- .NET 10 / ASP.NET Core Minimal API
- SQLite + Entity Framework Core 10
- Claude Code CLI (IA para búsqueda y generación de contenido)
- Scalar (documentación interactiva de la API)
