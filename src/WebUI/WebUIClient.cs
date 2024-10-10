using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebUI.pom;
using WebDriverManager;

namespace WebUI;

public class WebUIClient: IDisposable
{
    private IWebDriver driver;
    private POMLocator locator;

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

    public void Click(string uiId)
    {
        locator.GetUI(uiId).Click();
    }

    public void WaitUntilVisible(string uiId, int timeout=30)
    {
        locator.GetUI(uiId).WaitUntilVisible(timeout:timeout);
    }

    public string GetText(string uiId)
    {
        return locator.GetUI(uiId).GetText();
    }

    public void SelectOption(string uiId, string optionText)
    {
        locator.GetUI(uiId).SelectOption(optionText);
    }

    public void Dispose()
    {
        driver.Close();
        driver.Quit();
        driver.Dispose();
    }

    public string TakeScreenshot(string fileName, string dirPath)
    {
        var fullPath = $"{dirPath}/{fileName}";
        (driver as ITakesScreenshot)!.GetScreenshot().SaveAsFile(fullPath);
        return fileName;
    }
}
