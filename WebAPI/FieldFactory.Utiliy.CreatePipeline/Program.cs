using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace test_dotnet
{
    public class Program
    {
        static Dictionary<string, string> PlaceHolders = new Dictionary<string, string>() {
            { "entity", "$safeName$" },
            { "otherInteractors", "$otherInteractors$" },
            { "nbColTable", "$nbColTable$" },
            { "jsonField", "$jsonField$" },
            { "jsonAssignation", "$jsonAssignation$" },
        };

        static string _nbColTable;
        static string _isJsonDto;
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the name of the new Entity to Create : ");
            string entityToCreate = Console.ReadLine();


            //Executor
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***EXECUTOR***");
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "$Executor", "FieldFactory.Framework", "Executor"));


            //Interactor
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***INTERACTOR***");
            Console.WriteLine("==> Enter Interactor and Entity path ");
            Console.WriteLine("    (In /Core/{path} and /Core/Entities/{path})");
            string path = Console.ReadLine();
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "$Interactor", "FieldFactory.Core", path));

            //Entity
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***ENTITY***");
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "$", "FieldFactory.Core", $"Entities\\{path}"));

            //Provider
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***PROVIDER***");
            Console.WriteLine("==> Enter number of columns in db for this entity :");
            _nbColTable = Console.ReadLine();
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "$Provider", "FieldFactory.DataAccess", "Providers"));

            //SqLiteQueryBuilder
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***SQLITEQUERYBUILDER***");
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "SQLite$QueryBuilder", "FieldFactory.DataAccess", "SQLite"));

            //DTO
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***DTO***");
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "$DTO", "FieldFactory.DataAccess", "DTO"));

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Done ! Click to exit");
            Console.ReadLine();

        }

        private static void CreateFileFromTemplate(EntityInfo info)
        {
            Console.WriteLine($"    Creating {info.GetFileName()}...");
            string line = "";

            if (!Directory.Exists(info.FolderPath))
            {
                Directory.CreateDirectory(info.FolderPath);
                Console.WriteLine($"    Creating folder '{info.FolderPath}'...");
            }
            if (info.NamePattern.Contains("DTO"))
            {
                Console.WriteLine($"==> Is Dto in Json ? (y/n)");
                _isJsonDto = Console.ReadLine();
            }

            using (StreamReader sr = new StreamReader($"templates/{info.NamePattern}Template.txt"))
            {
                using (StreamWriter sw = new StreamWriter(info.GetFilePath()))
                {

                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Replace(PlaceHolders["entity"], info.Name);

                        if (line.Contains(PlaceHolders["otherInteractors"]))
                        {
                            line = AddOtherInteractorsForExecutor();
                        }

                        line = line.Replace(PlaceHolders["nbColTable"], _nbColTable);

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
                    sb.AppendLine($"{otherInteractor}Interactor {otherInteractor.ToLower()}Interactor = new {otherInteractor}Interactor();");
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
