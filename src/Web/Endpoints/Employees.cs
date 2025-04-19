using HrmBaharu.Application.Common.Models;
using HrmBaharu.Application.Features.Employees.Queries.GetEmployeesWithPagination;
using HrmBaharu.Application.Features.Employees.Commands.CreateEmployee;
using HrmBaharu.Application.Features.Employees.Commands.DeleteEmployee;
using HrmBaharu.Application.Features.Employees.Commands.UpdateEmployee;
using DepartmentProto;

namespace HrmBaharu.Web.Endpoints;

public class Employees : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetEmployeesWithPagination)
            .MapPost(CreateEmployee)
            .MapPut(UpdateEmployee, "{id}")
            .MapDelete(DeleteEmployee, "{id}");
    }
    public Task<PaginatedList<EmployeeDto>> GetEmployeesWithPagination(ISender sender, [AsParameters] GetEmployeesWithPaginationQuery query)
    {
        return sender.Send(query);
    }
    public async Task<int> CreateEmployee(ISender sender, DepartmentService.DepartmentServiceClient departmentClient, CreateEmployeeCommand command)
    {
        var grpcResponse = await departmentClient.GetAllDepartmentsAsync(new Empty());

        return await sender.Send(command);
    }

    public async Task<IResult> UpdateEmployee(ISender sender, int id, UpdateEmployeeCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
    public async Task<IResult> DeleteEmployee(ISender sender, int id)
    {
        await sender.Send(new DeleteEmployeeCommand(id));
        return Results.NoContent();
    }
}
