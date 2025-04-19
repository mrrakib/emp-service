using HrmBaharu.Application.Common.Interfaces;
using HrmBaharu.Domain.Entities;
using HrmBaharu.Domain.Events;

namespace HrmBaharu.Application.Features.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand : IRequest<int>
{
    public string? Name { get; init; }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Employee
        {
            Name = request.Name,
        };

        entity.AddDomainEvent(new EmployeeCreatedEvent(entity));

        _context.Employees.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
