# My Muse

#### My Muse is a cross-platform music streaming application built with .NET MAUI and ASP.NET Core (both .NET 10) and PostgreSQL. It provides a music discovery and playback experience inspired by modern music streaming platforms.

#### This project was created as a personal portfolio project to develop my skills in C#, ASP.NET Core, REST API design and .NET MAUI.

## Screenshots

| Home                       | Song page                       | Album page                       |
| -------------------------- | ------------------------------- | -------------------------------- |
| ![alt text](docs\home.jpg) | ![alt text](docs\song_page.jpg) | ![alt text](docs\album_page.jpg) |

| Search                            | Home (Windows)                     |
| --------------------------------- | ---------------------------------- |
| ![alt text](docs\search_page.jpg) | ![alt text](docs\home_desktop.png) |

## Supported Platforms

| Platform       | Status                                                  |
| -------------- | ------------------------------------------------------- |
| **Android**    | full support (primary target platform)                  |
| **Windows**    | Basic support; some layout adjustments are still needed |
| **iOS, macOS** | not tested                                              |

## Features

- **Music playback** – play songs directly from remote audio source.
- **Background and lock screen playback** — continue listening while the app is in the background or the Android device is locked.
- **Music discovery** — browse songs and albums on the home page in various sections. To refresh the page, swipe down from the top (on Windows, click the Refresh button in the top-right corner).
- **Searching** — search for songs, albums and artists. If you enter an artist’s or album’s name with sufficient accuracy, related tracks will also appear.
- **Search filters** — filter search results by content type: songs, albums or artists.
- **Song page** — control playback using the play/pause button or the slider, enable looping, and skip to the next or previous track.
- **Album page** — view album information and its tracks list, start playing from the first or randomly selected track.
- **Player bar** — access the current track and navigate to its details from different parts of the app.
- **Playback queue** — when song was selected from an album page, the queue contains remaining tracks from that album. In other cases, the queue contains randomly selected songs.
- **About page** — provides information about third-party asset licenses and attributions.

## Tech Stack

- ### Frontend
  - **C#**
  - **.NET MAUI (.NET 10)** — cross-platform UI
  - **MVVM architecture**
  - **NuGet packages:**
    - **CommunityToolkit.MVVM** — MVVM helpers and source generators
    - **CommunityToolkit.Maui** — additional MAUI components and utilities
    - **CommunityToolkit.MediaElement** — audio playback
  - The UI concept was designed in Figma with AI assistance.

- ### Backend (REST API)
  - **ASP.NET Core (.NET 10)**
  - **Controller-based**
  - **Repository Pattern**
  - **Entity Framework Core** — ORM, database access and migrations
  - **OpenAPI and Scalar** — API documentation and interactive endpoint exploration
  - **PostgreSQL**
  - **PostgreSQL's pg_trgm extension** — similarity-based searching

- ### Infrastructure and Tools
  - **Cloudflare R2** — remote audio and image storage.
  - **Northflank** — API and database hosting.
  - **Git & GitHub** — version control.

- ### Architecture
  - The codebase is organized by features, taking inspiration from Vertical Slice Architecture.
  - The backend uses the Repository Pattern to separate data access from other application logic.

## Installation

#### 1. Go to the [Releases page](https://github.com/Waldek4500/MyMuse/releases).

#### 2. Download the latest pre-release for your platform: Windows (.zip) or Android (.apk).

#### 3. Extract the archive and run the .exe file on Windows, or install the .apk file on your Android device. If you choose to install the .apk file, you might need to allow the installation of apps from unknown sources.

## Getting Started

These instructions describe how to run My Muse project in a local development environment.

### Prerequisites

- .NET 10 SDK
- Visual Studio with the .NET MAUI and ASP.NET Core workloads
- Android SDK and an Android emulator or device, or a Windows development environment
- A PostgreSQL database
- It's recommended to have remote storage for resources e.g. audio and images

### 1. Clone the repository

`https://github.com/Waldek4500/MyMuse.git`

### 2. Configure the database connection

Configure the MyMuse connection string for the ASP.NET Core API. For local development, use .NET User Secrets or an appropriate local configuration file e.g. appsettings.json.

The corresponding configuration key is:

```
"ConnectionStrings": {
     "MyMuse": "Host=localhost;Port=5432;Database=YOUR_DATABASE;Username=YOUR_USERNAME;Password=YOUR_PASSWORD;"
}
```

Do not commit actual passwords, credentials or production connection strings to the repository.

### 3. Apply database migrations

The API is configured to apply Entity Framework Core migrations at startup through `app.MigrateDb();` in `Program.cs`.

If the migrations are not applied automatically, run the following command from the API project directory:

`dotnet ef database update`

If the dotnet-ef tool is not installed, see the [EF Core CLI installation guide](https://learn.microsoft.com/en-us/ef/core/cli/dotnet).

### 4. Populate the database

The application requires data such as artists, albums and songs.

You can run the SQL query from `docs/mymuse_db.sql` in your database to populate it with some sample data to work with.

The database also needs valid URLs for the remote audio files and images used by the application.

### 5. Configure the API address

In MauiProgram.cs, configure the API BaseAddress to point to an address reachable from your target platform.

The ASP.NET Core API is configured to listen on 0.0.0.0 on port 5010, allowing connections from other devices on the network.

For example:

- **Windows:** Use `localhost` if the API runs on the same computer.

- **Android emulator:** Use `10.0.2.2` to access the host computer from the emulator. The `AndroidManifest.xml` is configured with `android:usesCleartextTraffic="true"` to allow HTTP connections during development.

- **Physical Android device (and previous ones):** Use your computer's local network IP address and ensure both devices are connected to the same network.

Example: `http://10.0.2.2:5010`

### 6. Run the application

Run the ASP.NET Core API and build the .NET MAUI app on your platform (windows or android recommended).

## Project Status

My Muse is an ongoing personal portfolio project focused primarily on Android. Windows support is available but the layout is not fully polished. iOS and macOS have not been tested.

The project may contain bugs, and future improvements are not guaranteed.

## Missing Improvements

- User accounts and authentication
- Artist page
- User's music library
- Song's options
- Searching by song genre

## License

The original source code developed for My Muse is licensed under the MIT License.

Third-party assets, including music tracks, album artwork, images, fonts and icons, are excluded from this license and remain subject to their respective licenses and copyright terms.

See `THIRD-PARTY-NOTICES.md` for information about third-party assets and their applicable licenses.
