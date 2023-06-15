using System;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models;

public class EmployeeVm
{
    public Guid Id { get; set; }
    
    public string FullName { get; set; }

    public string Email { get; set; }

    public List<RoleItemVm> Roles { get; set; }

    public int AppliedPromocodesCount { get; set; }
}