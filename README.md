# AGDATA_test
QA Technical Assessment assigned by AGDATA.
For information about the assignment details, please refer to the [appendix](#appendix-test-instructions).

# Solution Overview #

The solution uses Gauge Framework to enable defining high level data driven tests in easy edit and read spec files. The goal for this solution is to:
* Create Tests that are data-driven, where workflows can be parameterized and executed over a table of parameters that can be scaled easily
* Tests can be easily modified and readable for anyone, including a non-developer.
* Reports are illustrative and can be shown to people outside our team. Reports can even function as Living documentation to illustrate how a feature works by how it executes its tests.
* Solution is easy to debug with log files as well as rich debugging tools.

## Development Environment ##

This solution was developed on a Macbook, using VSCode. Google Chrome was the browser user for automation.

The following dependencies were installed:
* Microsoft.AspNetCore.App 8.0.8
* Microsoft.NETCore.App 8.0.8
* Microsof .NET SDK 8.0.402
* [Homebrew](https://brew.sh/) 4.4.0: A package manager used in Mac and Linux. This was used to install Gauge.
* Gauge Framework version: 1.6.9. The following additional plugins for Gauge were also installed:
** csharp (0.10.6)
** dotnet (0.7.2)
** html-report (4.3.1)
** screenshot (0.3.0)

VSCode had to be installed with the following extensions:
* [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
* [Gauge](https://marketplace.visualstudio.com/items?itemName=getgauge.gauge)

These dependencies were installed within the C# projects via nuget packages:
* FluentAssertions (6.12.0)
* Gauge.CSharp.Lib (0.11.1)
* Newtonsoft.Json (13.0.3)
* RestSharp (112.1.0)
* Selenium.Support (4.25.0)
* Selenium.WebDriver (4.25.0)
* Selenium.WebDriver.ChromeDriver (2.46.0)
* WebDriverManager (2.174)

## Repo Walkthrough ##

Here are the directories and files at the base level of the repository
* env: This is a gauge specific repo which store important environment values and configurations for executing gauge specs. Refer to [documentation](https://docs.gauge.org/configuration?os=macos&language=csharp&ide=vscode#using-environments-in-a-gauge-project)
* specs: This directory contains the High level Test steps. File format is .spec, and steps are written in english in markdown format. There are currently two files:
** Part1.spec: Implements [part 1 of the assignment](#part-1---api-testing), involving Rest API calls
** Part2.spec: Implements [part 2 of the assignment](#part-2---ui-testing-with-selenium), involving Selenium and Web UI testing
* src: This contains the C# solution and project that implement the steps used in the spec file. There are three main projects
** StepImplementation: Implements the steps used for the spec files.
** WebUI: Implements a client that wraps around Selenium library, and defines the Page Object Model of the AGDATA website used for testing.
** JsonPlaceHolder: Defines a RestAPI client for sending requests and processing responses. Serves to encapuslate the Rest Sharp client.
* testData: contains supplementary files used for the spec tests. For examples, they may contain example request payloads to use for tests.
* manifest.json: Gauge Framework required file used for noting language and plugins.

During execution, there will be certain directories that get generated:
* Chrome: This directory contains the Selnium Chrome Webdriver that will be used for WebUI tests. The driver is downloaded when launching the WebUI tests for the first time and is not committed to the repo.
* gauge_bin: Executable directory generated when gauge is called.
* logs: Logs generated by gauge during runtime that can be used for debugging.
* reports: HTML reports generated from test execution and generated here.

## Basic Execution ##

You can run the spec file from command line by navigating to the gauge repo directory and executing the following command to run all available spec files:
```
gauge run specs
```
You can also specify one specific spec file to execute by running:
```
gauge run specs/[NameofFile].spec
```
Test cases within spec files can be run in parallel by executing:
```
gauge run --parallel specs/Part1.spec 
```
After test execution, an html report is generated in the reports folder.

More information on how to execute gauge spec files can be found in the [documentation](https://docs.gauge.org/execution?os=macos&language=csharp&ide=vscode).

## Debugging ##
The gauge VSCode plugin enable a user to run the entire spec file or a specific scenario in debug mode. You can put breakpoints on any of the .cs files on the left side where the line numbers are. On a spec file, there is a linting header that allows you to select Debug Spec/Specification. Triggering it allows you to run the code in debug mode, where it will stop at breakpoints. From there you can step into, over or out a line, as well as peer through variable values in a watch window.

# Limitations #

## Reporting ##
Despite its lush vibrant reporting with ability to add screenshots, there are couple of deficiencies with reporting, specifically when executing test cases in a table as demonstrated in Part1.spec test cases 1-6. The reporting does not distinguish on the header what entry in the table was executed. You can only distiguish them by looking at the results.

There is also a [known issue](https://github.com/getgauge/gauge/issues/1960) where Table driven scenarios are always executed and reported last.

## Asynchronous Test Executions ##
While you are able to run test scenarios in parallel, you are unable to define async step implementations. This opens up a possibilty of testing simultaneous requests and checking for race condition. This also a [known issue](https://github.com/getgauge/gauge-dotnet/issues/30) in the Gauge Fraemwork.

## JsonPlaceHolder not updating ##
As noted in the [guide](https://jsonplaceholder.typicode.com/guide/), the JsonPlaceHolder does not update itself with calls. So test cases where we check to see if a post is updated after a call were out of scope.

# Future Improvements #

## Compatibility with Windows ##
This solution is configured to run on Mac. Ideally to have it run cross platform, I would have had it run on a docker container. I would have included the following:
* A Dockerfile to build the docker image to run the gauge framework. I had initially committed one, but then decided to remove it due to a lack of time. I would have also looked into debugging within a docker container as well.
* Created another env sub folder to define flags and values that pertain to windows and mac separately. Currently, in the default folder in env, I define certain flags that only pertain to the Mac (e.g. the URL to download the specific webdriver is specific to only Mac).
* Create a bash script to call the docker commands to build and run commands.

## Checking Response payloads against an expected schema ##
Currently, the Part 1 API tests check for status code and prints out the response call. I would ideally like to test the response against a schema to ensure that the response is structured properly. I would have used the [Newtonsoft.Json.Schema](https://www.newtonsoft.com/jsonschema) library to do so and implement specific test cases for this.

# References #

* [Gauge Framework Documentation](https://docs.gauge.org/?os=macos&language=csharp&ide=vscode)
* [JsonPlaceHolder Guide](https://jsonplaceholder.typicode.com/guide)
* [RestSharp Documentation](https://restsharp.dev/docs/intro)
* [Selenium Documentation](https://www.selenium.dev/documentation/)

# Appendix: Test Instructions #

As a contributor to the team in AGDATA, part of your responsibility will be writing automated tests at least at the API and UI level.  We expect our SDETs to be extremely comfortable with Visual Studio and GIT, be able to put together well-designed test plan, develop and run reliable automated regression test suites.
Create a public GitHub repository to complete the following exercise and reply with the GitHub URL.  There will be two parts, one for API testing and one for UI using Selenium.

### Part 1 - API Testing ###
In C#, develop a suite of automated API tests against API end points from https://jsonplaceholder.typicode.com/guide.  It is up to you to design and be able to explain why you built it the way you did.  You should use this an as opportunity to demonstrate to AGDATA your approach to automation and your technical expertise.  You are expected to compile and run these tests during the interview.  If you are unable to complete the test suite to your satisfaction, be prepared to explain what additional functionalities and how much more time you need to complete.  Below are the end points you should cover:
• GET https://jsonplaceholder.typicode.com/posts
• POST https://jsonplaceholder.typicode.com/posts
• PUT https://jsonplaceholder.typicode.com/posts/{postId}
• DELETE https://jsonplaceholder.typicode.com/posts/{postId}
• POST https://jsonplaceholder.typicode.com/posts/{postId}/comments
• GET https://jsonplaceholder.typicode.com/comments?postId={postId}

### Part 2 - UI Testing with Selenium  ###
Use the same repo but add a second project to the VS solution for UI testing.  Once again, this is to showcase your technical abilities and keep in mind DRY principal when creating the page objects.  We will run and debug the flow during the technical interview process.
Workflow:
• Open a browser and navigate to "www.agdata.com"
• On the top navigation menu click on "Company" > "Overview"
• On the "https://www.agdata.com/company/" page, get back the headings of the 'Our Values' section on the page in a LIST.
• Click on the "Let's Get Started" button at the bottom
• Validate that the 'Contact' page is displayed/loaded

### Project Acceptance Criteria ###  
• The code base is well organized, easily readable and appropriately commented
• The project's git commit history is well commented and easy to understand
• The tests are reliable, and can be executed numerous times in a row without failures
• Happy path tests were built for all major endpoints
• Negative path tests were built for testing standard failures
• Tests have detailed logs or reports helping to troubleshoot failures
• Tests can be executed in parallel
• Tests are data driven for maximum coverage
