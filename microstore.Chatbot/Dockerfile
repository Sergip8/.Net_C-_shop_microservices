FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["microstore.Chatbot/microstore.Chatbot.csproj", "microstore.Chatbot/"]
RUN dotnet restore "microstore.Chatbot/microstore.Chatbot.csproj"
COPY . .
WORKDIR "/src/microstore.Chatbot"
RUN dotnet build "microstore.Chatbot.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "microstore.Chatbot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "microstore.Chatbot.dll"]
