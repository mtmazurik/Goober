# .NET Core 2.1 C# RA (Reference Architecture) for a Microservice 

Descr: goober is a microservice scaffold, a template, written in C# and ASPNET Core 2.1 (or higher)

      "goober" is chosen (which means, "peanut" ), is simply a visible name to provide "high contrast", in the naming conventions in the project.

	  Ideally search and replace for goober, from the scaffold, and some directory name changes, or solution files will allow
	  you to "autoname" the skeletal scaffold of the microservice.  It is hoped that VS templating will also aid in this process.

# Getting Started
1.	Installation process: currently just copy this to project directory to use as a baseline project, in that the RA contains useful stuff.
2.	Software dependencies: should auto-install any packages base on the solution referencing what it needs
3.	Latest release:  0.1.0 (beta)
4.	API references: two   /ping   and  /version are implemented in the controller

# Build and Test
Run locally, with Docker for Windows (default), or remove dockerfile and it will run under IIS (express).    
Either launched directly from Visual Studio Debug\Start (F5).

# Contributors
Created - mtm; 9/29/2018 - tested three api calls    /ping   /version /recipe/peanutbutter (throws NYI)
