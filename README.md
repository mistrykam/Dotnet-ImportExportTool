# Clean Architecture - Import Export Console Tool
Console-based import/export application built using the Clean Architecture pattern. 

The Core will contain entities and interfaces.  The Application will contain the business logic and uses the Core.  All dependencies are injected via the constructor dependency injection framework.

The application will read data from a GIT repository and a JSON Placeholder repository and export the data to a CSV file.

There are two infrastructure components:
* DataAccess - Git API / JSON Placeholder
* File System to write out the CSV file

# Frameworks
.NET Core 3.1 / C# 

# Dependencies
Microsoft.Extensions.DependencyInjection
Microsoft.Extensions.Configuration
Microsoft.Extensions.Logging
Serilog

# Testing
Microsoft.VisualStudio.TestTools.UnitTesting

