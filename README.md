# TravelPlanner

A console application for travel planning. It searches for a location, retrieves current weather data, and then suggests attractions and equipment items tailored to the conditions and user preferences.

## Key Features

- creating and saving trips in a local SQLite database;
- searching for places and attractions via the Google Places API;
- fetching weather data from the OpenWeather API;
- recommending attractions based on weather, time of day, and user interests;
- recommending backpack equipment based on weather conditions;
- displaying and deleting saved trips;
- unit tests for the attraction scoring mechanism.

## Architecture

The project is divided into layers following Clean Architecture principles:

```text
src/
├── TravelPlanner.Domain/          # models and pure recommendation logic
│   └── Models/
│       ├── Place.cs
│       ├── TripContext.cs
│       ├── TravelContextEngine.cs
│       ├── BackPackContext.cs
│       └── BackpackContextEngine.cs
├── TravelPlanner.Application/     # application use cases and trip plan handling
├── TravelPlanner.Infrastructure/  # SQLite and weather API communication
│   ├── Persistence/
│   └── Weather/
└── TravelPlanner.Cli/             # console interface

Tests/                             # xUnit unit tests
```

### Recommendation System

`ContextEngine` scores attractions based on the trip context:

- during rain, it promotes indoor places, e.g. museums, galleries, and cafes;
- in sunny, warm weather, it promotes outdoor attractions, e.g. parks and beaches;
- in the morning, it boosts the rating of cafes and bakeries;
- matching the user's interests increases the attraction's score.

`BackpackContextEngine` creates a list of equipment, e.g. an umbrella and a rain jacket during rain, or sunscreen in high temperatures.

## Configuration

In the root directory, create a `.env` file:

```env
GOOGLE_PLACES_API_KEY=your_google_places_key
WEATHER_API_KEY=your_openweather_key
```

The `.env` file is ignored by Git and should not contain publicly shared keys.

## Running

.NET SDK 10 is required.

The project contains the solution file `TravelPlanner.slnx` (the default solution format for .NET 10), which includes all application projects and tests.

```bash
dotnet restore TravelPlanner.slnx
dotnet run --project src/TravelPlanner.Cli/TravelPlanner.Cli.cs.csproj
```

## Unit Tests

The tests are written in xUnit. They check, among other things, whether the attraction score is lower for outdoor places during rain and higher for them on a sunny, warm day.

```bash
dotnet test TravelPlanner.slnx
```
