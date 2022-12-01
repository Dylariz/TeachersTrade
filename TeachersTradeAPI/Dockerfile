FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TeachersTrade/TeachersTrade.csproj", "TeachersTrade/"]
RUN dotnet restore "TeachersTrade/TeachersTrade.csproj"
COPY . .
WORKDIR "/src/TeachersTrade"
RUN dotnet build "TeachersTrade.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeachersTrade.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeachersTrade.dll"]
