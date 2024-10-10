using OpenQA.Selenium;
using WebUI.pom.templates;

namespace WebUI.pom.HomePage
{
    public class AGDATAHeader(IWebDriver driver):GenericUI(driver, "//div/a[@class=\"site-title\"]") {}

    public class Navigator(IWebDriver driver):GenericUI(driver, "//ul[@id=\"primary-menu\"]")
    {
        public override void SelectOption(string option)
        {
            var iterElement = GetWebElement();
            foreach (var o in  option.Split('>', StringSplitOptions.TrimEntries))
            {
                iterElement.FindElement(By.XPath($"//a[contains(text(), \"{o}\")]/parent::li")).Click();
            }
        }
    }
}
