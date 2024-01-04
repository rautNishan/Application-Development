using System.Diagnostics;
using System.Text.Json;
using FinalCoffee1.common.helperClass;
using FinalCoffee1.Common.model;
namespace FinalCoffee1.common.helperServices;
public class ActionService
{
    private readonly AuthenticationService authentication;

    public ActionService(AuthenticationService authentication)
    {
        this.authentication = authentication;
    }
    public async Task<CustomType> Edit<T>(int id, T model, string fileName) where T : CommonModel, new()
    {
        try
        {
            Trace.WriteLine("This is password: " + model.Password);
            var path = new FileManagement().DirectoryPath("database", fileName);
            if (File.Exists(path))
            {
                var existingData = await File.ReadAllTextAsync(path);
                var list = JsonSerializer.Deserialize<List<T>>(existingData) ?? new List<T>();
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
                    if (itemToEdit.GetType().GetProperty("Username") != null)
                    {
                        itemToEdit.Username = model.Username;
                    }
                    if (itemToEdit.GetType().GetProperty("Password") != null)
                    {
                        model.Password = this.authentication.GenerateHash(model.Password);
                        itemToEdit.Password = model.Password;
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

    public async Task<CustomType> Delete<T>(int id, string fileName) where T : CommonModel, new()
    {
        try
        {
            var path = new FileManagement().DirectoryPath("database", fileName);
            if (File.Exists(path))
            {
                var existingData = await File.ReadAllTextAsync(path);
                var list = JsonSerializer.Deserialize<List<T>>(existingData) ?? new List<T>();
                var itemToDelete = list.FirstOrDefault(c => c.Id == id);
                Trace.WriteLine(itemToDelete);
                if (itemToDelete != null)
                {
                    list.Remove(itemToDelete);
                    var jsonData = JsonSerializer.Serialize(list);
                    await File.WriteAllTextAsync(path, jsonData);
                    return new CustomType { Success = true, Message = "Deleted" };
                }
                return new CustomType { Success = false, Message = "Item not found" };
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