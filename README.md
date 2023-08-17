# SurveyManager App

>Run Command

```cmd
dotnet watch run --project src/Presentation/API/SurveyManager.Api --launch-profile https
```

## Promotional & Explanation Video

> Usage video
[![Video](https://cdn.loom.com/sessions/thumbnails/324a0ac35a9042dab20d18161fd3805c-with-play.gif)](https://www.loom.com/share/324a0ac35a9042dab20d18161fd3805c)

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
  - [FluentValidation](#fluentvalidation)
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

![Entity Framework Core](https://codeopinion.com/wp-content/uploads/2017/10/Bitmap-MEDIUM_Entity-Framework-Core-Logo_2colors_Square_Boxed_RGB.png)

Source: [EF Core Source](https://learn.microsoft.com/en-us/ef/core/)

### MediatR

In software engineering, the mediator pattern defines an object that encapsulates how a set of objects interact. This pattern is considered to be a behavioral pattern due to the way it can alter the program's running behavior. In object-oriented programming, programs often consist of many classes.

![MediatR](./Documents/Images/pluginIcon.png)

Source: [MediatR Source](https://github.com/jbogard/MediatR)

### FluentValidation

Fluent Validation is a validation library for .NET, used for building strongly typed validation rules for business objects.

![FluentValidation](./Documents/Images/FluentVald.png)

Source: [FluentValidation Source](https://docs.fluentvalidation.net/en/latest/aspnet.html)

## Libraries

### ErrorOr

This library is an error handling mechanism. It based on [OneOf](https://github.com/mcintyre321/OneOf).

![ErrorOr](./Documents/Images/icon.png)

Source: [ErrorOr Source](https://github.com/amantinband/error-or)

### JWT Token

JSON Web Token is a proposed Internet standard for creating data with optional signature and/or optional encryption whose payload holds JSON that asserts some number of claims. The tokens are signed either using a private secret or a public/private key.

![JWT Token](./Documents/Images/jwt.png)

Source: [JWT Token Source](https://jwt.io/introduction)

### Mapster

As the name suggests, Mapster is a library that maps one object type to a different object type. It is a convention-based mapper that is easy to configure and use. Writing code to map one object to another can be very repetitive and boring. Because of this, Mapster frees us from writing error-prone boilerplate code.

In most cases, we shouldn’t try to reinvent the wheel but use the available options.

Let’s find out if Mapster is a good option.

![Mapster](./Documents/Images/mapster.png)

Source: [Mapster Source](https://code-maze.com/mapster-aspnetcore-introduction/)
