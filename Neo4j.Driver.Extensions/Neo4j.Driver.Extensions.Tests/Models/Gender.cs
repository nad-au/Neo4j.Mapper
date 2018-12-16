using System.ComponentModel.DataAnnotations;

namespace Neo4j.Driver.Extensions.Tests.Models
{
    public enum Gender
    {
        [Display(Order = 2)] 
        Male,

        [Display(Order = 1)]
        Female,

        [Display(Order = 4)] 
        Unknown,

        [Display(Order = 3, Name = "Indeterminate/Intersex/Unspecified")]
        IndeterminateIntersexUnspecified
    }
}
