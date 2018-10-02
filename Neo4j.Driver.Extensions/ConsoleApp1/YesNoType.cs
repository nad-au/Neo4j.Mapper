using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1
{
    public enum YesNoType
    {
        [Display(Order = 1)]
        Yes,

        [Display(Order = 2)]
        No
    }
}
