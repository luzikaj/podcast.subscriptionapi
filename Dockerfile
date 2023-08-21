FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY ./Podcast.SubscriptionApi ./
RUN dotnet restore
RUN dotnet publish -c Relase -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENV TZ='Europe/Warsaw'
EXPOSE 80
ENTRYPOINT [ "dotnet", "Podcast.SubscriptionApi.dll" ]