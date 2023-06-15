using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers;

/// <summary>
/// Сотрудники
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController
    : ControllerBase
{
    private readonly IRepository<Employee> _employeeRepository;

    public EmployeesController(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    /// <summary>
    /// Получить данные всех сотрудников
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IResult> GetEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();

        var employeesModelList = employees.Select(x =>
            new EmployeeShortVm() {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
            }).ToList();

        return Results.Ok(employeesModelList);
    }

    /// <summary>
    /// Получить данные сотрудника по Id
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<IResult> GetEmployeeByIdAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return Results.NotFound();

        var employeeModel = new EmployeeVm() {
            Id = employee.Id,
            Email = employee.Email,
            Roles = employee.Roles.Select(x => new RoleItemVm() {
                Name = x.Name,
                Description = x.Description
            }).ToList(),
            FullName = employee.FullName,
            AppliedPromocodesCount = employee.AppliedPromocodesCount
        };

        return Results.Ok(employeeModel);
    }

    /// <summary>
    /// Ввести данные нового сотрудника
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResult> CreateEmployeeAsync(EmployeeCreateVm employee)
    {
        var newEmployee = new Employee() {
            Id = Guid.NewGuid(),
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email
        };

        var id = await _employeeRepository.CreateAsync(newEmployee);
        return Results.Ok($"Запись о сотруднике создана. Идентификатор записи: {id}") ;
    }

    /// <summary>
    /// Изменить данные сотрудника
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public async Task<IResult> UpdateEmployeeAsync(EmployeeUpdateVm employee)
    {
        var employeeForUpd = await _employeeRepository.GetByIdAsync(employee.Id);

        if (employeeForUpd == null)
            return Results.NotFound("Сотрудник не найден");

        employeeForUpd.FirstName = employee.FirstName;
        employeeForUpd.LastName = employee.LastName;
        employeeForUpd.Email = employee.Email;

        return Results.Ok("Данные сотрудника изменены");
    }

    /// <summary>
    /// Удалить данные сотрудника
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IResult> DeleteEmployeeAsync(Guid id)
    {
        var employeeForDel = await _employeeRepository.GetByIdAsync(id);

        if (employeeForDel == null)
            return Results.NotFound("Сотрудник не найден");

        await _employeeRepository.DeleteAsync(id);

        return Results.Ok("Данные сотрудника удалены");
    }
}