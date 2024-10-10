using OpenQA.Selenium;
using WebUI.pom.templates;

namespace WebUI.pom;

public class POMLocator(IWebDriver driver)
{
    private IWebDriver driver = driver;

    public IGenericUI GetUI(string uiId)
    {
        var uiType = Type.GetType($"WebUI.pom.{uiId}");
        if (uiType != null)
        {
            return (IGenericUI)Activator.CreateInstance(uiType, driver)!;
        }

        throw new Exception($"UI \"{uiId}\" not defined in Page Object Model");
    }
}
