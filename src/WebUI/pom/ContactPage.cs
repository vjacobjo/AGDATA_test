using OpenQA.Selenium;
using WebUI.pom.templates;

namespace WebUI.pom.ContactPage
{
    public class ContactHeader(IWebDriver driver):GenericUI(driver, "//h4[contains(text(), \"Contact\")]"){}
    public class MessageBox(IWebDriver driver):GenericUI(driver, "//textarea[contains(@placeholder, \"Message\")]"){}
    public class SubmitButton(IWebDriver driver):GenericUI(driver, "//input[@value=\"Submit\"]"){}
}
