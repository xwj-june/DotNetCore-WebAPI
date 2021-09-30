using App.Repository;
using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ApplicationLogic
{
    public class ProjectsScreenUserCase
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
