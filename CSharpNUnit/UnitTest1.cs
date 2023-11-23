using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;
using Protractor;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;



namespace CSharpNUnit;


public class Tests
{

    public NgWebDriver _ngDriver;
    public IDictionary<string, object> vars { get; private set; }
    public IJavaScriptExecutor js;
    String[] ltFile = new string[] { "sample01.xlsx" };



    [SetUp]
    public void Setup()
    {

      

        // ChromeOptions
        ChromeOptions options = new ChromeOptions();
        options.BrowserVersion = "latest";
        options.PlatformName = "Windows 10";

        // Set LambdaTest specific capabilities
        options.AddAdditionalOption("user", "LT_USERNAME");
        options.AddAdditionalOption("accessKey", "LT_ACCESS_KEY");
        options.AddAdditionalOption("build", "NUnit Protractor Test");
        options.AddAdditionalOption("name", "CSharpNGTest");
        options.AddAdditionalOption("lambda:userFiles", ltFile);



        // Initialize RemoteWebDriver with LambdaTest Hub URL
        RemoteWebDriver remoteWebDriver = new RemoteWebDriver(
            new Uri("https://LT_USERNAME:LT_ACCESS_KEY@hub.lambdatest.com/wd/hub"), options.ToCapabilities(), TimeSpan.FromSeconds(600));

        // Wrap RemoteWebDriver in NgWebDriver
        _ngDriver = new NgWebDriver(remoteWebDriver);
        js = (IJavaScriptExecutor)_ngDriver.WrappedDriver;
        vars = new Dictionary<string, object>();


    }

    [Test]
    public void Test1()
    {

        _ngDriver.Navigate().GoToUrl("https://todomvc.com/examples/angularjs/");


         ((IJavaScriptExecutor)_ngDriver).ExecuteScript("lambda-status=passed");


        // Assuming _ngDriver is your initialized NgWebDriver or IWebDriver instance
        IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)_ngDriver;

        // Execute the script and get the result
        var result = jsExecutor.ExecuteScript("lambda-file-list=sample");

        // Check if the result is an array and cast it
        if (result is IEnumerable<object> arrayResult)
        {
            // Iterate and print each element in the array
            foreach (var item in arrayResult)
            {
                Console.WriteLine(item);
            }
        }
        else
        {
            Console.WriteLine("The script did not return an array.");
        }



    }

    [TearDown]
    protected void TearDown()
    {
        _ngDriver.Quit();
    }

}
