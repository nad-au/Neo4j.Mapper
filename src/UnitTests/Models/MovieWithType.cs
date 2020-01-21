namespace UnitTests.Models
{
    public class MovieWithType : Movie
    {
        public MovieType MovieType { get; set; }
    }

    public enum MovieType
    {
        Action,
        Drama,
        Romance
    }
}