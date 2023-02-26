FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS prod-env
WORKDIR /App
COPY --from=build-env /App/out .

# Set the internal port to 5000 (from the default 80 set by dotnet)
ENV ASPNETCORE_URLS=http://+:5000

# Set the port which the container will listen to
EXPOSE 5555
ENTRYPOINT ["dotnet", "FlightPlanAPI.dll"]