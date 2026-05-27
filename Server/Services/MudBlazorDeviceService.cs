// Services/MudBlazorDeviceService.cs
using Remotely.Shared.Entities;

namespace Remotely.Server.Services;

public interface IMudBlazorDeviceService
{
    Task<List<Device>> GetDevicesAsync();
    Task<Device?> GetDeviceAsync(string deviceId);
}

public class MudBlazorDeviceService : IMudBlazorDeviceService
{
    private readonly IDataService _dataService;
    private readonly ILogger<MudBlazorDeviceService> _logger;

    public MudBlazorDeviceService(IDataService dataService, ILogger<MudBlazorDeviceService> logger)
    {
        _dataService = dataService;
        _logger = logger;
    }

    public async Task<List<Device>> GetDevicesAsync()
    {
        try
        {
            return await Task.FromResult(_dataService.GetDevices());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar dispositivos");
            return new List<Device>();
        }
    }

    public async Task<Device?> GetDeviceAsync(string deviceId)
    {
        try
        {
            var result = await _dataService.GetDevice(deviceId);
            return result.IsSuccess ? result.Value : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar dispositivo {deviceId}", deviceId);
            return null;
        }
    }
}
