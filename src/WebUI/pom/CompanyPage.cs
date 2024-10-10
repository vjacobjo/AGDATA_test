/// <summary>
/// This is page loaded from https://www.agdata.com/contact/.
/// </summary>
using OpenQA.Selenium;
using WebUI.pom.templates;

namespace WebUI.pom.CompanyPage
{
    public class OverviewHeader(IWebDriver driver):GenericUI(driver, "//h1[contains(text(), \"Overview\")]") {}
    public class ValueCards(IWebDriver driver):GenericUI(driver, "//div[@class=\"col box one-fourth\"]/h3") {}
    public class LetsGetStartedButton(IWebDriver driver):GenericUI(driver, "//a[@href=\"/contact\"]") {}
}
