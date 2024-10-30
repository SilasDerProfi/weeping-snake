# Weeping Snake


![Build](https://github.com/SilasDerProfi/weeping-snake/workflows/Build/badge.svg)
![Test](https://github.com/SilasDerProfi/weeping-snake/workflows/Test/badge.svg)
![Lines of Code](https://img.shields.io/tokei/lines/github/SilasDerProfi/weeping-snake)


## What is Weeping Snake?

Weeping Snake is a kind of multiplayer snake. All details are in the [technical_documentation.pdf](technical_documentation.pdf) (only in german language, because the repo was created within a lecture at a german university).

Basically, this repo is a kind of coding dojo for the course Advanced Software Engineering at the [Baden-Wuerttemberg Cooperative State University Karlsruhe](https://www.karlsruhe.dhbw.de/en/general).

## Getting Started

1. Make sure that dotnet is installed on your machine. You need it to `build`, `test` and `run` the code
   - To **install dotnet** for example with pacman you can use `sudo pacman -S dotnet-sdk`
   - It is also possible to use **Docker** via the included [Dockerfile](src/WeepingSnake.Game/Dockerfile) to run the program, but no interaction is possible (e.g. controlling your own player)

2. You need the source code. Just load it via the git clone command
   - E.g. `git clone https://github.com/SilasDerProfi/weeping-snake.git`
   - Obviously you can specify a specific directory for the clone

### Build

With every commit the code is compiled automatically. You can see if the build was successful by the badge in this readme.

To build the code, you must run `dotnet build src` in the directory of your clone

### Test

With every commit the code is tested automatically. You can see if the test were successful by the badge in this readme.

To build the code, you must run `dotnet test src` in the directory of your clone


### Run

To run the code, you need to run `dotnet run --project src/WeepingSnake.ConsoleClient` in the directory of your clone

## A Gag

What do you call a school director who teaches computer science? - Programming Principal
