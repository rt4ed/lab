using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryNetwork.Interfaces
{
    internal interface IValidation
    {
        bool IsValid<T>(T obj, out ICollection<ValidationResult> results)
            where T: BaseStorageObject;
    }
}
