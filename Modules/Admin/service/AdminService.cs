
namespace FinalCoffee1.Modules.Admin.service;
using FinalCoffee1.Modules.Admin.model;
using FinalCoffee1.common.helperClass;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using FinalCoffee1.common.helperServices;
// using FinalCoffee1.common.helperServices;
public class AdminService
{
    //Register Admin
    public async Task<Type> Register(AdminModel data)
    {
        try
        {
            var path = new FileManagement().DirectoryPath("database", "admin.json");
            Trace.WriteLine(path);
            if (File.Exists(path))
            {
                var existingData = await File.ReadAllTextAsync(path);
                Trace.WriteLine("Existing Data" + existingData);
                Trace.WriteLine(existingData.GetType());
                if (!string.IsNullOrEmpty(existingData))
                {
                    Trace.WriteLine("Is not Null or Empty");
                    var existingAdmin = JsonSerializer.Deserialize<AdminModel>(existingData);
                    if (existingAdmin != null)
                    {
                        Trace.WriteLine("Returning Message");
                        return new Type { Success = false, Message = "Admin already exists" };
                    }
                }
                  Trace.WriteLine("Existing Data" + existingData);
            }
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data.Password));
                data.Password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            var jsonData = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync(path, jsonData);
            Trace.WriteLine(path);
            return new Type { Success = true, Message = "Admin registered successfully" };

        }
        catch (Exception ex)
        {
            Trace.WriteLine("Exception: {0}", ex.Message);
            return new Type { Success = false, Message = ex.Message };
        }
    }
    //Login Logic
    public async Task<Type> Login(AdminModel data)
    {
        try
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
                        using (var sha256 = SHA256.Create())
                        {
                            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data.Password));
                            var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                            if (data.Username == existingAdmin.Username && hashedPassword == existingAdmin.Password)
                            {
                                return new Type { Success = true, Message = "Login Success" };
                            }
                            else
                            {
                                return new Type { Success = false, Message = "Invalid Credentials" };
                            }
                        }
                    }
                }
            }
            return new Type { Success = false, Message = "Please Make Sure Admin is Resister" };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new Type { Success = false, Message = ex.Message };
        }
    }

    public Task<Type> logOut()
    {
        return Task.FromResult(new Type { Success = true, Message = "Logout Success" });
    }

}