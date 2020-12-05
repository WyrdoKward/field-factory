using System;

namespace FieldFactory.Utility.CreatePipeline
{
    public class EntityInfo
    {
        private string _templateFile;
        private string _projectDir;
        //public string ProjectFile;
        private string _folderPath;

        private string _rootSlnFolder = "C:\\Users\\pierr\\Documents\\Code\\Repos\\field-factory\\WebAPI";

        public EntityInfo(string templateFile, string project, string specifiFolder)
        {
            _templateFile = templateFile;
            _projectDir = $"{_rootSlnFolder}\\{project}\\";
            //ProjectFile = $"{_projectDir}{project}.csproj";
            _folderPath = $"{_projectDir}{specifiFolder}\\";
        }
        public string GetTemplateFilePath()
        {
            return $"templates\\{_templateFile}.cs";
        }

        public string GetTargetFolder(){
            return _folderPath;
        }
        public string GetTargetFilePath()
        {
            return $"{_folderPath}{GetFileName()}.cs";
        }

        public string GetFileName()
        {
            return _templateFile.Replace("$", ConfigInfo.EntityName);
        }
    }
}