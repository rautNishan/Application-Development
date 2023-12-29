using System.Diagnostics;
using System.Text.Json;
using FinalCoffee1.common.helperClass;
using FinalCoffee1.Modules.Coffee.model;

namespace FinalCoffee1.Modules.Coffee.service;

public class CoffeeService
{
    List<CoffeeModel> coffeeList;
    public async Task<CustomType> addCoffee(CoffeeModel coffeeData)
    {
        try
        {
            var path = new FileManagement().DirectoryPath("database", "coffee.json");
            Trace.WriteLine("This is Path: " + path);
            if (!File.Exists(path))
            {
                Trace.WriteLine("This is Existing Data: " + "No Data");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                coffeeList = new List<CoffeeModel> { coffeeData };
            }
            else
            {
                var existingData = await File.ReadAllTextAsync(path);
                Trace.WriteLine("This is Existing Data: " + existingData);
                coffeeList = JsonSerializer.Deserialize<List<CoffeeModel>>(existingData) ?? new List<CoffeeModel>();
                coffeeList.Add(coffeeData);
            }
            var jsonData = JsonSerializer.Serialize(coffeeList);
            await File.WriteAllTextAsync(path, jsonData);
            return new CustomType { Success = true, Message = "Added" };
        }
        catch (Exception error)
        {
            return new CustomType { Success = false, Message = error.Message };
        }
    }

    public async Task readCoffee()
    {
        try
        {
            var path = new FileManagement().DirectoryPath("database", "coffee.json");
            if (File.Exists(path))
            {
                var existingData = await File.ReadAllTextAsync(path);
                Trace.WriteLine("This is Existing Data: " + existingData);
                coffeeList = JsonSerializer.Deserialize<List<CoffeeModel>>(existingData) ?? new List<CoffeeModel>();
            }
        }
        catch (Exception error)
        {
            Trace.WriteLine("This is Error: " + error.Message);
        }

    }
    public async Task<List<CoffeeModel>> getCoffeeList()
    {
        await this.readCoffee();
        Trace.WriteLine("This is Coffee List: " + this.coffeeList);
        return this.coffeeList;
    }
}