using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.Test;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

public class Person
{
    [Required]
    public string Name { get; set; }

    [Required]
    [MaxLength(6)]
    [MinLength(3)]
    public string Id { get; set; }

    [Range(1, 100)]
    public int Age { get; set; }
}


public class ValidationDemo
{
    public void run()
    {
        // Create a new Person object
        Person person = new Person { Name = "", Id="aa", Age = 200 };

        // Validate the object
        var results = new List<ValidationResult>();
        var context = new ValidationContext(person);
        bool isValid = Validator.TryValidateObject(person, context, results,true);


        Dictionary<string, string[]> dic = new Dictionary<string, string[]>();

        // Check if validation succeeded
        if (!isValid)
        {
            // Validation failed
            foreach (var validationResult in results)
            {
                string key = validationResult.MemberNames.FirstOrDefault();
                string[] values = new string[] { validationResult.ErrorMessage };
                dic.Add(key, values);
            }
            
        }

        ValidationProblemDetails details = new ValidationProblemDetails(dic);
    }
}



