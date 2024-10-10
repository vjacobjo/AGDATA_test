using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebUI.pom.templates;

public class GenericUI(IWebDriver driver, string xpath) : IGenericUI
{
    protected IWebDriver driver = driver;
    private string xpath = xpath;
    private IWebElement _webElement;
    private IList<IWebElement> _webElements;

    public virtual void Click()
    {
        GetWebElement().Click();
    }

    public virtual string GetText()
    {
        string retVal = "";
        foreach (IWebElement e in GetWebElements())
        {
            retVal += $",{e.Text}";
        }

        return retVal.TrimStart(',');
    }

    public virtual void WaitUntilVisible(int timeout=30)
    {
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(timeout));
        wait.Until(d => GetWebElement().Displayed);
    }

    public IWebElement GetWebElement()
    {
        _webElement ??= driver.FindElement(By.XPath(xpath));
        return _webElement;
    }

    public IList<IWebElement> GetWebElements()
    {
        _webElements ??= driver.FindElements(By.XPath(xpath));
        return _webElements;
    }

    public virtual void SelectOption(string option)
    {
        Click();
        foreach (var e in GetChildren("//*[contains(@class, \"menu-item\")]"))
        {
            if (e.Text.Contains(option))
            {
                e.Click();
                return;
            }
        }

        throw new Exception($"Option \"{option}\" not found in {this.GetType().Name}");
    }

    private IList<IWebElement> GetChildren(string xpath)
    {
        return GetWebElement().FindElements(By.XPath(xpath));
    }
}
