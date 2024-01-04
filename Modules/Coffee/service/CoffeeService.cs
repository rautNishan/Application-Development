using System.Diagnostics;
using System.Text.Json;
using FinalCoffee1.common.helperClass;
using FinalCoffee1.Modules.Coffee.model;

namespace FinalCoffee1.Modules.Coffee.service;

public class CoffeeService
{
    List<CoffeeModel> coffeeList;
    int count = 0;
    public async Task<CustomType> addCoffee(CoffeeModel coffeeData)
    {
        try
        {
            Trace.WriteLine("Calling Add Coffee");
            var path = new FileManagement().DirectoryPath("database", "coffee.json");
            Trace.WriteLine("This is Path: " + path);
            if (!File.Exists(path))
            {
                Trace.WriteLine("This is Existing Data: " + "No Data");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                coffeeData.Id = 1;
                coffeeList = new List<CoffeeModel> { coffeeData };
            }
            else
            {
                var existingData = await File.ReadAllTextAsync(path);
                Trace.WriteLine("This is Existing Data: " + existingData);
                coffeeList = JsonSerializer.Deserialize<List<CoffeeModel>>(existingData) ?? new List<CoffeeModel>();
                coffeeData.Id = coffeeList.Count + 1;
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
    public async Task<CustomType> editCoffee(int id, CoffeeModel updatedCoffeeData)
    {
        try
        {
            var path = new FileManagement().DirectoryPath("database", "coffee.json");
            Trace.WriteLine("This is ID: " + id);
            Trace.WriteLine("This is Incomming Data: " + updatedCoffeeData.Name);
            if (File.Exists(path))
            {
                var existingData = await File.ReadAllTextAsync(path);
                coffeeList = JsonSerializer.Deserialize<List<CoffeeModel>>(existingData) ?? new List<CoffeeModel>();
                var coffeeToEdit = coffeeList.FirstOrDefault(c => c.Id == id); 
                Trace.WriteLine("This is CoffeeToEdit: " + coffeeToEdit);
                if (coffeeToEdit != null)
                {
                    coffeeToEdit.Name = updatedCoffeeData.Name;
                    coffeeToEdit.CoffeeType = updatedCoffeeData.CoffeeType;
                    coffeeToEdit.Size = updatedCoffeeData.Size;
                    coffeeToEdit.Price = updatedCoffeeData.Price;

                    int index = coffeeList.FindIndex(c => c.Id == id);
                    coffeeList[index] = coffeeToEdit;
                    var jsonData = JsonSerializer.Serialize(coffeeList);
                    await File.WriteAllTextAsync(path, jsonData);
                    return new CustomType { Success = true, Message = "Updated" };
                }
                else
                {
                    return new CustomType { Success = false, Message = "Coffee not found" };
                }
            }
            else
            {
                return new CustomType { Success = false, Message = "File not found" };
            }
        }
        catch (Exception error)
        {
            return new CustomType { Success = false, Message = error.Message };
        }
    }
    public async Task<CustomType> deleteCoffee(int id)
    {
        try
        {
            var path = new FileManagement().DirectoryPath("database", "coffee.json");
            if (File.Exists(path))
            {
                var existingData = await File.ReadAllTextAsync(path);
                coffeeList = JsonSerializer.Deserialize<List<CoffeeModel>>(existingData) ?? new List<CoffeeModel>();
                var coffeeToDelete = coffeeList.FirstOrDefault(c => c.Id == id);
                if (coffeeToDelete != null)
                {
                    coffeeList.Remove(coffeeToDelete);
                    var jsonData = JsonSerializer.Serialize(coffeeList);
                    await File.WriteAllTextAsync(path, jsonData);
                    return new CustomType { Success = true, Message = "Deleted" };
                }
                return new CustomType { Success = false, Message = "Coffee not found" };
            }
            else
            {
                return new CustomType { Success = false, Message = "File not found" };
            }
        }
        catch (Exception error)
        {
            return new CustomType { Success = false, Message = error.Message };
        }
    }
}