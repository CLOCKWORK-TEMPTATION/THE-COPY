# Blueprint: TheCopy

## Overview

TheCopy is a web application designed to allow users to manage and share code snippets, scripts, and project templates. It provides a collaborative space for developers to store, discover, and reuse code.

## Features & Design

*   **Authentication:** Users can register and log in to the application.
*   **Project Management:** Users can create, view, and manage their projects.
*   **Script Management:** Users can add, edit, and delete scripts within their projects.
*   **AI-Powered Assistance:** The application integrates with a generative AI service to provide assistance.
*   **Clean Architecture:** The project follows a clean architecture pattern, separating concerns into Domain, Application, Infrastructure, and Presentation layers.
*   **Frontend:** The client-side is built with Blazor WebAssembly.
*   **Backend:** The server-side is built with ASP.NET Core Web API.
*   **Database:** The application uses PostgreSQL for relational data and MongoDB for other data storage needs.

## Current Task

**Goal:** Initial project setup and fixing build errors.

**Plan:**

1.  [x] Resolve duplicated assembly attribute errors by removing the rogue `myapp.csproj` file.
2.  [x] Clean the solution by deleting `bin` and `obj` folders.
3.  [x] Fix package downgrade errors by aligning package versions for `Npgsql.EntityFrameworkCore.PostgreSQL` and `MongoDB.Driver` across all projects.
4.  [x] Restore NuGet packages for the entire solution.
5.  [x] Run the `TheCopy.Server` project to host the backend and serve the Blazor client application.
6.  [ ] Verify that the application is running correctly in the browser.
