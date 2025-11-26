#!/bin/bash

# Script to run both Server and Client together

echo "🚀 Starting TheCopy Application..."
echo ""

# Kill any existing dotnet processes on these ports
echo "🧹 Cleaning up any existing processes..."
lsof -ti:5126 | xargs kill -9 2>/dev/null || true
lsof -ti:5000 | xargs kill -9 2>/dev/null || true

# Start Server in background
echo "🔧 Starting Server (Backend API)..."
cd TheCopy.Server
dotnet run --urls "http://localhost:5126" > ../server.log 2>&1 &
SERVER_PID=$!
cd ..

# Wait for server to start
echo "⏳ Waiting for server to start..."
sleep 5

# Start Client
echo "🎨 Starting Client (Blazor WebAssembly)..."
cd TheCopy.Client
dotnet watch run --urls "http://localhost:5000"

# Cleanup on exit
trap "kill $SERVER_PID 2>/dev/null" EXIT
