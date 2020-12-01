using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace test_dotnet
{
    public class Program
    {
        static Dictionary<string, string> PlaceHolders = new Dictionary<string, string>() {
            { "$entityName$", "$entityName$" },
            { "$entityNameLowerCase$", "$entityNameLowerCase$" },
            { "$entityPath$", "$otherInteractors$" },
            { "$otherInteractors$", "$otherInteractors$" },
            { "$nbColTable$", "$nbColTable$" },
            { "$dtoToEntityLine$", "$dtoToEntityLine$" },
            { "$StaticRessourcesNamespace$", "" },
        };

        static string _isJsonDto;
        static string _entityPath;
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the name of the new Entity to Create : ");
            string entityToCreate = Console.ReadLine();
            PlaceHolders["$entityName$"] = entityToCreate;
            PlaceHolders["$entityNameLowerCase$"] = entityToCreate.ToLower();

            Console.WriteLine("==> Enter Interactor and Entity path ");
            Console.WriteLine("    (In /Core/{path} and /Core/Entities/{path})");
            _entityPath = Console.ReadLine();
            PlaceHolders["$entityPath$"] = _entityPath;

            Console.WriteLine($"==> Is Dto in Json ? (y/n)");
            _isJsonDto = Console.ReadLine();
            string staticFolder = "";
            if (_isJsonDto == "y")
            {
                staticFolder = "StaticRessource\\";
                PlaceHolders["$StaticRessourcesNamespace$"] = ".StaticRessource";
            }


            //Executor
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***EXECUTOR***");
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "$Executor", "FieldFactory.Framework", "Executor"));


            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***QUERY***");
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "Get$Query", "FieldFactory.Framework", "Query"));

            //Interactor
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***INTERACTOR***");
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "$Interactor", "FieldFactory.Core", _entityPath));

            //Entity
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***ENTITY***");
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "$", "FieldFactory.Core", $"Entities\\{_entityPath}"));

            //Provider
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***PROVIDER***");
            string providerTemplate = $"{staticFolder}$Provider";
            string providerFolder = $"{staticFolder}Providers";
            Console.WriteLine("==> Enter number of columns in db for this entity :");
            PlaceHolders["$nbColTable$"] = Console.ReadLine();
            CreateFileFromTemplate(new EntityInfo(entityToCreate, providerTemplate, "FieldFactory.DataAccess", providerFolder));

            //SqLiteQueryBuilder
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***SQLITEQUERYBUILDER***");
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "SQLite$QueryBuilder", "FieldFactory.DataAccess", "SQLite"));

            //DTO
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***DTO***");
            string dtoTemplate = $"{staticFolder}$DTO";
            string dtoFolder = $"{staticFolder}DTO";
            CreateFileFromTemplate(new EntityInfo(entityToCreate, dtoTemplate, "FieldFactory.DataAccess", dtoFolder));

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Done ! Click to exit");
            Console.ReadLine();

        }

        private static void CreateFileFromTemplate(EntityInfo info)
        {
            if (File.Exists(info.GetFilePath()))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File {info.GetFilePath()} already exists.");
                Console.WriteLine($"Skipping to next file to create...");
                Console.ResetColor();
                return;
            }
            Console.WriteLine($"    Creating {info.GetFileName()}...");
            string line = "";

            if (!Directory.Exists(info.FolderPath))
            {
                Directory.CreateDirectory(info.FolderPath);
                Console.WriteLine($"    Creating folder '{info.FolderPath}'...");
            }

            using (StreamReader sr = new StreamReader($"templates\\{info.NamePattern}Template.txt"))
            {
                using (StreamWriter sw = new StreamWriter(info.GetFilePath()))
                {

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains(PlaceHolders["$otherInteractors$"]))
                        {
                            line = AddOtherInteractorsForExecutor();
                        }
                        //Configure variable lines
                        if (line.Contains(PlaceHolders["$dtoToEntityLine$"]))
                        {
                            line = BuildDtoToEntityConvertion();
                        }
                        if (line.Contains(PlaceHolders["$jsonDefaultFields$"]))
                        {
                            line = BuildJsonDefaultFields();
                        }
                        if (line.Contains(PlaceHolders["$dtoMethods$"]))
                        {
                            line = BuildEntityDtoMethods();
                        }

                        //Replace placeHolders
                        foreach (var placeHolder in PlaceHolders)
                        {
                            line = line.Replace(placeHolder.Key, placeHolder.Value);
                        }




                        if (_isJsonDto == "y")
                        {
                            line = line.Replace(PlaceHolders["jsonField"], "public string Json;");
                            line = line.Replace(PlaceHolders["jsonAssignation"], "Json = json;");
                        }

                        sw.WriteLine(line);
                        //Console.WriteLine(line);
                    }
                }
            }

            Console.WriteLine($"    File {info.GetFilePath()} created");

        }


        private static string AddOtherInteractorsForExecutor()
        {
            StringBuilder sb = new StringBuilder();
            string otherInteractor = "123";
            while (otherInteractor != "")
            {
                Console.WriteLine("==> Enter name of other Interactor used by executor if needed : (press 'Enter' to skip)");
                otherInteractor = Console.ReadLine();
                if (!string.IsNullOrEmpty(otherInteractor))
                    sb.AppendLine($"\t\t{otherInteractor}Interactor {otherInteractor.ToLower()}Interactor = new {otherInteractor}Interactor();");
            }
            return sb.ToString();
        }

        private static string BuildDtoToEntityConvertion()
        {
            StringBuilder sb = new StringBuilder();
            if (_isJsonDto == "y")
            {
                sb.AppendLine("var $entityNameLowerCase$ = JsonConvert.DeserializeObject<$entityName$>($entityNameLowerCase$Dto.Json);");
            }
            else
            {
                sb.AppendLine("var $entityNameLowerCase$ = new $entityName$($entityNameLowerCase$Dto);");
            }
            return sb.ToString();

        }

        private static string BuildJsonDefaultFields()
        {
            StringBuilder sb = new StringBuilder();
            if (_isJsonDto == "y")
            {
                sb.AppendLine("\t\tpublic int Id;");
                sb.AppendLine("\t\tpublic string Title;");
                sb.AppendLine("\t\tpublic string Description;");
            }
            else
            {
                sb.AppendLine("\t\tpublic string IdPlayer { get; set; }");
            }
            return sb.ToString();
        }

        private static string BuildEntityDtoMethods()
        {
            StringBuilder sb = new StringBuilder();
            if (_isJsonDto != "y")
            {
                sb.AppendLine("\t\tpublic $entityName$($entityName$DTO dto)");
                sb.AppendLine("\t\t{");
                sb.AppendLine("\t\t\tIdPlayer = dto.IdPlayer;");
                sb.AppendLine("\t\t}");
                sb.AppendLine("");
                sb.AppendLine("\t\tpublic $entityName$DTO ConvertToDTO()");
                sb.AppendLine("\t\t{");
                sb.AppendLine("\t\t\treturn new $entityName$DTO(IdPlayer);");
                sb.AppendLine("\t\t}");
            }
            return sb.ToString();
        }

        private static string ConfigureDTO()
        {

            StringBuilder sb = new StringBuilder();
            Console.WriteLine("Is it a json DTO ? (= static entity all stored in json in db)");
            Console.WriteLine("(y/n)");
            string isJson = Console.ReadLine();
            if (isJson == "y")
            {
                //on créé juste le champ id et Json
                sb.AppendLine("ConfigureDTO = y");

            }
            else
            {
                sb.AppendLine("ConfigureDTO = n");

            }
            //sinon je sais pas encore, on laisse un DTO vide ?

            return sb.ToString();
        }

    }
}
