using Microsoft.Extensions.Configuration;

namespace Items.Infrastructure.Context;

public class ApplicationConfiguration
{
	public string DbConnectionString { get; }

    public ApplicationConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder();
        
        var appsettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        configurationBuilder.AddJsonFile(appsettingsPath,false);
        
        var root = configurationBuilder.Build();

        DbConnectionString = root.GetSection("ConnectionStrings:DefaultConnection").Value;
    }
}
