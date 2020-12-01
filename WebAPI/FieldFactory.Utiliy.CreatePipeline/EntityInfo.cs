using System;

namespace FieldFactory.Utility.CreatePipeline
{
    public class EntityInfo
    {
        public string Name;
        public string NamePattern;
        public string ProjectDir;
        public string ProjectFile;
        public string FolderPath;

        private string _rootSlnFolder = "C:\\Users\\pierr\\Documents\\Code\\Repos\\field-factory\\WebAPI";

        public EntityInfo(string name, string namePattern, string project, string folderPath){
            Name = name;
            NamePattern = namePattern;
            ProjectDir = $"{_rootSlnFolder}\\{project}\\";
            ProjectFile = $"{ProjectDir}{project}.csproj";
            FolderPath = $"{ProjectDir}{folderPath}\\";
        }

        public string GetFilePath(){
            return $"{FolderPath}{GetFileName()}.cs";
        }

        public string GetFileName(){
            return NamePattern.Replace("$", Name);
        }
    }
}