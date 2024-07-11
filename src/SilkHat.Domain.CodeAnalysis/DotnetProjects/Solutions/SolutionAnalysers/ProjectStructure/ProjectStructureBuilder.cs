using System.Text.Json;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;
using SilkHat.Domain.Common;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure
{
    public class ProjectStructureBuilder : IProjectStructureBuilder
    {
        public ProjectStructureModel ProjectStructure(ProjectModel projectModel,
            IEnumerable<DocumentModel> documentModels)
        {
            List<DocumentModel> docs = documentModels.ToList();
            List<ProjectStructureModel> fileSystemModels = new();

            List<string> filePaths = docs
                .Select(documentModel => documentModel.Path)
                .Select(path => path.Replace('/', '\\'))
                .ToList();

            string commonRoot = PathUtilities.CommonParent(filePaths);

            Console.WriteLine($"Common root: {commonRoot}");

            filePaths = filePaths
                .Select(path => path!.Replace(commonRoot, ""))
                .Select(StripLeadingPathSeparator)
                .ToList();

            Console.WriteLine(JsonSerializer.Serialize(filePaths));

            ProjectStructureModel root = new(
                projectModel.Name,
                new List<ProjectStructureModel>(),
                ProjectStructureType.Project);

            foreach (string path in filePaths)
            {
                string[] parts = path.Split('\\');
                EnsurePartExists(root, parts);
            }

            return root;
        }

        private static void EnsurePartExists(ProjectStructureModel projectStructureModel, IEnumerable<string> parts)
        {
            List<string> list = parts.ToList();
            switch (list.Count)
            {
                case > 1:
                {
                    string name = list.First();

                    ProjectStructureModel? child = AddChild(projectStructureModel, name, ProjectStructureType.Folder);

                    EnsurePartExists(child, list.Skip(1));
                    break;
                }
                case 1:
                {
                    string name = list.First();

                    AddChild(projectStructureModel, name, ProjectStructureType.File);

                    break;
                }
            }
        }

        private static ProjectStructureModel AddChild(ProjectStructureModel projectStructureModel,
            string name, ProjectStructureType projectStructureType)
        {
            ProjectStructureModel? child = projectStructureModel.Children.SingleOrDefault(x => x.Name == name);

            if (child != null) return child!;

            child = new ProjectStructureModel(name, new List<ProjectStructureModel>(), projectStructureType);

            projectStructureModel.Children.Add(child);

            return child!;
        }

        private string StripLeadingPathSeparator(string path)
        {
            return path[0] == '\\' ? string.Join("", path.Skip(1)) : path;
        }
    }
}