using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FieldFactory.Utility.CreatePipeline
{
    public class Program
    {
        static Dictionary<string, string> PlaceHolders = new Dictionary<string, string>() {
            { "$entityName$", "$entityName$" },
            { "$entityNameLowerCase$", "$entityNameLowerCase$" },
            { "$entityPath$", "$otherInteractors$" },
            { "$nbColTable$", "$nbColTable$" },
            { "$dtoToEntityLine$", "$dtoToEntityLine$" },
            { "$jsonDefaultFields$", "$jsonDefaultFields$" },
            { "$StaticRessourcesNamespace$", "" },
        };
        static Dictionary<string, string> BlocksOnTheFly = new Dictionary<string, string>()
        {
            {"$BLOCK_otherInteractors$", "$BLOCK_otherInteractors$"},
        };

        static Dictionary<string, string> BlocksToReplace = new Dictionary<string, string>()
        {
            {"$BLOCK_publicFields$", ""},
            {"$BLOCK_ConstructorParams$", ""},
            {"$BLOCK_fieldsAssignation$", ""},
            {"$BLOCK_constructorWithDtoFieldsAssignation$", ""},
            {"$BLOCK_dtoMethods$", ""}
        };

        static Dictionary<string, string> EntityFields = new Dictionary<string, string>();

        static bool _isJsonDto;
        static string _entityPath;
        static void Main(string[] args)
        {
            //Configuration
            Console.WriteLine("Please enter the name of the new Entity to Create : ");
            string entityToCreate = Console.ReadLine();
            PlaceHolders["$entityName$"] = entityToCreate;
            PlaceHolders["$entityNameLowerCase$"] = entityToCreate.ToLower();

            Console.WriteLine("==> Enter Interactor and Entity path ");
            Console.WriteLine("    (In /Core/{path} and /Core/Entities/{path})");
            _entityPath = Console.ReadLine();
            PlaceHolders["$entityPath$"] = $".{_entityPath}";

            Console.WriteLine($"==> Is Dto in Json ? (y/n)");
            _isJsonDto = Console.ReadLine() == "y" ? true : false; ;

            string staticFolder = "";
            if (_isJsonDto)
            {
                staticFolder = "StaticRessource";
                PlaceHolders["$StaticRessourcesNamespace$"] = ".StaticRessource";
            }
            else
            {
                ConfigureEntityFieldsAndPopulateBlocks();
            }


            //Executor
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***EXECUTOR***");
            CreateFileFromTemplate(new EntityInfo(entityToCreate, "$Executor", "FieldFactory.Framework", "Executor"));

            //Query
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
            string entityTemplate = $"{staticFolder}\\$";
            CreateFileFromTemplate(new EntityInfo(entityToCreate, entityTemplate, "FieldFactory.Core", $"Entities\\{_entityPath}"));

            //Provider
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***PROVIDER***");
            string providerTemplate = $"{staticFolder}\\$Provider";
            string providerFolder = $"Providers\\{staticFolder}";
            if (_isJsonDto)
            {
                Console.WriteLine("==> Enter number of columns in db for this entity :");
                PlaceHolders["$nbColTable$"] = Console.ReadLine();
            }
            CreateFileFromTemplate(new EntityInfo(entityToCreate, providerTemplate, "FieldFactory.DataAccess", providerFolder));

            //SqLiteQueryBuilder
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***SQLITEQUERYBUILDER***");
            string builderTemplate = $"{staticFolder}\\SQLite$QueryBuilder";
            string builderFolder = $"SQLite\\{staticFolder}";
            CreateFileFromTemplate(new EntityInfo(entityToCreate, builderTemplate, "FieldFactory.DataAccess", builderFolder));

            //DTO
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***DTO***");
            string dtoTemplate = $"{staticFolder}\\$DTO";
            string dtoFolder = $"DTO\\{staticFolder}";
            CreateFileFromTemplate(new EntityInfo(entityToCreate, dtoTemplate, "FieldFactory.DataAccess", dtoFolder));

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Done ! Click to exit");
            Console.ReadLine();

        }

        internal static void ConfigureEntityFieldsAndPopulateBlocks()
        {
            Console.WriteLine("");
            Console.WriteLine("Please paste here SQLite CREATE param section)");
            Console.WriteLine("It should look like this :");
            Console.WriteLine("\"idPlayer\" text,\"intColumn\" integer, \"dateColumn\" datetime");
            string cols = Console.ReadLine();

            EntityFields = UtilityLogic.ParseSQLiteParams(cols);

            //populate blocks
            BlocksToReplace["$BLOCK_publicFields$"] = UtilityLogic.BuildPublicFieldBlock(EntityFields);
            BlocksToReplace["$BLOCK_ConstructorParams$"] = UtilityLogic.BuildConstructorDtoParams(EntityFields);
            BlocksToReplace["$BLOCK_fieldsAssignation$"] = UtilityLogic.BuildFieldAssignationBlock(EntityFields, false);
            BlocksToReplace["$BLOCK_constructorWithDtoFieldsAssignation$"] = UtilityLogic.BuildFieldAssignationBlock(EntityFields, true);
            BlocksToReplace["$BLOCK_dtoMethods$"] = UtilityLogic.BuildEntityConvertToDTOMethods(EntityFields);
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
                        //Replace Blocks
                        foreach (var bloc in BlocksToReplace)
                        {
                            line = line.Replace(bloc.Key, bloc.Value);
                        }
                        if (line.Contains(BlocksOnTheFly["$BLOCK_otherInteractors$"]))
                        {
                            line = AddOtherInteractorsForExecutorOnTheFly();
                        }


                        //Configure variable lines
                        if (line.Contains(PlaceHolders["$dtoToEntityLine$"]))
                        {
                            line = UtilityLogic.BuildDtoToEntityConvertion(_isJsonDto);
                        }
                        if (line.Contains(PlaceHolders["$jsonDefaultFields$"]))
                        {
                            line = UtilityLogic.BuildJsonDefaultFields(_isJsonDto);
                        }

                        //Replace placeHolders
                        foreach (var placeHolder in PlaceHolders)
                        {
                            line = line.Replace(placeHolder.Key, placeHolder.Value);
                        }

                        sw.WriteLine(line);
                        //Console.WriteLine(line);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"File {info.GetFilePath()} created");
            Console.ResetColor();

        }


        private static string AddOtherInteractorsForExecutorOnTheFly()
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
    }
}
