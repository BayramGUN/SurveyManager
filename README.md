# SurveyManager App

## Promotional & Explanation Video

```cmd
dotnet watch run --project src/Presentation/API/SurveyManager.Api --launch-profile https
```

### Documents of the project

>Please click the links below to see detailed documents!

- [Api](Documents/Api/Api.Auth.md)
  - [Auth](Documents/Api/Api.Auth.md#auth)
    - [Login](Documents/Api/Api.Auth.md#login)
    - [Register](Documents/Api/Api.Auth.md#register)

- [DomainModels](Documents/DomainModels)
  - [User Domain Model](Documents/DomainModels/Aggregates.User.md)
  - [Survey Domain Model](Documents/DomainModels/Aggregates.Survey.md)
  - [Answer Domain Model](Documents/DomainModels/Aggregates.Statistic.md)

## Technologies/Programming Languages, Frameworks & Libraries

- [Technologies/Programming Languages](#technologiesprogramming-languages)
  - [Dotnet Core](#dotnet-core)
  - [C#](#c)
  - [Pure JavaScript](#pure-javascript)
- [Frameworks](#frameworks)
  - [Entity Framework Core](#entity-framework-core)
  - [MediatR](#mediatr)
- [Libraries](#libraries)
  - [ErrorOr](#erroror)
  - [JWT Token](#jwt-token)
  - [Mapster](#mapster)

## Technologies/Programming Languages

### Dotnet Core

.NET is a free and open-source, managed computer software framework for Windows, Linux, and macOS operating systems. It is a cross-platform successor to .NET Framework. The project is mainly developed by Microsoft employees by way of the .NET Foundation, and released under an MIT License.

![Dotnet Core](https://www.typemock.com/wp-content/uploads/2022/03/Dotnetcore.png)

Source: [Dotnet Source](https://learn.microsoft.com/en-us/dotnet/)

### C\#

C# (pronounced "See Sharp") is a modern, object-oriented, and type-safe programming language. C# enables developers to build many types of secure and robust applications that run in .NET. C# has its roots in the C family of languages and will be immediately familiar to C, C++, Java, and JavaScript programmers.

![C#](https://static.gunnarpeipman.com/wp-content/uploads/2009/10/csharp-featured.png)

Source: [C# Source](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/)

### Pure JavaScript

JavaScript (JS) is a lightweight, interpreted, or just-in-time compiled programming language with first-class functions. While it is most well-known as the scripting language for Web pages, many non-browser environments also use it, such as Node.js, Apache CouchDB and Adobe Acrobat. JavaScript is a prototype-based, multi-paradigm, single-threaded, dynamic language, supporting object-oriented, imperative, and declarative (e.g. functional programming) styles.

![Pure JavaScript](https://upload.wikimedia.org/wikipedia/commons/thumb/6/6a/JavaScript-logo.png/800px-JavaScript-logo.png)

Source: [JS Source](https://developer.mozilla.org/en-US/docs/Web/JavaScript)

## Frameworks

### Entity Framework Core

Entity Framework (EF) Core is a lightweight, extensible, open source and cross-platform version of the popular Entity Framework data access technology.

EF Core can serve as an object-relational mapper (O/RM), which:

- Enables .NET developers to work with a database using .NET objects.

- Eliminates the need for most of the data-access code that typically needs to be written.

EF Core supports many database engines, see Database Providers for details.

![Entity Framework Core](data:image/png;base64,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)

Source: [EF Core Source](https://learn.microsoft.com/en-us/ef/core/)

### MediatR

In software engineering, the mediator pattern defines an object that encapsulates how a set of objects interact. This pattern is considered to be a behavioral pattern due to the way it can alter the program's running behavior. In object-oriented programming, programs often consist of many classes.

![MediatR](https://plugins.jetbrains.com/files/18313/342138/icon/pluginIcon.png)

Source: [MediatR Source](https://github.com/jbogard/MediatR)

## Libraries

### ErrorOr

Insert description of ErrorOr library here.

![ErrorOr](https://example.com/erroror.png)

Source: [Example Source](https://example.com/erroror-source)

### JWT Token

Insert description of JWT Token library here.

![JWT Token](https://example.com/jwt-token.png)

Source: [Example Source](https://example.com/jwt-token-source)

### Mapster

Insert description of Mapster library here.

![Mapster](https://example.com/mapster.png)

Source: [Example Source](https://example.com/mapster-source)
