using HrmBaharu.Application.Common.Interfaces;
using HrmBaharu.Application.Common.Mappings;
using HrmBaharu.Application.Common.Models;

namespace HrmBaharu.Application.Features.Employees.Queries.GetEmployeesWithPagination
{
    public record GetEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetEmployeesWithPaginationQueryHandler : IRequestHandler<GetEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<EmployeeDto>> Handle(GetEmployeesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .OrderBy(x => x.Name)
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
