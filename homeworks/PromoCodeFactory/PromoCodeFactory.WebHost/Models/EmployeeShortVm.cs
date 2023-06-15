using System;

namespace PromoCodeFactory.WebHost.Models;

public class EmployeeShortVm
{
    public Guid Id { get; set; }
    
    public string FullName { get; set; }

    public string Email { get; set; }
}