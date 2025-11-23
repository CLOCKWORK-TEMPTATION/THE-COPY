# Blueprint

## Overview

This document outlines the project's purpose, capabilities, style, design, and features. It also tracks the plan and steps for requested changes.

## Project Details

### Purpose and Capabilities

This is a .NET web application built with ASP.NET Core. The project is designed for development within the Firebase Studio environment. It leverages a "Fat Model, Skinny Controller" approach, with a focus on creating a robust and scalable application.

### Style, Design, and Features

*   **Backend:** C#, ASP.NET Core, Entity Framework Core
*   **Frontend:** Razor Pages/MVC
*   **Database:** Code-First approach with EF Core Migrations
*   **Dependency Management:** NuGet for .NET, npm for frontend (if applicable)
*   **Configuration:** `appsettings.json` and User Secrets
*   **Development Environment:** Nix-based environment defined in `dev.nix`

## Current Change Request

### Plan (Reverted... again)

1.  **Initial Goal:** Install the Doppler CLI.
2.  **First Attempt:** Added `pkgs.doppler-cli` to `dev.nix` with the `stable` channel. This caused an environment build failure.
3.  **First Rollback:** Reverted the changes to `dev.nix`.
4.  **Second Attempt:** Changed the channel in `dev.nix` to `unstable` and re-added `pkgs.doppler-cli`. This also caused an environment build failure.
5.  **Second Rollback:** Reverted the changes to `dev.nix` again to restore a stable working environment.

### Steps

- [x] ~~Attempt to install `doppler-cli` via `dev.nix` on `stable` channel.~~ (Failed)
- [x] Revert `dev.nix`.
- [x] ~~Attempt to install `doppler-cli` via `dev.nix` on `unstable` channel.~~ (Failed)
- [x] Revert `dev.nix` to original state.
- [x] Update `blueprint.md` to document the process.
