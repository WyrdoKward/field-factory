using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldFactory.Utility.CreatePipeline
{
    public static class ConfigInfo
    {
        #region Public Fields
        public static string EntityName;
        public static Dictionary<string, string> EntityFields = new Dictionary<string, string>();
        public static Dictionary<string, string> DiscriminatingFields = new Dictionary<string, string>();
        public static Dictionary<string, string> PlaceHolders = new Dictionary<string, string>() {
            { "$queryFlatParams$", "$queryFlatParams$" },
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
            { "$insertValues$", "$insertValues$" },
            { "$discriminatingFieldsComparison$", "$discriminatingFieldsComparison$" },
            { "$discriminatingFieldsComparisonFromDto$", "$discriminatingFieldsComparisonFromDto$" },
            { "$discriminatingFields$", "$discriminatingFields$" },
            { "$discriminatingFlatParams$", "$discriminatingFlatParams$" },
            { "$jsonDefaultFields$", "$jsonDefaultFields$" },
            { "$StaticRessourcesNamespace$", "" },
        };
        public static Dictionary<string, string> BlocksOnTheFly = new Dictionary<string, string>()
        {
            {"$BLOCK_otherInteractors$", "$BLOCK_otherInteractors$"},
        };

        public static Dictionary<string, string> BlocksToReplace = new Dictionary<string, string>()
        {
            {"$BLOCK_publicFields$", ""},
            {"$BLOCK_ConstructorParams$", ""},
            {"$BLOCK_fieldsAssignation$", ""},
            {"$BLOCK_constructorWithDtoFieldsAssignation$", ""},
            {"$BLOCK_dtoMethods$", ""},
            {"$BLOCK_discriminatingPublicFields$", ""},
            {"$BLOCK_discriminatingFieldsAssignation$", ""}
        };
        #endregion

        public static void Clean()
        {
            EntityName = string.Empty;
            EntityFields = new Dictionary<string, string>();
            DiscriminatingFields = new Dictionary<string, string>();
        }
        public static bool IsJson
        {
            get
            {
                return EntityFields.ContainsKey("json");
            }
        }
        public static void PrintConfigInfos()
        {
            Console.WriteLine($"Entity to Create : {EntityName}");
            Console.WriteLine($"IsJson : {IsJson}");
            Console.WriteLine("");
            Console.WriteLine("Fields :");
            foreach (var f in EntityFields)
            {
                Console.WriteLine($"    {f.Key} : {f.Value}");
            }
            Console.WriteLine("");
            Console.WriteLine("Discriminating Fields :");
            foreach (var f in DiscriminatingFields)
            {
                Console.WriteLine($"    {f.Key} : {f.Value}");
            }

        }
    }

}