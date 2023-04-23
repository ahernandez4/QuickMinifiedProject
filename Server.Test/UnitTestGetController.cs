namespace Server.Test;
using Server.API.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

public class UnitTestGetController
{
    [Fact]
    public async Task ControllerShouldReturnObject()
    {
        //arrange
        var services = new ServiceCollection();
        services.AddMemoryCache();
        var serviceProvider = services.BuildServiceProvider();
        var memoryCache = serviceProvider.GetService<IMemoryCache>();

        Mock<ILogger<QuickUrlController>> mockLogger = new();
        MyUrlObject myTestObject = new MyUrlObject("http://localhost/testobject/myobject");

        memoryCache.Set(myTestObject.SmallerURL, myTestObject);
        var controllerUnderTest = new QuickUrlController(memoryCache!, mockLogger.Object);

        //Act
        var httpresult = await controllerUnderTest.Get(myTestObject.SmallerURL);
        var result = httpresult as ContentResult;

        //Console.WriteLine("Content is:\n" + result.Content);
        string expectedResponseBody = JsonSerializer.Serialize(myTestObject);
        //var body = result.Content;
        Assert.Equal(expectedResponseBody, result.Content);

        //Console.WriteLine(expectedResponseBody);
    }
    [Fact]
    public void ControllerShouldReturnEmptyObject()
    {
        string resShouldBe = @"{""OriginalURL"":"""",""SmallerURL"":""""}";
    }
    [Fact]
    public async Task ControllerShouldReturnNewObject(){
        var services = new ServiceCollection();
        services.AddMemoryCache();
        var serviceProvider = services.BuildServiceProvider();
        var memoryCache = serviceProvider.GetService<IMemoryCache>();

        Mock<ILogger<QuickUrlController>> mockLogger = new();
        var controllerUnderTest = new QuickUrlController(memoryCache!, mockLogger.Object);
        string myTestStringURL = "http://localhost/testobject/myobject";
        //Act
        var httpresult = await controllerUnderTest.Get(myTestStringURL);
        var result = httpresult as ContentResult;

        //Assert
        Assert.Contains(myTestStringURL,result.Content);
        
    }
}