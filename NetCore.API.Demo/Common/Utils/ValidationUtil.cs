using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils;

public static class ValidationUtil
{
    public static bool IsValid(object obj,out ValidationProblemDetails problemDetails)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(obj);
        
        bool isValid = Validator.TryValidateObject(obj, context, results, true);

        Dictionary<string, string[]> dic = new Dictionary<string, string[]>();
        if (!isValid)
        {
            foreach (var validationResult in results)
            {
                string key = validationResult.MemberNames.FirstOrDefault();
                string[] values = new string[] { validationResult.ErrorMessage };
                dic.Add(key, values);
            }
        }
        problemDetails = new ValidationProblemDetails(dic);
        return isValid;
    }
}
