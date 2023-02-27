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

# to build, run:
# docker build --tag flight-plan-api .
#
# to run, run:
# docker run --rm -d --publish 5555:5000 --name flight-plan-api flight-plan-api
# or
# docker compose up
#
# on Windows 10, if you get the following error:
# docker: Error response from daemon: Ports are not available: exposing port TCP 0.0.0.0:5555 -> 0.0.0.0:0: listen tcp 0.0.0.0:5555: bind: An attempt was made to access a socket in a way forbidden by its access permissions.
# run these commands as admin:
# net stop winnat
# run your container
# net start winnat
