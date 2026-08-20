# TV Shows Collector

TV Shows Collector is a .NET API for collecting and managing TV show data. It stores shows in SQL Server and can sync show information from the public TvMaze API.

## What it does

- Lists, adds, updates, and deletes TV shows.
- Syncs TV shows from TvMaze.
- Uses Entity Framework Core with SQL Server for persistence.

## Getting started

### Prerequisites

- .NET 10 SDK
- SQL Server running locally or a reachable SQL Server instance
- Optional: `dotnet-ef` for applying database migrations

Install the EF Core CLI if you do not already have it:

```bash
dotnet tool install --global dotnet-ef
```

### Configure the database

The API reads its connection string from:

```text
TvShowsHub/TvShowsHub.API/appsettings.Development.json
```

Update `DbConnectionString` if your SQL Server host, user, password, or database name is different.

### Restore and create the database

From the repository root:

```bash
cd TvShowsHub
dotnet restore
dotnet ef database update \
	--project TvShowsHub.Repository \
	--startup-project TvShowsHub.API
```

### Run the API

```bash
dotnet run --project TvShowsHub.API
```

By default, the development profile uses:

- `http://localhost:5192`
- `https://localhost:7108`

OpenAPI is available in development at:

```text
https://localhost:7108/openapi/v1.json
```

### Main endpoints

- `GET /api/TvShowManager` - list TV shows
- `POST /api/TvShowManager` - add a TV show
- `PUT /api/TvShowManager` - update a TV show
- `DELETE /api/TvShowManager/{id}` - delete a TV show
- `GET /api/TvMazeSync/sync` - sync shows from TvMaze

### Run tests

```bash
dotnet test TvShowsHub.sln
```