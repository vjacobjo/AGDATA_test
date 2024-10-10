/// <summary>
/// This is the main class for dynamically retrieving UI elements defined in the POM folder.
/// uiId must be structured as "ParentPage.UIName" (e.g. HomePage.Navigator)
/// Homepages are defined in .cs files just under the POM directory.
/// </summary>
using OpenQA.Selenium;
using WebUI.pom.templates;

namespace WebUI.pom;

public class POMLocator(IWebDriver driver)
{
    private IWebDriver driver = driver;

    public IGenericUI GetUI(string uiId)
    {
        var uiType = Type.GetType($"WebUI.pom.{uiId}") ?? throw new Exception($"UI \"{uiId}\" not found in POM");
        return (IGenericUI)Activator.CreateInstance(uiType, driver)!;
    }
}
