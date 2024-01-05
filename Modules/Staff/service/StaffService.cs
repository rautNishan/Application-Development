
using System.Diagnostics;
using FinalCoffee1.common.helperClass;
using FinalCoffee1.Modules.Coffee.model;

namespace FinalCoffee1.Modules.Staff.service;

public class StaffService
{

    public async Task<CustomType> TakeOrder(List<CoffeeModel> coffeeData)
    {
        Trace.WriteLine("This is CoffeeData: " + coffeeData);
        foreach (var item in coffeeData)
        {
            Trace.WriteLine("This is CoffeeData: " + item);
        }
        return new CustomType { Success = true, Message = "Success" };
    }

    List<CoffeeModel> coffeeList = new List<CoffeeModel>();
    public void setOrderedList(CoffeeModel coffeeData)
    {
        Trace.WriteLine("This is CoffeeData in Staff: " + coffeeData);
        this.coffeeList.Add(coffeeData);

    }
    public Task<List<CoffeeModel>> getOrderedCoffee()
    {
        return Task.FromResult(this.coffeeList);
    }

    public async Task<List<CoffeeModel>> removeOrderList(int index)
    {
        var itemToRemove = this.coffeeList.FirstOrDefault(c => c.index == index);
        this.coffeeList.Remove(itemToRemove);
        return this.coffeeList;
    }
}