using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure
{
    public interface IProjectStructureBuilder
    {
        ProjectStructureModel ProjectStructure(ProjectModel projectModel,
            IEnumerable<DocumentModel> documentModels);
    }
}