using LibraryNetwork.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryNetwork.Classes
{
    public class NullObjectValidator : IValidation
    {
        public bool IsValid<T>(T obj, out ICollection<ValidationResult> results) where T : BaseStorageObject
        {
            results = new List<ValidationResult>();

            if (obj is null)
            {
                return false;
            }

            return true;
        }
    }
}
