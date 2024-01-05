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

    public async Task<CustomType> Edit(int id, CoffeeModel model, string fileName)
    {
        try
        {
            var path = new FileManagement().DirectoryPath("database", fileName);
            if (File.Exists(path))
            {
                var existingData = await File.ReadAllTextAsync(path);
                var list = JsonSerializer.Deserialize<List<CoffeeModel>>(existingData) ?? new List<CoffeeModel>();
                var itemToEdit = list.FirstOrDefault(c => c.Id == id);
                Trace.WriteLine("This is ItemToEdit: " + itemToEdit);
                if (itemToEdit != null)
                {
                    if (itemToEdit.GetType().GetProperty("Name") != null)
                    {
                        itemToEdit.Name = model.Name;
                    }
                    if (itemToEdit.GetType().GetProperty("CoffeeType") != null)
                    {
                        itemToEdit.CoffeeType = model.CoffeeType;
                    }
                    if (itemToEdit.GetType().GetProperty("Size") != null)
                    {
                        itemToEdit.Size = model.Size;
                    }
                    if (itemToEdit.GetType().GetProperty("Price") != null)
                    {
                        itemToEdit.Price = model.Price;
                    }


                    int index = list.FindIndex(c => c.Id == id);
                    list[index] = itemToEdit;
                    var jsonData = JsonSerializer.Serialize(list);
                    await File.WriteAllTextAsync(path, jsonData);
                    return new CustomType { Success = true, Message = "Updated" };
                }
                else
                {
                    return new CustomType { Success = false, Message = "Item not found" };
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

}