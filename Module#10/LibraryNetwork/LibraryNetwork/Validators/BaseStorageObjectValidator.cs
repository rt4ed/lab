using LibraryNetwork.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryNetwork.Classes
{
    public class BaseStorageObjectValidator : IValidation
    {
        public bool IsValid<T>(T obj, out ICollection<ValidationResult> results)
            where T : BaseStorageObject
        {
            results = new List<ValidationResult>();

            if (obj is null)
            {
                results.Add(new ValidationResult("Value cannot be null!"));
                return false;
            }

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }
    }
}
