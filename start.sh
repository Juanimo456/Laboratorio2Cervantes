#!/bin/bash
set -e

# Publicar la app .NET en modo Release
dotnet publish -c Release -o out

# Ejecutar la app publicada
dotnet out/Tiendita.dll