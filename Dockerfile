# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copiar todo el repo dentro del contenedor de build
COPY . .

# Restaurar
RUN dotnet restore src/Booking.Api/Booking.Api.csproj

# Publicar
RUN dotnet publish src/Booking.Api/Booking.Api.csproj -c Release -o /app/publish

# Etapa final para runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app

COPY --from=build /app/publish .

# ASP.NET Core en contenedor escucha normalmente en 8080
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:7778

ENTRYPOINT ["dotnet", "Booking.Api.dll"]
