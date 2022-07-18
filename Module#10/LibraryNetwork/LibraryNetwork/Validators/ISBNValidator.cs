using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryNetwork.Validators
{
    public class ISBNValidator: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string userName)
            {
                if (userName != "admin")    // если имя не равно admin
                    return true;
                else
                    ErrorMessage = "Некорректное имя: admin";
            }
            return false;
        }
    }
}
