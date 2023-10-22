using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using ToDoList.Models;
using System;

namespace ToDoList.Tests
{

  [TestClass]
  public class ItemTests : IDisposable
  {

    public IConfiguration Configuration { get; set; }

    public void Dispose()
    {
      Item.ClearAll();
    }

    public ItemTests()
    {
      IConfigurationBuilder builder = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json");
      Configuration = builder.Build();
      DBConfiguration.ConnectionString = Configuration["ConnectionStrings:TestConnection"];
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_ItemList()
    {
      List<Item> newList = new List<Item> { };
      List<Item> result = Item.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }
    [TestMethod]
    public void ReferenceTypes_ReturnsTrueBecauseBothItemsAreSameReference_Bool()
    {
      Item firstItem = new Item("Mow the lawn", 0);
      Item secondItem = firstItem;
      secondItem.Description = "Learn about C#";
      Assert.AreEqual(firstItem.Description, secondItem.Description);
    }
    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      Item testItem = new Item("Mow the lawn", 0);
      testItem.Save();
      List<Item> result = Item.GetAll();
      List<Item> testList = new List<Item> { testItem };
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_ReturnsCorrectItemFromDatabase_Item()
    {
      Item newItem = new Item("Mow the lawn", 0);
      newItem.Save();
      Item newItem2 = new Item("Wash dishes", 1);
      newItem2.Save();
      Item foundItem = Item.Find(newItem.Id);
      Assert.AreEqual(newItem, foundItem);
    }

    // [TestMethod]
    // public void ItemsConstructor_CreatesInstanceOfItems_Item()
    // {
    //   Item newItem = new Item("0");
    //   Assert.AreEqual(typeof(Item), newItem.GetType());
    // }
    // [TestMethod]
    // public void GetId_ItemsInstantiateWithAnIdAndGetterReturns_Int()
    // {
    //   string description = "Walk the dog.";
    //   Item newItem = new Item(description);
    //   int result = newItem.Id;
    //   Assert.AreEqual(1, result);
    // }

    // [TestMethod]
    // public void GetAll_ReturnsItems_ItemList()
    // {
    //   string description01 = "Walk the dog";
    //   string description02 = "Wash the dishes";
    //   Item newItem1 = new Item(description01, 0);
    //   newItem1.Save();
    //   Item newItem2 = new Item(description02, 1);
    //   newItem2.Save();
    //   List<Item> newList = new List<Item> { newItem1, newItem2 };
    //   List<Item> result = Item.GetAll();
    //   Assert.AreEqual(newList, result); 
    // }

  }
}