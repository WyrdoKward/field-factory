using FieldFactory.Core.Utils.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FieldFactory.Utility.CreatePipeline
{
    public class Program
    {
        static Dictionary<string, string> PlaceHolders = new Dictionary<string, string>() {
            { "$interactorParam$", "$interactorParam$" },
            { "$interactorFlatParam$", "$interactorFlatParam$" },
            { "$providerRawDtoParams$", "$providerRawDtoParams$" },
            { "$entityName$", "$entityName$" },
            { "$entityNameLowerCase$", "$entityNameLowerCase$" },
            { "$entityPath$", "" },
            { "$nbColTable$", "$nbColTable$" },
            { "$firstParamWithType$", "$firstParamWithType$" },
            { "$firstParamName$", "$firstParamName$" },
            { "$dtoToEntityLine$", "$dtoToEntityLine$" },
            { "$updateSetValues$", "$updateSetValues$" },
            { "$flatParamsMin$", "$flatParamsMin$" },
            { "$queryFlatParams$", "$queryFlatParams$" },
            { "$insertValues$", "$insertValues$" },
            { "$discriminatingFieldsComparison$", "$discriminatingFieldsComparison$" },
            { "$discriminatingFieldsComparisonFromDto$", "$discriminatingFieldsComparisonFromDto$" },
            { "$discriminatingFields$", "$discriminatingFields$" },
            { "$discriminatingFlatParams$", "$discriminatingFlatParams$" },
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
            {"$BLOCK_dtoMethods$", ""},
            {"$BLOCK_discriminatingPublicFields$", ""},
            {"$BLOCK_discriminatingFieldsAssignation$", ""}
        };

        static bool _isJsonDto;
        static string _entityPath;
        static void Main(string[] args)
        {
            //Configuration
            Console.WriteLine("Please enter the name of the new Entity to Create : ");
            string entityToCreate = Console.ReadLine().FirstCharToUpper();
            PlaceHolders["$entityName$"] = entityToCreate;
            PlaceHolders["$entityNameLowerCase$"] = entityToCreate.ToLower();

            Console.WriteLine("==> Enter Interactor and Entity path ");
            Console.WriteLine("    (In /Core/{path} and /Core/Entities/{path})");
            _entityPath = Console.ReadLine();
            PlaceHolders["$entityPath$"] = !string.IsNullOrEmpty(_entityPath) ? $".{_entityPath}" : "";

            //TODO sera détecté automatiquement
            Console.WriteLine($"==> Is Dto in Json ? (y/n)");
            _isJsonDto = Console.ReadLine() == "y" ? true : false;
            string staticFolder = "";
            try
            {
                if (_isJsonDto)
                {
                    staticFolder = "StaticRessource";
                    PlaceHolders["$StaticRessourcesNamespace$"] = ".StaticRessource";
                }
                else
                {

                    ConfigureNonJsonEntityFieldsAndPopulateBlocks();

                }
                PlaceHolders["$interactorParam$"] = UtilityLogic.BuildInteractorParam(_isJsonDto);
                PlaceHolders["$interactorFlatParam$"] = UtilityLogic.BuildInteractorFlatParam(_isJsonDto);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("");
                Console.WriteLine("*****CONFIGURATION ERROR*****");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
                return;
            }


            foreach (var placeHolder in PlaceHolders)
            {
                Console.WriteLine($"{placeHolder.Key} : {placeHolder.Value}");
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
            string queryTemplate = $"{staticFolder}\\Get$Query";
            CreateFileFromTemplate(new EntityInfo(entityToCreate, queryTemplate, "FieldFactory.Framework", "Query"));

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

        internal static void ConfigureNonJsonEntityFieldsAndPopulateBlocks()
        {
            Console.WriteLine("");
            Console.WriteLine("Please paste here SQLite (CREATE param section)");
            Console.WriteLine("It should look like this :");
            Console.WriteLine("\"idPlayer\" text,\"intColumn\" integer, \"dateColumn\" datetime");
            string cols = Console.ReadLine();

            //Initialiaze EntityFields
            UtilityLogic.ParseSQLiteParams(cols);
            PlaceHolders["$nbColTable$"] = UtilityLogic.EntityFields.Count.ToString();

            Console.WriteLine("");
            Console.WriteLine($"What are the sql discriminating fields ? (leave empty to use {UtilityLogic.EntityFields.FirstOrDefault().Key})");
            Console.WriteLine("Separated by comma like this :");
            Console.WriteLine("idPlayer, otherId");
            string discriminating = Console.ReadLine();
            string wrongParam = UtilityLogic.ParseSQLiteDiscriminatingParams(discriminating);
            while (!string.IsNullOrEmpty(wrongParam))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Parameter {wrongParam} doesn't exist ! Please Re-enter discriminating params.");
                Console.ResetColor();
                discriminating = Console.ReadLine();
                wrongParam = UtilityLogic.ParseSQLiteDiscriminatingParams(discriminating);
            }


            //populate blocks
            BlocksToReplace["$BLOCK_publicFields$"] = UtilityLogic.BuildPublicFieldBlock(UtilityLogic.EntityFields);
            BlocksToReplace["$BLOCK_ConstructorParams$"] = UtilityLogic.BuildConstructorDtoParams(UtilityLogic.EntityFields);
            BlocksToReplace["$BLOCK_fieldsAssignation$"] = UtilityLogic.BuildFieldAssignationBlock(UtilityLogic.EntityFields, false, false);
            BlocksToReplace["$BLOCK_constructorWithDtoFieldsAssignation$"] = UtilityLogic.BuildFieldAssignationBlock(UtilityLogic.EntityFields, false, true);
            BlocksToReplace["$BLOCK_dtoMethods$"] = UtilityLogic.BuildEntityConvertToDTOMethods();
            BlocksToReplace["$BLOCK_discriminatingPublicFields$"] = UtilityLogic.BuildPublicFieldBlock(UtilityLogic.DiscriminatingFields);
            BlocksToReplace["$BLOCK_discriminatingFieldsAssignation$"] = UtilityLogic.BuildFieldAssignationBlock(UtilityLogic.DiscriminatingFields, true, false);

            PlaceHolders["$providerRawDtoParams$"] = UtilityLogic.BuildFlatParamsFromRaw();
            PlaceHolders["$firstParamWithType$"] = UtilityLogic.BuildMethodParamFirstOnly();
            PlaceHolders["$firstParamName$"] = UtilityLogic.GetFirstParamName();
            PlaceHolders["$flatParamsMin$"] = UtilityLogic.BuildFlatParams(UtilityLogic.EntityFields, false);
            PlaceHolders["$insertValues$"] = UtilityLogic.BuildInsertValues();
            PlaceHolders["$updateSetValues$"] = UtilityLogic.BuildUpdateSetOrEqualValues(UtilityLogic.EntityFields, true);
            PlaceHolders["$discriminatingFields$"] = UtilityLogic.BuildConstructorDtoParams(UtilityLogic.DiscriminatingFields);
            PlaceHolders["$discriminatingFieldsComparison$"] = UtilityLogic.BuildUpdateSetOrEqualValues(UtilityLogic.DiscriminatingFields, false);
            PlaceHolders["$discriminatingFieldsComparisonFromDto$"] = UtilityLogic.BuildUpdateSetOrEqualValues(UtilityLogic.DiscriminatingFields, true);
            PlaceHolders["$queryFlatParams$"] = UtilityLogic.BuildFlatParams(UtilityLogic.DiscriminatingFields, true, "query.");
            PlaceHolders["$discriminatingFlatParams$"] = UtilityLogic.BuildFlatParams(UtilityLogic.DiscriminatingFields, false, "");


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

                        //Demandes à la volée
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
