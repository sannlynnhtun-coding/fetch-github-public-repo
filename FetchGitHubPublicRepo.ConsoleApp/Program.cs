using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Octokit;

var client = new GitHubClient(new Octokit.ProductHeaderValue("MyGitHubApp"));

string owner = "burma-project-ideas";
string repo = "myanmar-proverbs";
string path = "MyanmarProverbs.json";
string branch = "main";

try
{
    var folderContents = await client.Repository
        .Content
        .GetAllContentsByRef(owner, repo, path, branch);
    var repositoryContents = folderContents
        .Where(file => file.Type == Octokit.ContentType.File && file.Name.EndsWith(".json"))
        .ToList();
    foreach (var repository in repositoryContents)
    {
        var data = JsonConvert.DeserializeObject<JToken>(repository.Content)!;
        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex}");
}