# Multi-stage build 
# Etapa 1: Compilación (Build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos los archivos del proyecto
# dotnet restore => descargar e instalarlas dependencias.
COPY src/AdminTasks.Backend.Api/*.csproj ./AdminTasks.Backend.Api/
RUN dotnet restore AdminTasks.Backend.Api/AdminTasks.Backend.Api.csproj

# Copiamos el resto del código y compilamos
# RUN dotnet publish -c => Indica al compilador que aplique todas las optimizaciones de código.
# -o /app => Especifica el directorio de salida para los archivos publicados.
COPY . .
RUN dotnet publish src/AdminTasks.Backend.Api/AdminTasks.Backend.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Etapa de ejecución (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiamos desde la etapa anterior
COPY --from=build /app/publish .

# Puerto expuesto (el mismo que usa Kestrel por defecto)
EXPOSE 8080

# Define el comando principal
ENTRYPOINT ["dotnet", "AdminTasks.Backend.Api.dll"]