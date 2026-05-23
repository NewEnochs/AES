// 配置服务接口
using AES.Helper;
using Microsoft.Extensions.Options;

public interface IItemValueService
{
    List<SmsData> GetAllSmsData();
    List<ConnStringData> GetAllConnStringData();
    SmsData GetSmsById(int id);
    SmsData GetSmsByName(string name);
    ConnStringData GetConnStringById(int id);
    ConnStringData GetConnStringByName(string name);
    string GetConnectionString(string name);
}

// 配置服务实现
public class ItemValueService : IItemValueService
{
    private readonly ItemValuesData _config;

    public ItemValueService(IOptions<ItemValuesData> options)
    {
        _config = options.Value;
    }

    public List<SmsData> GetAllSmsData()
    {
        return _config?.SmsData ?? new List<SmsData>();
    }

    public List<ConnStringData> GetAllConnStringData()
    {
        return _config?.ConnStringData ?? new List<ConnStringData>();
    }

    public SmsData GetSmsById(int id)
    {
        return _config?.SmsData?.FirstOrDefault(x => x.Id == id);
    }

    public SmsData GetSmsByName(string name)
    {
        return _config?.SmsData?.FirstOrDefault(x => x.Name == name);
    }

    public ConnStringData GetConnStringById(int id)
    {
        return _config?.ConnStringData?.FirstOrDefault(x => x.Id == id);
    }

    public ConnStringData GetConnStringByName(string name)
    {
        return _config?.ConnStringData?.FirstOrDefault(x => x.Name == name);
    }

    public string GetConnectionString(string name)
    {
        var conn = GetConnStringByName(name);
        return conn?.ConnString;
    }
}