
namespace FinalCoffee1.Modules.Admin.service;
using FinalCoffee1.Modules.Admin.model;
using FinalCoffee1.common.helperClass;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using FinalCoffee1.common.helperServices;

public class AdminService
{
    private readonly AuthenticationService authentication;

    public AdminService(AuthenticationService authentication)
    {
        this.authentication = authentication;
    }

    //Register Admin
    public async Task<CustomType> Register(AdminModel data)
    {
        try
        {
            var path = new FileManagement().DirectoryPath("database", "admin.json");
            Trace.WriteLine("This is Path: " + path);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            data.Password = this.authentication.GenerateHash(data.Password);
            var jsonData = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync(path, jsonData);
            Trace.WriteLine(path);
            return new CustomType { Success = true, Message = "Admin registered successfully" };
        }
        catch (Exception ex)
        {
            Trace.WriteLine("Exception: {0}", ex.Message);
            return new CustomType { Success = false, Message = ex.Message };
        }
    }

    public async Task<CustomType> IsAdmin(AdminModel data)
    {
        var path = new FileManagement().DirectoryPath("database", "admin.json");
        if (File.Exists(path))
        {
            var existingData = await File.ReadAllTextAsync(path);
            if (!string.IsNullOrEmpty(existingData))
            {
                var existingAdmin = JsonSerializer.Deserialize<AdminModel>(existingData);
                if (existingAdmin != null)
                {
                    var isAuthenticatedUser = authentication.Authenticate(existingAdmin, data);
                    // Perform additional logic here if needed
                    if (isAuthenticatedUser)
                    {
                        return new CustomType { Success = true};
                    }
                    else
                    {
                        return new CustomType { Success = false};
                    }
                }
            }
        }
        return new CustomType { Success = false, Message = "" };
    }
    //Login Logic
    public async Task<CustomType> Login(AdminModel data)
    {
        try
        {
            var isAuthenticatedUser = await this.IsAdmin(data);

            if (isAuthenticatedUser.Success)
            {
                return new CustomType { Success = true, Message = "Login Success" };
            }
            else
            {
                return new CustomType { Success = false, Message = "Invalid Credentials" };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new CustomType { Success = false, Message = ex.Message };
        }
    }
    public Task<CustomType> logOut()
    {
        return Task.FromResult(new CustomType { Success = true, Message = "Logout Success" });
    }

}
