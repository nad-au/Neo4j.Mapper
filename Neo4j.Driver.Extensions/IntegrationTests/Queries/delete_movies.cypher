MATCH (person:Person)
MATCH (movie:Movie)
DETACH DELETE person, movie
