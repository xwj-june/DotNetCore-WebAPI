using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface IProjectsScreenUserCase
    {
        Task<IEnumerable<Project>> ViewProjectsAsync();
	}
}