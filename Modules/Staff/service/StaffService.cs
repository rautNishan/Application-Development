
using System.Diagnostics;
using System.Text.Json;
using FinalCoffee1.common.helperClass;
using FinalCoffee1.Common.model;
using FinalCoffee1.Modules.Coffee.model;

namespace FinalCoffee1.Modules.Staff.service;

public class StaffService
{

    int orderCount = 0;
    public async Task<CustomType> TakeOrder(List<CommonModel> coffeeData, string Email, decimal totalPrice,bool free)
    {
        try
        {

            var path = new FileManagement().DirectoryPath("database", "orderData.json");
            Trace.WriteLine("This is Path: " + path);
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            List<OrderModel> existingOrderData = new List<OrderModel>();
            if (File.Exists(path))
            {
                var existingJson = await File.ReadAllTextAsync(path);
                existingOrderData = JsonSerializer.Deserialize<List<OrderModel>>(existingJson) ?? new List<OrderModel>();
            }
            for(int i = 0; i < existingOrderData.Count; i++)
            {
                if(existingOrderData[i].Email == Email)
                {
                    orderCount= existingOrderData[i].Count;
                }
            }
            var newOrderData = new OrderModel
            {
                Email = Email,
                TotalPrice = totalPrice,
                CoffeeData = coffeeData,
                Date = DateTime.Now,
                Count = orderCount+1
            };
             if(free)
            {
                newOrderData.Count=1;
            }
            existingOrderData.Add(newOrderData);

            var jsonData = JsonSerializer.Serialize(existingOrderData);
            await File.WriteAllTextAsync(path, jsonData);
            Trace.WriteLine(path);

            return new CustomType { Success = true, Message = "Order placed successfully" };
        }
        catch (Exception ex)
        {
            Trace.WriteLine("Exception: {0}", ex.Message);
            return new CustomType { Success = false, Message = ex.Message };
        }
    }

    List<CommonModel> coffeeList = new List<CommonModel>();
    public void setOrderedList(CommonModel coffeeData)
    {
        Trace.WriteLine("This is CoffeeData in Staff: " + coffeeData);
        this.coffeeList.Add(coffeeData);

    }
    public Task<List<CommonModel>> getOrderedCoffee()
    {
        return Task.FromResult(this.coffeeList);
    }

    public async Task<List<CommonModel>> removeOrderList(int index)
    {
        var itemToRemove = this.coffeeList.FirstOrDefault(c => c.index == index);
        this.coffeeList.Remove(itemToRemove);
        return this.coffeeList;
    }

    public async Task<CustomType> clearOrderList()
    {
        this.coffeeList.Clear();
        return new CustomType { Success = true, Message = "Success" };
    }

    public async Task<CustomType> isUserRegistered(string email)
    {
        var path = new FileManagement().DirectoryPath("database", "user.json");
        List<UserModel> userList = new List<UserModel>();
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        if (File.Exists(path))
        {
            var existingData = await File.ReadAllTextAsync(path);
            userList = JsonSerializer.Deserialize<List<UserModel>>(existingData) ?? new List<UserModel>();
        }
        for (int i = 0; i < userList.Count; i++)
        {
            if (userList[i].Email == email)
            {
                Trace.WriteLine("This is Email: " + userList[i].Email);
                return new CustomType { Success = true, Message = "Success" };
            }
        }
        return new CustomType { Success = false, Message = "User Not Found" };
    }

    
    public async Task<bool> getIsFree(string email)
    {
        var path = new FileManagement().DirectoryPath("database", "orderData.json");
        int count=0;
        List<OrderModel> orderList = new List<OrderModel>();
        if (File.Exists(path))
        {
            var existingData = await File.ReadAllTextAsync(path);
            orderList = JsonSerializer.Deserialize<List<OrderModel>>(existingData) ?? new List<OrderModel>();
            Trace.WriteLine("This is Order Count: " + orderList.Count());
            for (int i = 0; i < orderList.Count(); i++)
            {
                if (orderList[i].Email == email)
                {
                    count= orderList[i].Count;
                }
                Trace.WriteLine("This is Count: "+count);
            }
            if (count >= 10)
            {
                return true;
            }
        }
        return false;
    }

}