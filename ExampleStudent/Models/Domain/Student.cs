using System;
using System.Collections.Generic;

namespace ExampleStudent.Models.Domain;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Birth { get; set; }

    public bool Gender { get; set; }

    public string ImgUrl { get; set; } = null!;

    public string Mssv { get; set; } = null!;

    public string? Description { get; set; }
}
