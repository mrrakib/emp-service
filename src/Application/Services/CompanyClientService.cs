using DepartmentProto;
using Grpc.Net.Client;

namespace HrmBaharu.Application.Services;

public class CompanyClientService
{
    private readonly CompanyService.CompanyServiceClient _client;

    public CompanyClientService()
    {
        // Make sure your ServerService is running at this URL/port
        var channel = GrpcChannel.ForAddress("https://localhost:5002");
        _client = new CompanyService.CompanyServiceClient(channel);
    }

    public async Task<CompanyReply> GetDepartmentByIdAsync(int id)
    {
        var request = new CompanyRequest { Id = id };
        var response = await _client.GetCompanyByIdAsync(request);
        return response;
    }

    public async Task<CompanyList> GetAllDepartmentsAsync()
    {
        var response = await _client.GetAllCompaniesAsync(new Empty());
        return response;
    }
}
