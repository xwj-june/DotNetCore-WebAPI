using App.Repository;
using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:44314", httpClient);

Console.WriteLine("*****************");
Console.WriteLine("Reading projects");
await GetProjects();

Console.WriteLine("*****************");
Console.WriteLine("Reading project tickets");
await GetProjectTickets(1);


Console.WriteLine("*****************");
Console.WriteLine("Create project");
var pId = await CreatProject();
await GetProjects();


Console.WriteLine("*****************");
Console.WriteLine("Update a project");
var project = await GetProject(pId);
project.Name += " updated";
await UpdateProject(project);
await GetProjects();


Console.WriteLine("*****************");
Console.WriteLine("Delete a project");
await DeleteProject(pId);
await GetProjects();

async Task GetProjects()
{
    ProjectRepository repository = new(apiExecuter);
    var projects = await repository.GetAsync();
    foreach (var project in projects)
    {

        Console.WriteLine($"Project: {project.Name}");
    }
}

async Task<Project> GetProject(int id)
{
    ProjectRepository repository = new(apiExecuter);
    return await repository.GetByIdAsync(id);
}

async Task GetProjectTickets(int id)
{
    var project = await GetProject(id);
    Console.WriteLine($"Project: {project.Name}");

    ProjectRepository repository = new(apiExecuter);
    var tickets = await repository.GetProjectTicketsAsync(id);
    foreach (var ticket in tickets)
    {
        Console.WriteLine($"    Ticket: {ticket.Title}");
    }
}

async Task<int> CreatProject()
{
    var project = new Project { Name = "Another Project" };
    ProjectRepository repository = new(apiExecuter);

    return await repository.CreateAsync(project);
}

async Task UpdateProject(Project project)
{
    ProjectRepository repository = new(apiExecuter);
    await repository.UpdateAsync(project);
}

async Task DeleteProject(int id)
{
    ProjectRepository repository = new(apiExecuter);
    await repository.DeleteAsync(id);
}