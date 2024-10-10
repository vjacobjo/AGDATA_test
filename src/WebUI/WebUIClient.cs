/// <summary>
/// This client wraps around Selenium and additional tools to intialize, click, verify and wait for Webelements.
/// </summary>
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebUI.pom;
using WebDriverManager;

namespace WebUI;

public class WebUIClient: IDisposable
{
    private IWebDriver driver;
    private POMLocator locator;

    /// <summary>
    /// Starts the WebDriver and launches browser to load the baseUrl provided.
    /// If WebDriver server is not initially provided, uses WebDriverManager to download the driver to a known location.
    /// Initializes the POMLocator.
    /// Note: The web_driver url and the local path to the downloaded driver are defined in env/default/web.properties.
    /// </summary>
    /// <param name="baseUrl"></param>
    public WebUIClient(string baseUrl)
    {
        var driver_path = Environment.GetEnvironmentVariable("webdriver_local_path");
        new DriverManager().SetUpDriver(
            Environment.GetEnvironmentVariable("webdriver_url"),
            Path.Combine(Directory.GetCurrentDirectory(), driver_path)
        );
        driver = new ChromeDriver(Path.Combine(Directory.GetCurrentDirectory(), driver_path));
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        driver.Url = baseUrl;
        locator = new POMLocator(driver);
    }

    /// <summary>
    /// With ui Id provided, driver clicks on the element.
    /// </summary>
    /// <param name="uiId"></param>
    public void Click(string uiId)
    {
        locator.GetUI(uiId).Click();
    }

    /// <summary>
    /// With ui Id provided, driver waits until the element is displayed within the timeout seconds provided. By default it is 30 seconds.
    /// </summary>
    /// <param name="uiId"></param>
    /// <param name="timeout"></param>
    public void WaitUntilVisible(string uiId, int timeout=30)
    {
        locator.GetUI(uiId).WaitUntilVisible(timeout:timeout);
    }

    /// <summary>
    /// With UI ID provided, it gets text of a particular element, If multiple elements are found, it returns all the text in an array.
    /// </summary>
    /// <param name="uiId"></param>
    /// <returns></returns>
    public string GetText(string uiId)
    {
        return locator.GetUI(uiId).GetText();
    }

    /// <summary>
    /// From UI, specified by uiID, selects the element that contains the desired optionText.
    /// Can be used for dropdown or menu Items.
    /// </summary>
    /// <param name="uiId"></param>
    /// <param name="optionText"></param>
    public void SelectOption(string uiId, string optionText)
    {
        locator.GetUI(uiId).SelectOption(optionText);
    }

    /// <summary>
    /// Closes and Quits the driver, then disposes it.
    /// </summary>
    public void Dispose()
    {
        driver.Close();
        driver.Quit();
        driver.Dispose();
    }

    /// <summary>
    /// Defines how screenshots are taken using Selenium. 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="dirPath"></param>
    /// <returns></returns>
    public string TakeScreenshot(string fileName, string dirPath)
    {
        var fullPath = $"{dirPath}/{fileName}";
        (driver as ITakesScreenshot)!.GetScreenshot().SaveAsFile(fullPath);
        return fileName;
    }
}
