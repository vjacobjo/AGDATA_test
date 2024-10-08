/// <summary>
/// This implements the Gauge Steps that pertain to WebUI Testing. 
/// Examples of the Steps can be found in specs/Part2.spec.
/// </summary>
using Gauge.CSharp.Lib;
using Gauge.CSharp.Lib.Attribute;
using WebUI;

namespace StepImplementations;

public class WebUITestSteps : ICustomScreenshotWriter
{
    /// <summary>
    /// Instance of the WebUIClient are not stored within the class but in the data store (spec) level from where they are retrieved.
    /// </summary>
    private WebUIClient webClient{
        get { return (WebUIClient)DataStoreGuard.GetValue("webDriver", true); }
        set { DataStoreGuard.CacheValue("webDriver", value, true); }
    }

    [Step("Launch browser to <baseUrl>")]
    public void LaunchBrowser(string baseUrl)
    {
        webClient = new WebUIClient(baseUrl);
    }

    [Step("Click on <uiId>")]
    public void ClickUI(string uiId)
    {
        webClient.Click(uiId);
    }

    [Step("Wait until <uiId> is displayed")]
    public void WaitForUi(string uiId)
    {
        webClient.WaitUntilVisible(uiId);
    }

    [Step("Wait until <uiId> is displayed within <tiimeout> seconds")]
    public void WaitWithTimeout(string uiId, int timeout)
    {
        webClient.WaitUntilVisible(uiId, timeout);
    }

    [Step("Get Text Displayed in <uiId>")]
    public void GetUIText(string uiId)
    {
        GaugeMessages.WriteMessage(webClient.GetText(uiId));
    }

    [Step("Select <optionText> from <uiId>")]
    public void SelectOption(string optionText, string uiId)
    {
        webClient.SelectOption(uiId, optionText);
    }

    [Step("Get Screenshot")]
    public void GetScreenshot()
    {
        GaugeScreenshots.Capture();
    }

    [Step("Close browser")]
    public void CloseBrowser()
    {
        webClient.Dispose();
    }

    /// <summary>
    /// This implements abstract method defined in ICustomScreenshotWriter, an interface provided by Gauge.
    /// Enables taking a screenshot using any custom driver, in this case using our custom webUiClient.
    /// </summary>
    /// <returns></returns>
    public string TakeScreenShot()
    {
        var dirPath = Environment.GetEnvironmentVariable("gauge_screenshots_dir");
        var fileName = "screenshot_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        return webClient.TakeScreenshot(fileName, dirPath);
    }
}
