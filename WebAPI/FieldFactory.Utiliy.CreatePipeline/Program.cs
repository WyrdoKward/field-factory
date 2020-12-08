using FieldFactory.Core.Utils.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.SqlParser.Parser;

namespace FieldFactory.Utility.CreatePipeline
{
    public class Program
    {
        static string _specificEntityPath;
        static string _staticFolder = "";
        static void Main(string[] args)
        {
            //Detecting sql files
            Console.WriteLine("Reading sql files...");
            var files = Directory.GetFiles("input").Where(f => f.EndsWith(".sql"));
            Console.WriteLine($"{files.Count()} sql files detected !");
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
            Console.WriteLine("");

            foreach (var file in files)
            {
                Console.WriteLine($"**** {file} ****");
                SqlParser.ReadSqlFile(file);
                SqlParser.ParseSql();
                SqlParser.ExtractInfosFromTokens();

                ConfigureGeneric();
                ConfigInfo.PrintConfigInfos();

                Console.WriteLine("");
                Console.WriteLine("Configuration complete, press enter to create the files.");
                Console.ReadLine();
                /*foreach (var placeHolder in PlaceHolders)
                {
                    Console.WriteLine($"{placeHolder.Key} : {placeHolder.Value}");
                }*/

                WriteNewFiles(_specificEntityPath);

                ConfigInfo.Clean();
                Console.WriteLine("");
            }


            //END Main
        }


