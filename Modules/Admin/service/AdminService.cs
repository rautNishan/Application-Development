
namespace FinalCoffee1.Modules.Admin.service;
using FinalCoffee1.Modules.Admin.model;
using FinalCoffee1.Modules.Staff.model;
using FinalCoffee1.common.helperClass;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using FinalCoffee1.common.helperServices;

public class AdminService
{
    private readonly AuthenticationService authentication;
    private readonly SessionService sessionService;
    List<StaffModel> staffList;
    public AdminService(AuthenticationService authentication)
    {
        this.authentication = authentication;
    }

    //Register Admin
    public async Task<CustomType> Register(AdminModel data)
    {
        try
        {
            data.userType = UserType.Admin;
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
    //Login Logic
    public async Task<CustomType> Login(AdminModel data)
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
                        var isAuthenticatedUser = authentication.Authenticate(existingAdmin, data);

                        if (isAuthenticatedUser)
                        {
                            return new CustomType { Success = true, Message = "Login Success" };
                        }
                        else
                        {
                            return new CustomType { Success = false, Message = "Invalid Credentials" };
                        }
                    }
                }
            }
            return new CustomType { Success = false, Message = "Please Make Sure Admin is Resister" };
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

    public async Task<CustomType> addStaff(StaffModel staffData)
    {
        try
        {
            staffData.userType = UserType.Staff;
            var path = new FileManagement().DirectoryPath("database", "staff.json");
            Trace.WriteLine("This is Path: " + path);
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            List<StaffModel> staffList = new List<StaffModel>();
            if (File.Exists(path))
            {
                var existingData = await File.ReadAllTextAsync(path);
                staffList = JsonSerializer.Deserialize<List<StaffModel>>(existingData) ?? new List<StaffModel>();
            }
            int maxId = staffList.Any() ? staffList.Max(s => s.Id) : 0;
            staffData.Id = maxId + 1;

            staffData.Password =  this.authentication.GenerateHash(staffData.Password);
            Trace.WriteLine("This is PASSWORD: ", staffData.Password);
            staffList.Add(staffData);

            var jsonData = JsonSerializer.Serialize(staffList);
            await File.WriteAllTextAsync(path, jsonData);
            Trace.WriteLine(path);
            return new CustomType { Success = true, Message = "Staff registered successfully" };
        }
        catch (Exception ex)
        {
            Trace.WriteLine("Exception: {0}", ex.Message);
            return new CustomType { Success = false, Message = ex.Message };
        }
    }

    public async Task readStaff()
    {
        try
        {
            var path = new FileManagement().DirectoryPath("database", "staff.json");
            if (File.Exists(path))
            {
                var existingData = await File.ReadAllTextAsync(path);
                Trace.WriteLine("This is Existing Data: " + existingData);
                staffList = JsonSerializer.Deserialize<List<StaffModel>>(existingData) ?? new List<StaffModel>();
            }
        }
        catch (Exception error)
        {
            Trace.WriteLine("This is Error: " + error.Message);
        }

    }
    public async Task<List<StaffModel>> getStaffList()
    {
        await this.readStaff();
        Trace.WriteLine("This is staff List: " + this.staffList);
        return this.staffList;
    }
}
