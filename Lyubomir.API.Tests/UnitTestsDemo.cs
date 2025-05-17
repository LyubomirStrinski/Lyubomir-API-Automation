using DemoNunitTest.Models;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Text.Json;

namespace DemoNunitTest
{
    public class UnitTestsDemo
    {
        RestClient client;

        [SetUp]
        public void Setup()
        {
            var githubUsername = "LyubomirStrinski";

            var githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

            if (string.IsNullOrEmpty(githubToken))
            {
                throw new Exception("GitHub token not found in environment variables. Please set GITHUB_TOKEN.");
            }

            var options = new RestClientOptions("https://api.github.com")
            {
                MaxTimeout = 3000,
                Authenticator = new HttpBasicAuthenticator(githubUsername, githubToken)
            };

            this.client = new RestClient(options);
        }

        [TearDown]
        public void Teardown()
        {
            this.client.Dispose();
        }

        [Test]
        public void Test_GitHubAPIRequest()
        {
            //var client = new RestClient("https://api.github.com");
            var request = new RestRequest("/repos/testnakov/test-nakov-repo/issues", Method.Get);
            //request.Timeout = 1000;
            var response = client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void Test_GetAllIssuesFromARepo()
        {
            var request = new RestRequest("repos/testnakov/test-nakov-repo/issues");
            var response = client.Execute(request);
            var issues = JsonSerializer.Deserialize<List<Issue>>(response.Content);

            Assert.That(issues.Count > 1);

            foreach (var issue in issues)
            {
                Assert.That(issue.id, Is.GreaterThan(0));
                Assert.That(issue.number, Is.GreaterThan(0));
                Assert.That(issue.title, Is.Not.Empty);

            }
        }

        private Issue CreateIssue(string title, string body)
        {
            var request = new RestRequest("repos/testnakov/test-nakov-repo/issues");
            request.AddBody(new { body, title });
            var response = client.Execute(request, Method.Post);
            var issue = JsonSerializer.Deserialize<Issue>(response.Content);
            return issue;
        }

        [Test]
        public void Test_CreateGitHubIssue()
        {
            string title = "This is a Demo Issue";
            string body = "QA Back-End Automation Course February 2024";
            var issue = CreateIssue(title, body);
            Assert.That(issue.id, Is.GreaterThan(0));
            Assert.That(issue.number, Is.GreaterThan(0));
            Assert.That(issue.title, Is.Not.Empty);
        }


        [Test]
        public void Test_EditIssue()
        {
            string title = "Initial Title for Edit Test";
            string body = "Issue created to be edited in the same test.";

            var createRequest = new RestRequest("repos/LyubomirStrinski/test-repo/issues");
            createRequest.AddJsonBody(new { title = title, body = body });

            var createResponse = client.Execute(createRequest, Method.Post);
            var createdIssue = JsonSerializer.Deserialize<Issue>(createResponse.Content);

            Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(createdIssue.id, Is.GreaterThan(0));

            var editRequest = new RestRequest($"repos/LyubomirStrinski/test-repo/issues/{createdIssue.number}");
            editRequest.AddJsonBody(new
            {
                title = "Edited title via automation test"
            });

            var editResponse = client.Execute(editRequest, Method.Patch);
            var editedIssue = JsonSerializer.Deserialize<Issue>(editResponse.Content);

            Assert.That(editResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(editedIssue.title, Is.EqualTo("Edited title via automation test"));
        }
    }
}