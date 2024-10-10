# INCOMEX API - Proyecto .NET Core 8

Este proyecto es una API RESTful desarrollada en .NET Core 8 para gestionar productos y categorías, ofreciendo servicios tanto a clientes como para procesos internos de INCOMEX. La API está diseñada para ser escalable, segura y fácilmente mantenible, siguiendo las mejores prácticas de desarrollo.

## Índice

- [Descripción del Proyecto](#descripción-del-proyecto)
- [Diseño de la Arquitectura](#diseño-de-la-arquitectura)
- [Requerimientos](#requerimientos)
- [Instalación y Ejecución](#instalación-y-ejecución)
- [Servicios Disponibles (Endpoints)](#servicios-disponibles-endpoints)
- [Pruebas Unitarias](#pruebas-unitarias)
- [Seguridad](#seguridad)
- [Despliegue en la Nube](#despliegue-en-la-nube)
- [Autor](#autor)

## Descripción del Proyecto

La API permite gestionar **categorías** y **productos**, proporcionando los siguientes servicios clave:

- Creación de categorías (por defecto, se crean las categorías "SERVIDORES" y "CLOUD").
- Creación de 100,000 productos asociados a las categorías previamente creadas.
- Listado de productos, con opciones de paginación.
- Búsqueda de productos por ID y recuperación de la foto de la categoría asociada.

El proyecto está estructurado en múltiples capas para asegurar la separación de responsabilidades, la escalabilidad, y facilitar la inyección de dependencias, pruebas unitarias, y mantenimiento.

## Diseño de la Arquitectura

El proyecto sigue una arquitectura en capas, organizada de la siguiente manera:

- **Business/**: Contiene la lógica de negocio de la aplicación, organizando las reglas de negocio y su interacción con la capa de datos.

  - **Implements/**: Implementaciones concretas de las interfaces de negocio.
  - **Interfaces/**: Define las interfaces para la lógica de negocio y permite inyección de dependencias y pruebas unitarias.

- **Data/**: Acceso a datos, incluyendo la configuración del contexto de la base de datos y las implementaciones para persistir la información.

  - **Implements/**: Implementaciones para el acceso a datos.
  - **Interfaces/**: Interfaces que definen las operaciones de acceso a datos.

- **Entity/**: Modelos y DTOs que representan las entidades de la base de datos y los objetos de transferencia de datos.

  - **Context/**: Configuración de `DbContext`, que gestiona las conexiones y operaciones en la base de datos.
  - **Dto/**: Objetos de transferencia de datos utilizados para intercambiar información entre las capas.
  - **Model/**: Clases que representan las entidades de la base de datos.

- **Utilities/**: Clases auxiliares para validaciones, formatos y otros métodos que no pertenecen directamente a las demás capas.

- **Web/**: Proyecto principal que expone la API a través de controladores.

  - **Controllers/**: Controladores de API que manejan solicitudes HTTP, coordinando entre la lógica de negocio y la capa de datos.
  - **appsettings.json**: Archivo de configuración con parámetros clave, como cadenas de conexión y ajustes de logging.
  - **Dockerfile**: Configuración para crear una imagen Docker de la API.
  - **Program.cs**: Punto de entrada de la aplicación que configura los servicios y el servidor.
  - **ServiceExtensions.cs**: Métodos de extensión para registrar servicios y simplificar la configuración de la aplicación.

- **TestIncomex.Tests/**: Pruebas unitarias para validar la funcionalidad del código.

### Diagrama de la Arquitectura

```plaintext
┌───────────────────────┐
│       Web/API         │
│    (Controladores)     │
└───────────────────────┘
           │
           ▼
┌───────────────────────┐
│      Business         │
│   (Lógica de negocio) │
└───────────────────────┘
           │
           ▼
┌───────────────────────┐
│        Data           │
│  (Acceso a datos)     │
└───────────────────────┘
           │
           ▼
┌───────────────────────┐
│       Entity          │
│  (Modelos/DTOs)       │
└───────────────────────┘
```

## Requerimientos

Para ejecutar este proyecto, necesitarás:

- .NET Core 8.0 SDK
- Visual Studio 2022 (o superior)
- SQL Server
- Docker (opcional para contenedores)
- Acceso a Google App Engine o AWS para despliegue en la nube.

## Instalación y Ejecución

1. Clona el repositorio:

   ```bash
   git clone https://github.com/tu-repo/incomex-api.git
   ```

2. Configura la base de datos:

   - En el archivo `appsettings.json`, actualiza la cadena de conexión a la base de datos.

3. Ejecuta las migraciones para crear el esquema de la base de datos:

   ```bash
   dotnet ef database update
   ```

4. Inicia la API:
   ```bash
   dotnet run
   ```

## Servicios Disponibles (Endpoints)

### 1. Crear Categoría

- **POST** `/Category/`
- Crea las categorías "SERVIDORES" y "CLOUD".

### 2. Crear Producto

- **POST** `/Product/`
- Inserta 100,000 productos generados aleatoriamente, asociados a las categorías creadas.

### 3. Listar Productos

- **GET** `/Products/`
- Devuelve la lista de productos con soporte para paginación.
  - Parámetros opcionales: `pageSize`, `pageNumber`.

### 4. Obtener Producto por ID

- **GET** `/Products/{id}/`
- Devuelve los detalles de un producto específico, junto con la foto de la categoría a la que pertenece.

## Pruebas Unitarias

El proyecto incluye un conjunto de pruebas unitarias para validar la lógica de negocio y las operaciones de acceso a datos. Las pruebas están organizadas en el proyecto `TestIncomex.Tests`, el cual puede ejecutarse con:

```bash
dotnet test
```

## Seguridad

La API implementa las siguientes medidas de seguridad:

- **Validación de Entradas**: Todas las entradas del usuario son validadas y sanitizadas para evitar ataques de inyección de código y otros vectores maliciosos.
- **HTTPS obligatorio**: Todas las conexiones se realizan sobre HTTPS.

## Despliegue en la Nube

La API está preparada para ser desplegada en servicios de cloud computing como **Google App Engine** o **AWS**. Puedes usar el `Dockerfile` incluido para crear una imagen Docker y desplegarla fácilmente.

Pasos para despliegue en Google App Engine:

1. Compilar la imagen Docker:
   ```bash
   docker build -t incomex-api .
   ```
2. Ejecutar la imagen localmente:
   ```bash
   docker run -p 8080:80 incomex-api
   ```

## Autor

**Jhon Corredor**  
Proyecto para INTCOMEX.  
Desarrollado con **.NET Core 8** y tecnologías modernas para la implementación de APIs.
