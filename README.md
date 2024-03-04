The main purpose of this project was to play around with AWS, Terraform, Docker and Kubernetes from scratch. Some unit tests as a bonus.

# Pokedex

This project was coded with [VS Code](https://code.visualstudio.com/) version 1.63.0 and [.Net Core](https://dotnet.microsoft.com/en-us/download/dotnet/3.1) version 3.1.413

In order to run the application locally, go to the root folder and run `dotnet run --project .\Pokedex.API\`. Alternatively, open the project in VS Code and press F5

The api has two endpoints:

`/{name}` to receive information of the given Pokemon.
`/translated/{name}` to receive the pokemon with a funny description depending on its habitat.

## Docker

The application is also [dockerised](https://www.docker.com/get-started).
In order to run a dockerised version of the API, go to the project root folder and run `docker build -t <name-you-wish> .\Pokedex.API\` to create the image. Run
`docker run -p 80:80 -it <name-you-wish>` to start it.

## Test

I have tested the app using [Postman](https://www.postman.com/) and [Firefox Browser](https://www.mozilla.org/en-GB/firefox/new/).
The base url will be <http://localhost> (<http://localhost/pokemon/translated/mewtwo> for example) in the case of the dockerised app, and <http://localhost:5000> or <https://localhost:5001> (<http://localhost:5000/pokemon/translated/mewtwo> for example) running locally.

Alternatively, it can be tested using Swagger. In the web browser, go to <http://localhost:5000/swagger/index.html> or <http://localhost/swagger/index.html> if the app is running using docker

## Running unit tests

Run `dotnet test .\Pokedex.API.Test\` in the root folder to execute the unit tests.
