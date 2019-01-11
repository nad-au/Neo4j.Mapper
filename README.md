# Neo4jMapper
### Build Status
[![Build Status](https://travis-ci.org/barnardos-au/Neo4jMapper.svg?branch=master)](https://travis-ci.org/barnardos-au/Neo4jMapper)
[![Build status](https://ci.appveyor.com/api/projects/status/lm9w5ro0735kyi45/branch/master?svg=true)](https://ci.appveyor.com/project/neildobson-au/neo4jmapper/branch/master)
### What is Neo4jMapper?
A library to simplify mapping of cypher values onto your models
### Minimum Viable Snippet
```csharp
var result = Session.Run(@"
  MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
  WITH person, COLLECT(movie) AS movies
  RETURN person, movies");

var actor = result.Map<Person, IEnumerable<Movie>, Person>((person, movies) =>
{
  person.MovesActedIn = movies;
  return person;
}).SingleOrDefault();

Assert.IsNotNull(actor);
Assert.AreEqual("Cuba Gooding Jr.", actor.name);
Assert.AreEqual(1968, actor.born);
Assert.AreEqual(4, actor.MovesActedIn.Count());
```
