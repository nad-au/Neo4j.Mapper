using System.ComponentModel.DataAnnotations;

namespace Neo4j.Driver.Extensions.Tests.Models
{
    public enum YesNoType
    {
        [Display(Order = 1)]
        Yes,

        [Display(Order = 2)]
        No
    }
}
