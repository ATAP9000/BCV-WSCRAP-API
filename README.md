# BCV Scrapper / Extractor BCV

## ENG (English / Inglés)

### About

A little RestAPI WebScrapper built in .NET 8 that gets multiple information from the [Central Bank of Venezuela](https://bcv.org.ve) website in JSON format.

### Prerequisites

Before you begin, ensure you have the following installed:
* .NET 8.0 SDK

### Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. Deployment is up to you.

1. **Clone the repository**

   ```bash
   git clone https://github.com/ATAP9000/BCV-WSCRAP-API.git
   ```

2. **Navigate to the project directory**

   ```bash
   cd BCV-WSCRAP-API
   ```

3. **Install necessary packages**

   Make sure all the required NuGet packages are restored:

   ```bash
   dotnet restore
   ```

### Usage

Start the API locally by running:

```bash
dotnet run
```

Your API will be available at `http://localhost:5211` by default.

### API Endpoints

The following endpoints are available:

- `GET /BCVSCRAP`: Gets Response of API.
- `GET /BCVSCRAP/CurrentExchangeRate`: Retrieves current exchange rate of the available currencies.
- `GET /BCVSCRAP/RecentIntervention`: Retrieves the most recent intervention.
- `GET /BCVSCRAP/Interventions`: Retrieves list of interventions based on a query.
- `GET /BCVSCRAP/BankRates`: Retrieves list of informational rates of the banking system based on a query.

The project also contains a swagger page that can be accessed at `https://localhost:5211/swagger/index.html`

Examples of usage can be seen at the .http file. (Only for VS2022)

### License

Distributed under the MIT License. See `LICENSE` for more information.

## ESP (Spanish / Español)

Un pequeño RestAPI WebScrapper hecho en .NET 8 que obtiene multiple informacion desde el sitio web del [Banco Central de Venezuela](https://bcv.org.ve) en formato JSON.

### Requisitos previos

Antes de empezar, hay que asegurarse de tener instalado lo siguiente:
* .NET 8.0 SDK

### Instalación

Estas instrucciones permitirán tener una copia del proyecto funcionando en la máquina local para fines de desarrollo y pruebas. El despliegue dependera de usted.

1. **Clonar el Repositorio**

   ```bash
   git clone https://github.com/ATAP9000/BCV-WSCRAP-API.git
   ```

2. **Navegar al directorio del proyecto**

   ```bash
   cd BCV-WSCRAP-API
   ```

3. **Instalar los paquetes necesarios**

   Asegúrese de que todos los paquetes NuGet necesarios están restaurados:

   ```bash
   dotnet restore
   ```

### Uso

Para empezar a usar el API localmente:

```bash
dotnet run
```

El API estara disponible en la siguiente ruta `http://localhost:5211` por defecto.

### API Endpoints

Los siguientes son los endpoints disponibles:

- `GET /BCVSCRAP`: Obtiene una respúesta del API.
- `GET /BCVSCRAP/CurrentExchangeRate`: Obtiene la tasa de cambio actual de las divisas disponibles.
- `GET /BCVSCRAP/RecentIntervention`: Obtiene la intervención más reciente.
- `GET /BCVSCRAP/Interventions`: Obtiene listado de intervenciones basada en una consulta.
- `GET /BCVSCRAP/BankRates`: Obtiene listado de las tasas informativas del sistema bancario basado en una consulta.

El proyecto también contiene una página swagger a la que se puede acceder por la siguiente ruta `https://localhost:5211/swagger/index.html`.

Se pueden ver ejemplos de uso en el archivo .http. (Solo para VS2022)

### Licencia

Distribuido bajo la licencia MIT. Véase `LICENSE` para más información.
