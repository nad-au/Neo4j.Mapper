MATCH (film:Film)
MATCH (people:People)
MATCH (vehicle:Vehicle)
DETACH DELETE film, people, vehicle
