# AGDATA_test
QA Technical Assessment assigned by AGDATA

# Test Overview #

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

# Solution #

## Setup ##

1. Checkout this repo.

2. Navigate to the directory and run the following command.
    ```
    docker build -t testimg1 .
    ```
3. Run the container by executing the following command.
    ```
    docker run -t testimg1 testcntr
    ```
