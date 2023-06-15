using System;

namespace PromoCodeFactory.WebHost.Models;

public class EmployeeUpdateVm
{

    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

}