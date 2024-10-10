/// <summary>
/// This is page loaded from https://www.agdata.com/.
/// </summary>
using OpenQA.Selenium;
using WebUI.pom.templates;

namespace WebUI.pom.HomePage
{
    public class AGDATAHeader(IWebDriver driver):GenericUI(driver, "//div/a[@class=\"site-title\"]") {}

    /// <summary>
    /// This is the Menu Item at the top where users can select solutions, overview, etc.
    /// </summary>
    /// <param name="driver"></param>
    public class Navigator(IWebDriver driver):GenericUI(driver, "//ul[@id=\"primary-menu\"]")
    {
        /// <summary>
        /// Overrides default SelectOption function. 
        /// Enables selecting a chain of items to select from a nested menu list, by specifing them in a string delimited by '>'.
        /// (E.g. MenuItem1 > MenuItem2)
        /// </summary>
        /// <param name="option"></param>
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
