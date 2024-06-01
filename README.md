# powerplant-coding-challenge

## Build and launch

### Using Visual Studio

Open the solution in Visual Studio. Build and run the included launch profile (http) directly from Visual Studio.

### From the CLI

Make sure you have the dotnet CLI tools installed.

To build and run from the command line interface, execute the following command:

`dotnet run --project .\PowerPlant.Api`



## Docker instructions

Make sure you have Docker installed.

First, build the image. Run this from the solution root:

`docker build . -f .\PowerPlant.Api\Dockerfile -t powerplantapi`

After successfully building the image, you can run it in a container:

`docker run -p 8888:8080 powerplantapi`