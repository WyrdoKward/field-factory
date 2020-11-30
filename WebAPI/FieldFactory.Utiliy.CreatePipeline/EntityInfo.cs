using System;

namespace test_dotnet
{
    public class EntityInfo
    {
        public string Name;
        public string NamePattern;
        public string Project;
        public string FolderPath;

        public EntityInfo(string name, string namePattern, string project, string folderPath){
            Name = name;
            NamePattern = namePattern;
            Project = $"{project}/{project}.csproj";
            FolderPath = $"{project}/{folderPath}/";;
        }

        public string GetFilePath(){
            return $"{FolderPath}{GetFileName()}.cs";
        }

        public string GetFileName(){
            return NamePattern.Replace("$", Name);
        }
    }
}