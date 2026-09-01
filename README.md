# TravelPlanner

Konsolowa aplikacja do planowania podróży. Wyszukuje miejsce, pobiera aktualne dane pogodowe, a następnie proponuje atrakcje i elementy ekwipunku dopasowane do warunków oraz preferencji użytkownika.

## Najważniejsze funkcje

- tworzenie i zapisywanie podróży w lokalnej bazie SQLite;
- wyszukiwanie miejsc i atrakcji przez Google Places API;
- pobieranie pogody z OpenWeather API;
- rekomendowanie atrakcji na podstawie pogody, pory dnia i zainteresowań użytkownika;
- rekomendowanie wyposażenia plecaka na podstawie warunków pogodowych;
- wyświetlanie i usuwanie zapisanych podróży;
- testy jednostkowe mechanizmu oceny atrakcji.

## Architektura

Projekt jest podzielony na warstwy zgodne z zasadami Clean Architecture:

```text
src/
├── TravelPlanner.Domain/          # modele i czysta logika rekomendacji
│   └── Models/
│       ├── Place.cs
│       ├── TripContext.cs
│       ├── TravelContextEngine.cs
│       ├── BackPackContext.cs
│       └── BackpackContextEngine.cs
├── TravelPlanner.Application/     # scenariusze aplikacji i obsługa planu podróży
├── TravelPlanner.Infrastructure/  # SQLite oraz komunikacja z API pogody
│   ├── Persistence/
│   └── Weather/
└── TravelPlanner.Cli/             # interfejs konsolowy

Tests/                             # testy jednostkowe xUnit
```

### System rekomendacji

`ContextEngine` ocenia atrakcje na podstawie kontekstu podróży:

- podczas deszczu promuje miejsca wewnątrz, np. muzea, galerie i kawiarnie;
- przy słonecznej, ciepłej pogodzie promuje atrakcje na zewnątrz, np. parki i plaże;
- rano zwiększa ocenę kawiarni oraz piekarni;
- zgodność z zainteresowaniami użytkownika zwiększa wynik atrakcji.

`BackpackContextEngine` tworzy listę wyposażenia, np. parasol i kurtkę przeciwdeszczową podczas deszczu albo krem z filtrem przy wysokiej temperaturze.

## Konfiguracja

W katalogu głównym utwórz plik `.env`:

```env
GOOGLE_PLACES_API_KEY=twoj_klucz_google_places
WEATHER_API_KEY=twoj_klucz_openweather
```

Plik `.env` jest ignorowany przez Git i nie powinien zawierać kluczy udostępnianych publicznie.

## Uruchamianie

Wymagany jest .NET SDK 10.

Projekt zawiera plik rozwiązania `TravelPlanner.slnx` (domyślny format rozwiązania dla .NET 10), który obejmuje wszystkie projekty aplikacji oraz testy.

```bash
dotnet restore TravelPlanner.slnx
dotnet run --project src/TravelPlanner.Cli/TravelPlanner.Cli.cs.csproj
```

## Testy jednostkowe

Testy są napisane w xUnit. Sprawdzają między innymi, czy ocena atrakcji jest niższa dla miejsc plenerowych podczas deszczu oraz wyższa dla nich w słoneczny, ciepły dzień.

```bash
dotnet test TravelPlanner.slnx
```
