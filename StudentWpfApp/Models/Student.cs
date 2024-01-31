using System;
using System.Collections.Generic;

namespace StudentWpfApp.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; } = 0;
}