        private static void ConfigureGeneric()
        {
            ConfigInfo.PlaceHolders["$entityName$"] = ConfigInfo.EntityName;
            ConfigInfo.PlaceHolders["$entityNameLowerCase$"] = ConfigInfo.EntityName.FirstCharToLower();
            ConfigInfo.PlaceHolders["$nbColTable$"] = ConfigInfo.EntityFields.Count.ToString();

            Console.WriteLine("==> Enter Interactor and Entity specific folder if needed");
            Console.WriteLine("    (In /Core/{path} and /Core/Entities/{path})");
            _specificEntityPath = Console.ReadLine();
            ConfigInfo.PlaceHolders["$entityPath$"] = !string.IsNullOrEmpty(_specificEntityPath) ? $".{_specificEntityPath}" : "";

            _staticFolder = "";
            try
            {
                if (ConfigInfo.IsJson)
                {
                    _staticFolder = "StaticRessource";
                    ConfigInfo.PlaceHolders["$StaticRessourcesNamespace$"] = ".StaticRessource";
                    ConfigInfo.PlaceHolders["$queryFlatParams$"] = UtilityLogic.BuildFlatParams(new Dictionary<string, string>() { { "$entityName$Id", "" } }, true, "query.");
                }
                else
                {
                    ConfigInfo.PlaceHolders["$StaticRessourcesNamespace$"] = "";
                    ConfigureNonJsonEntityFieldsAndPopulateBlocks();

                }
                
                ConfigInfo.PlaceHolders["$interactorParam$"] = UtilityLogic.BuildInteractorParam();
                ConfigInfo.PlaceHolders["$interactorFlatParam$"] = UtilityLogic.BuildInteractorFlatParam();
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
        }

        private static void ConfigureNonJsonEntityFieldsAndPopulateBlocks()
        {
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine($"What are the sql discriminating fields ? (leave empty to use {ConfigInfo.EntityFields.FirstOrDefault().Key})");
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

            UtilityLogic.CurrentDictionary = ConfigInfo.EntityFields;
            //populate blocks
            ConfigInfo.BlocksToReplace["$BLOCK_publicFields$"] = UtilityLogic.BuildPublicFieldBlock();
            ConfigInfo.BlocksToReplace["$BLOCK_ConstructorParams$"] = UtilityLogic.BuildConstructorDtoParams(ConfigInfo.EntityFields);
            ConfigInfo.BlocksToReplace["$BLOCK_fieldsAssignation$"] = UtilityLogic.BuildFieldAssignationBlock(false, false);
            ConfigInfo.BlocksToReplace["$BLOCK_constructorWithDtoFieldsAssignation$"] = UtilityLogic.BuildFieldAssignationBlock(false, true);
            ConfigInfo.BlocksToReplace["$BLOCK_dtoMethods$"] = UtilityLogic.BuildEntityConvertToDTOMethods();

            ConfigInfo.PlaceHolders["$flatParamsMin$"] = UtilityLogic.BuildFlatParams(ConfigInfo.EntityFields, false);
            ConfigInfo.PlaceHolders["$updateSetValues$"] = UtilityLogic.BuildUpdateSetOrEqualValues(true);

            UtilityLogic.CurrentDictionary = ConfigInfo.DiscriminatingFields;

            ConfigInfo.BlocksToReplace["$BLOCK_discriminatingPublicFields$"] = UtilityLogic.BuildPublicFieldBlock();
            ConfigInfo.BlocksToReplace["$BLOCK_discriminatingFieldsAssignation$"] = UtilityLogic.BuildFieldAssignationBlock(true, false);

            ConfigInfo.PlaceHolders["$providerRawDtoParams$"] = UtilityLogic.BuildFlatParamsFromRaw();
            ConfigInfo.PlaceHolders["$firstParamWithType$"] = UtilityLogic.BuildMethodParamFirstOnly();
            ConfigInfo.PlaceHolders["$firstParamName$"] = UtilityLogic.GetFirstParamName();
            ConfigInfo.PlaceHolders["$insertValues$"] = UtilityLogic.BuildInsertValues();
            ConfigInfo.PlaceHolders["$discriminatingFields$"] = UtilityLogic.BuildConstructorDtoParams(ConfigInfo.DiscriminatingFields);
            ConfigInfo.PlaceHolders["$discriminatingFieldsComparison$"] = UtilityLogic.BuildUpdateSetOrEqualValues(false);
            ConfigInfo.PlaceHolders["$discriminatingFieldsComparisonFromDto$"] = UtilityLogic.BuildUpdateSetOrEqualValues(true);
            ConfigInfo.PlaceHolders["$queryFlatParams$"] = UtilityLogic.BuildFlatParams(ConfigInfo.DiscriminatingFields, true, "query.");
            ConfigInfo.PlaceHolders["$discriminatingFlatParams$"] = UtilityLogic.BuildFlatParams(ConfigInfo.DiscriminatingFields, false, "");
        }

        private static void WriteNewFiles(string _specificEntityPath)
        {
            string entityToCreate = ConfigInfo.EntityName;
            //Executor
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***EXECUTOR***");
            CreateFileFromTemplate(new EntityInfo("$Executor", "FieldFactory.Framework", "Executor"));

            //Query
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***QUERY***");
            //string queryTemplate = $"{_staticFolder}\\Get$Query";
            string queryTemplate = Path.Combine(_staticFolder, "Get$Query");
            CreateFileFromTemplate(new EntityInfo(queryTemplate, "FieldFactory.Framework", "Query"));

            //Interactor
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***INTERACTOR***");
            CreateFileFromTemplate(new EntityInfo("$Interactor", "FieldFactory.Core", _specificEntityPath));

            //Entity
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***ENTITY***");
            string entityTemplate = Path.Combine(_staticFolder, "$");
            CreateFileFromTemplate(new EntityInfo(entityTemplate, "FieldFactory.Core", Path.Combine("Entities", _specificEntityPath)));

            //Provider
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***PROVIDER***");
            string providerTemplate = Path.Combine(_staticFolder, "$Provider");
            string providerFolder = Path.Combine("Providers", _staticFolder);
            CreateFileFromTemplate(new EntityInfo(providerTemplate, "FieldFactory.DataAccess", providerFolder));

            //SqLiteQueryBuilder
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***SQLITEQUERYBUILDER***");
            string builderTemplate = Path.Combine(_staticFolder, "SQLite$QueryBuilder");
            string builderFolder = Path.Combine("SQLite", _staticFolder);
            CreateFileFromTemplate(new EntityInfo(builderTemplate, "FieldFactory.DataAccess", builderFolder));

            //DTO
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***DTO***");
            string dtoTemplate = Path.Combine(_staticFolder, "$DTO");
            string dtoFolder = Path.Combine("DTO", _staticFolder);
            CreateFileFromTemplate(new EntityInfo(dtoTemplate, "FieldFactory.DataAccess", dtoFolder));

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Done ! Click to continue");
            Console.ReadLine();
        }

        private static void CreateFileFromTemplate(EntityInfo info)
        {
            string creating = "Creating";
            if (File.Exists(info.GetTargetFilePath()))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File {info.GetTargetFilePath()} already exists.");
                //TODO if Entity ou DTO proposer d'overwrite ?
                string ow = string.Empty;
                if (info.CanBeOverwritten())
                {
                    Console.WriteLine($"Do you want to override {info.GetFileName()} with the last version ? (y/n)");
                    ow = Console.ReadLine();
                    if (ow != "y")
                    {
                        Console.WriteLine($"Skipping to next file to create...");
                        Console.ResetColor();
                        return;
                    }
                    creating = "Overwriting";
                }
                else
                {
                    Console.WriteLine($"Skipping to next file to create...");
                    Console.ResetColor();
                    return;
                }
            }
            Console.WriteLine($"    {creating} {info.GetFileName()}...");
            string line = "";

            if (!Directory.Exists(info.GetTargetFolder()))
            {
                Directory.CreateDirectory(info.GetTargetFolder());
                Console.WriteLine($"    Creating folder '{info.GetTargetFolder()}'...");
            }

            using (StreamReader sr = new StreamReader($"{info.GetTemplateFilePath()}Template.txt"))
            {
                using (StreamWriter sw = new StreamWriter(info.GetTargetFilePath()))
                {

                    while ((line = sr.ReadLine()) != null)
                    {
                        //Replace Blocks
                        foreach (var bloc in ConfigInfo.BlocksToReplace)
                        {
                            line = line.Replace(bloc.Key, bloc.Value);
                        }

                        //Demandes à la volée
                        if (line.Contains(ConfigInfo.BlocksOnTheFly["$BLOCK_otherInteractors$"]))
                        {
                            line = AddOtherInteractorsForExecutorOnTheFly();
                        }


                        //Configure variable lines
                        if (line.Contains(ConfigInfo.PlaceHolders["$dtoToEntityLine$"]))
                        {
                            line = UtilityLogic.BuildDtoToEntityConvertion();
                        }
                        if (line.Contains(ConfigInfo.PlaceHolders["$jsonDefaultFields$"]))
                        {
                            line = UtilityLogic.BuildJsonDefaultFields();
                        }

                        //Replace placeHolders
                        foreach (var placeHolder in ConfigInfo.PlaceHolders)
                        {
                            line = line.Replace(placeHolder.Key, placeHolder.Value);
                        }

                        sw.WriteLine(line);
                        //Console.WriteLine(line);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"File {info.GetTargetFilePath()} created");
            Console.ResetColor();
        }


        private static string AddOtherInteractorsForExecutorOnTheFly()
        {
            StringBuilder sb = new StringBuilder();
            string otherInteractor = "123";
            while (otherInteractor != "")
            {
                Console.WriteLine($"==> Enter name of other Interactor used by {ConfigInfo.EntityName}Executor if needed : (press 'Enter' to skip)");
                otherInteractor = Console.ReadLine();
                if (!string.IsNullOrEmpty(otherInteractor))
                    sb.AppendLine($"\t\t{otherInteractor}Interactor {otherInteractor.ToLower()}Interactor = new {otherInteractor}Interactor();");
            }
            return sb.ToString();
        }
    }
}
