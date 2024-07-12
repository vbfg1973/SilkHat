using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure
{
    public interface IProjectStructureBuilder
    {
        Task<ProjectStructureModel> ProjectStructure(ProjectModel projectModel,
            IEnumerable<DocumentModel> documentModels);
    }
}