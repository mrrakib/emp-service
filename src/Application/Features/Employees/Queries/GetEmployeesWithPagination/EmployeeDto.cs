using HrmBaharu.Domain.Entities;

namespace HrmBaharu.Application.Features.Employees.Queries.GetEmployeesWithPagination;

public class EmployeeDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}

