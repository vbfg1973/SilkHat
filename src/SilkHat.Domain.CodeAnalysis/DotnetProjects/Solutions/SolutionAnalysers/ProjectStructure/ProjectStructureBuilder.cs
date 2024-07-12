using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;

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

            string? commonRoot = Path.GetDirectoryName(projectModel.Path);

            filePaths = filePaths
                .ToList();

            ProjectStructureModel root = new(
                projectModel.Name,
                projectModel.Path,
                RelativePath(commonRoot!, projectModel.Path),
                new List<ProjectStructureModel>(),
                ProjectStructureType.Project);

            foreach (string fullPath in filePaths)
            {
                if (fullPath.Contains(".nuget")) continue;

                string relativePath = RelativePath(commonRoot!, fullPath);
                
                string[] parts = relativePath.Split('\\', StringSplitOptions.RemoveEmptyEntries);

                if (parts[0] != "bin" && parts[0] != "obj") CreateStructureIfNotExists(root, relativePath, fullPath, parts);
            }

            return root;
        }

        private static void CreateStructureIfNotExists(ProjectStructureModel projectStructureModel, string relativePath,
            string fullPath,
            IEnumerable<string> parts)
        {
            List<string> list = parts.ToList();
            switch (list.Count)
            {
                case > 1:
                {
                    string name = list.First();

                    ProjectStructureModel? child = AddChild(projectStructureModel, relativePath, "", name,
                        ProjectStructureType.Folder);

                    CreateStructureIfNotExists(child, relativePath, fullPath, list.Skip(1));
                    break;
                }
                case 1:
                {
                    string name = list.First();

                    AddChild(projectStructureModel, relativePath, fullPath, name, ProjectStructureType.File);

                    break;
                }
            }
        }

        private static ProjectStructureModel AddChild(ProjectStructureModel projectStructureModel, string relativePath,
            string fullPath,
            string name, ProjectStructureType projectStructureType)
        {
            ProjectStructureModel? child = projectStructureModel.Children.SingleOrDefault(x => x.Name == name);

            if (child != null) return child!;

            child = CreateChildNode(relativePath, fullPath, name, projectStructureType, child);

            projectStructureModel.Children.Add(child);

            return child!;
        }

        private static ProjectStructureModel? CreateChildNode(string relativePath, string fullPath, string name,
            ProjectStructureType projectStructureType, ProjectStructureModel? child)
        {
            return projectStructureType switch
            {
                ProjectStructureType.File => new ProjectStructureModel(
                    name, 
                    fullPath,
                    relativePath,
                    new List<ProjectStructureModel>(), 
                    projectStructureType),
                
                ProjectStructureType.Folder => new ProjectStructureModel(
                    name, 
                    "", 
                    "",
                    new List<ProjectStructureModel>(),
                    projectStructureType),
                _ => child
            };
        }

        private static string RelativePath(string commonRoot, string fullPath)
        {
            return StripLeadingPathSeparator(fullPath.Replace(commonRoot, ""));
        }

        private static string StripLeadingPathSeparator(string path)
        {
            return path[0] == '\\' ? string.Join("", path.Skip(1)) : path;
        }
    }
}