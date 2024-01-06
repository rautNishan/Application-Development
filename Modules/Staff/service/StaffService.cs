
using System.Diagnostics;
using FinalCoffee1.common.helperClass;
using FinalCoffee1.Modules.Coffee.model;

namespace FinalCoffee1.Modules.Staff.service;

public class StaffService
{

    public async Task<CustomType> TakeOrder(List<CommonModel> coffeeData)
    {
        Trace.WriteLine("This is CoffeeData: " + coffeeData);
        foreach (var item in coffeeData)
        {
            Trace.WriteLine("This is CoffeeData: " + item);
        }
        return new CustomType { Success = true, Message = "Success" };
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
}