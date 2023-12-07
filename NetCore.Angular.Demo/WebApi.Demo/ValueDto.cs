﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Demo;

public class ValueDto
{
    [Required]
    [StringLength(10, ErrorMessage = "Should shorter than 10 chars")]
    public string Name { get; set; }
    [Required]
    public string Value { get; set; }
}
