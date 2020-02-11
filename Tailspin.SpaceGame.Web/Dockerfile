# set the .net core sdk image for the build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
# define the working directory
WORKDIR /source

# copy csproj
COPY Tailspin.SpaceGame.Web.csproj ./Tailspin.SpaceGame.Web/
# copy everything else
COPY . ./Tailspin.SpaceGame.Web/

# set the working directory as a project directory
WORKDIR /source/Tailspin.SpaceGame.Web
# restore dependencies and tools for the project in the current directory showing normal output.
RUN dotnet restore --verbosity normal

# builds and publishes the compiled application
RUN dotnet publish -c Release -o /app --no-restore

# final stage/image - pulls slim asp.core image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
# copy the compiled content from the build image to the runtime
COPY --from=build /app ./
EXPOSE 5004:80
# set the application entry point
ENTRYPOINT ["dotnet", "Tailspin.SpaceGame.Web.dll"]