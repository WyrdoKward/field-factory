using System.IO;
using System;

namespace FieldFactory.Utility.CreatePipeline
{
    public class EntityInfo
    {
        private string _fileName;
        private string _templateFile;
        private string _projectDir;
        //public string ProjectFile;
        private string _folderPath;

        private string _rootSlnFolder = Path.Combine("..",""); 
        //private string _rootSlnFolder = "C:\\Users\\pierr\\Documents\\Code\\Repos\\field-factory\\WebAPI";

        public EntityInfo(string templateFile, string project, string specifiFolder, bool staticTemplate = false)
        {
            if(templateFile.StartsWith("StaticRessource")){
                _fileName = templateFile.Substring(16);
            }
            else{
                _fileName = templateFile;
            }
            
            _templateFile = templateFile;
            _projectDir = $"{_rootSlnFolder}\\{project}\\";
            //ProjectFile = $"{_projectDir}{project}.csproj";
            _folderPath = Path.Combine(_rootSlnFolder, project, specifiFolder);
            //_folderPath = $"{_projectDir}{specifiFolder}\\";
        }
        public string GetTemplateFilePath()
        {
            return Path.Combine("templates", _templateFile);
            //return $"templates\\{_templateFile}";
        }

        public string GetTargetFolder()
        {
            return _folderPath;
        }
        public string GetTargetFilePath()
        {
            //return $"{_folderPath}{GetFileName()}.cs";
            return Path.Combine(_folderPath, $"{GetFileName()}.cs");
        }

        public string GetFileName()
        {
            return _fileName.Replace("$", ConfigInfo.EntityName);
        }

        public bool CanBeOverwritten()
        {
            if (_templateFile == "$" || _templateFile == "$DTO")
                return true;

            return false;
        }
    }
}