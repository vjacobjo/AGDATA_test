using System;

namespace WebUI.pom.templates;

public interface IGenericUI
{
    public void Click();

    public void WaitUntilVisible(int timeout=30);

    public string GetText();

    public void SelectOption(string option);
}
