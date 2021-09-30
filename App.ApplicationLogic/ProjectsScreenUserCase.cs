using MyApp.Repository;
using MyApp.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class ProjectsScreenUserCase : IProjectsScreenUserCase
    {
        private readonly IProjectRepository projectRepository;

        public ProjectsScreenUserCase(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> ViewProjectsAsync()
        {
            return await projectRepository.GetAsync();
        }
    }
}
