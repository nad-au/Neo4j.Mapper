# Neo4j.Mapper
### What is Neo4j.Mapper?
Formally known as `Neo4jMapper`, Neo4j.Mapper is a .NET6.0 library to simplify mapping of cypher values onto your models.
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
 - Works asynchronously via [IStatementResultCursor](https://github.com/neo4j/neo4j-dotnet-driver/blob/1.7/Neo4j.Driver/Neo4j.Driver/V1/IStatementResultCursor.cs) methods.
 - Automatic mapping of node Ids to your entity models, and GetNode & SetNode helper methods to simplify writing CRUD methods.
 - Custom converters to facilitate conversion between Neo4j Driver's temporal data types such as [ZonedDateTime](https://github.com/neo4j/neo4j-dotnet-driver/blob/1.7/Neo4j.Driver/Neo4j.Driver/V1/Types/ZonedDateTime.cs) and .NET CLR types such as DateTimeOffset.
 - Neo4jParameters wrapper and Dictionary extension methods to assist in creating parameter maps when performing cypher updates.
 - Superior performance due to the use of best-in-class mapper [ServiceStack.Text](https://github.com/ServiceStack/ServiceStack.Text).
 - Targets .NET6.0.
 - Examples.
### Getting Started
The easiest way to add Neo4j.Mapper to your .NET project is to use the NuGet Package Manager.
#### With Visual Studio IDE
From Visual Studio use the Nuget Package Manager to browse to the Neo4j.Mapper package and install it to your project. Alternatively, you can use the Package Manager Console: 
```powershell
Install-Package Neo4j.Mapper
```
#### Using .NET Core Common Language Runtime (CoreCLR)
If you are developing .NET Core projects and you are using the command line tools, then you can run the following command from within your project directory:
```powershell
dotnet add package Neo4j.Mapper
```
### Integration Tests
Use Neo4j Docker image for testing
```sh
docker run -p7474:7474 -p7687:7687 -e NEO4J_AUTH=neo4j/s3cr3t neo4j
```
### Obtaining Help & Support
For general usage help please ask a question on [StackOverflow](https://stackoverflow.com/questions/tagged/neo4j.mapper). To report a bug, please raise an issue. To help make this software even better, please fork the repository, add your changes and raise a pull request.
