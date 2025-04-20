using HrmBaharu.Application.GRPCServices.Department;

using Grpc.Net.Client;

namespace HrmBaharu.Application.Services;

public class DepartmentClientService
{
    private readonly DepartmentService.DepartmentServiceClient _client;

    public DepartmentClientService()
    {
        // Make sure your ServerService is running at this URL/port
        var channel = GrpcChannel.ForAddress("https://localhost:5002");
        _client = new DepartmentService.DepartmentServiceClient(channel);
    }

    public async Task<DepartmentReply> GetDepartmentByIdAsync(int id)
    {
        var request = new DepartmentRequest { Id = id };
        var response = await _client.GetDepartmentByIdAsync(request);
        return response;
    }

    public async Task<DepartmentListReply> GetAllDepartmentsAsync()
    {
        var response = await _client.GetAllDepartmentsAsync(new Empty());
        return response;
    }
}
