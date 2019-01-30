# Neo4jMapper
### Build Status
[![Build Status](https://travis-ci.org/barnardos-au/Neo4jMapper.svg?branch=master)](https://travis-ci.org/barnardos-au/Neo4jMapper)
[![Build status](https://ci.appveyor.com/api/projects/status/lm9w5ro0735kyi45/branch/master?svg=true)](https://ci.appveyor.com/project/neildobson-au/neo4jmapper/branch/master)
### What is Neo4jMapper?
A .NET Standard 2.0 library to simplify mapping of cypher values onto your models.
### Minimum Viable Snippet
```csharp
var cursor = await Session.RunAsync(@"
  MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
  RETURN person, COLLECT(movie) AS movies");

var actor = await cursor
  .MapSingleAsync((Person person, IEnumerable<Movie> movies) =>
  {
    person.MoviesActedIn = movies;
    return person;
  });

Assert.AreEqual("Cuba Gooding Jr.", actor.name);
Assert.AreEqual(1968, actor.born);
Assert.AreEqual(4, actor.MoviesActedIn.Count());
```
### Features

 - Simple Map API which extends Neo4j Driver's [IRecord](https://github.com/neo4j/neo4j-dotnet-driver/blob/1.7/Neo4j.Driver/Neo4j.Driver/V1/IRecord.cs) to greatly simplify the extraction and projection of cypher values onto your entity models.
 - Works asynchronously via [IStatementResultCursor](https://github.com/neo4j/neo4j-dotnet-driver/blob/1.7/Neo4j.Driver/Neo4j.Driver/V1/IStatementResultCursor.cs) methods or synchronously via [IStatementResult](https://github.com/neo4j/neo4j-dotnet-driver/blob/1.7/Neo4j.Driver/Neo4j.Driver/V1/IStatementResult.cs).
 - Automatic mapping of node Ids to your entity models, and GetNode & SetNode helper methods to simplify writing CRUD methods.
 - Custom converters to facilitate conversion between Neo4j Driver's temporal data types such as [ZonedDateTime](https://github.com/neo4j/neo4j-dotnet-driver/blob/1.7/Neo4j.Driver/Neo4j.Driver/V1/Types/ZonedDateTime.cs) and .NET CLR types such as DateTimeOffset.
 - Neo4jParameters wrapper and Dictionary extension methods to assist in creating parameter maps when performing cypher updates.
 - Superior performance due to the use of best-in-class mapper [ServiceStack.Text](https://github.com/ServiceStack/ServiceStack.Text). Benchmarks show queries in excess of 4x quicker than [Neo4jClient](https://github.com/Readify/Neo4jClient).
 - Targets .NET Standard 2.0 for use in full .NET Framework or cross-platform .NET Core projects.
 - 100% Code Coverage.
 - Benchmarks.
 - Examples.
### Getting Started
The easiest way to add Neo4jMapper to your .NET project is to use the NuGet Package Manager.
#### With Visual Studio IDE
From Visual Studio use the Nuget Package Manager to browse to the Neo4jMapper package and install it to your project. Alternatively, you can use the Package Manager Console: 
````powershell
Install-Package Neo4jMapper
````
#### Using .NET Core Common Language Runtime (CoreCLR)
If you are developing .NET Core projects and you are using the command line tools, then you can run the following command from within your project directory:
````powershell
dotnet add package Neo4jMapper
````
### Working with Neo4jMapper
See [neo4jmapper.tk](https://www.neo4jmapper.tk/) for comprehensive usage information.
### Obtaining Help & Support
For general usage help please ask a question on [StackOverflow](https://stackoverflow.com/questions/tagged/neo4jmapper). To report a bug, please raise an issue. To help make this software even better, please fork the repository, add your changes and raise a pull request.
