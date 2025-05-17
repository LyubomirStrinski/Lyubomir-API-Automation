# Lyubomir API Automation Testing Project

This is a QA Automation project for testing the [GitHub REST API](https://docs.github.com/en/rest).  
It is built using C#, .NET, NUnit, and RestSharp.

The tests are focused on the following functionalities:
- Getting a list of issues from a public repository
- Creating a new issue
- Editing an existing issue

All tests are written using **NUnit**, and executed through **Visual Studio**.

---

## ✅ Technologies Used

- **C# / .NET 8**
- **NUnit** - test framework
- **RestSharp** - HTTP client for API testing
- **Visual Studio 2022**
- **Git / GitHub**

---

## 🔧 How to Run the Tests

 Clone this repository  
   ```bash
   git clone https://github.com/LyubomirStrinski/Lyubomir-API-Automation.git
Open the .sln file in Visual Studio

Add your GitHub Personal Access Token as an environment variable:

Go to Edit System Environment Variables

Add new variable:

Name: GITHUB_TOKEN

Value: your token (keep it secret)

Run the tests from the Test Explorer in Visual Studio

🔒 Security Notes
The GitHub token is not hardcoded in the source code.

It is securely retrieved from the GITHUB_TOKEN environment variable.

This is done to protect your GitHub account and follow best practices.

📁 Structure
bash
Copy
Edit
Lyubomir-API-Automation/
│
├── Lyubomir.API.Tests/           # Main test project
│   ├── UnitTestsDemo.cs
│   ├── ZippopotamusTest.cs
│   ├── Models/
│   │   ├── Issue.cs
│   │   ├── Location.cs
│   │   └── Place.cs
│   └── GlobalUsings.cs
│
├── README.md                     # Project overview
└── Lyubomir-API-Automation.sln   # Solution file
🧪 Example Tests
✅ Test_GitHubAPIRequest

✅ Test_GetAllIssuesFromARepo

✅ Test_CreateGitHubIssue

✅ Test_EditIssue

✅ Test_Zippopotamus_GetDataForBG

👨‍💻 Author
Lyubomir Strinski
Junior QA Automation Engineer
LinkedIn: https://www.linkedin.com/in/lyubomir-strinski-169886280/