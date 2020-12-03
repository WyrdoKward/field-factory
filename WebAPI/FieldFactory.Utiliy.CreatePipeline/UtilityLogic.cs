using FieldFactory.Core.Utils.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FieldFactory.Utility.CreatePipeline
{
    public static class UtilityLogic
    {
        static Dictionary<string, string> SQLiteTypeMapping = new Dictionary<string, string>()
        {
            {"text", "string" },
            {"integer", "int" },
            {"datetime", "DateTime" },
        };

        public static Dictionary<string, string> EntityFields = new Dictionary<string, string>();
        public static Dictionary<string, string> DiscriminatingFields = new Dictionary<string, string>();

        public static void ParseSQLiteParams(string sql)
        {
            foreach (var col in sql.Split(','))
            {
                var colName = col.Split('"')[1].Trim();
                var cSharpType = SQLiteTypeMapping[col.Split('"')[2].Trim()];
                EntityFields.Add(colName, cSharpType);
            }
        }
        public static string ParseSQLiteDiscriminatingParams(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                var firstParam = EntityFields.FirstOrDefault();
                if (!EntityFields.Keys.Contains(firstParam.Key))
                    return firstParam.Key;

                DiscriminatingFields.Add(firstParam.Key, firstParam.Value);
            }
            else
            {
                foreach (var col in input.Split(','))
                {
                    var colName = col.Trim();
                    if (!EntityFields.Keys.Contains(colName))
                        return colName;

                    var cSharpType = EntityFields[colName];
                    DiscriminatingFields.Add(colName, cSharpType);
                }
            }
            return "";
        }

        public static string BuildMethodParamFirstOnly()
        {
            var firstParam = EntityFields.FirstOrDefault();
            var firstParamDict = new Dictionary<string, string>() { { firstParam.Key, firstParam.Value } };
            return BuildConstructorDtoParams(firstParamDict);
        }

        internal static string GetFirstParamName()
        {
            return EntityFields.FirstOrDefault().Key;
        }

        public static string BuildConstructorDtoParams(Dictionary<string, string> cols)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in cols)
            {
                sb.Append($"{item.Value} {item.Key}, ");
            }

            return sb.ToString().Substring(0, sb.ToString().Length - 2);
        }
        public static string BuildPublicFieldBlock(Dictionary<string, string> cols)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in cols)
            {
                sb.Append("\t\t");
                sb.Append(BuildPublicFieldLine(item.Key, item.Value));
                sb.Append("\r\n");
            }

            return sb.ToString().Substring(2);
        }

        public static string BuildPublicFieldLine(string name, string @type)
        {
            name = name.FirstCharToUpper();
            return $"public {type} {name} {{ get; set; }}";
        }

        public static string BuildFieldAssignationBlock(Dictionary<string, string> cols, bool validateParams, bool fromDto)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in cols)
            {
                sb.Append("\t\t\t");
                sb.Append(BuildFieldAssignationLine(item.Key, validateParams, fromDto));
                sb.Append("\r\n");
            }

            return sb.ToString().Substring(3);
        }

        public static string BuildFieldAssignationLine(string name, bool validateParams, bool fromDto)
        {
            string dtoParam = fromDto ? $"dto.{name.FirstCharToUpper()}" : name;
            string validation = validateParams ? $" ?? throw new ArgumentOutOfRangeException(nameof({dtoParam}))" : "";
            return $"{name.FirstCharToUpper()} = {dtoParam}{validation};";
        }

        public static string BuildEntityConvertToDTOMethods()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t\tpublic $entityName$DTO ConvertToDTO()");
            sb.AppendLine("\t\t{");
            sb.AppendLine($"\t\t\treturn new $entityName$DTO({BuildFlatParams(EntityFields, true)});");
            sb.AppendLine("\t\t}");
            return sb.ToString();
        }

        /// <summary>
        /// "param1, param2"
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="firstCharToUpper"></param>
        /// <returns></returns>
        public static string BuildFlatParams(Dictionary<string, string> fields, bool firstCharToUpper, string prefix = "")
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in fields)
            {
                string p = firstCharToUpper ? item.Key.FirstCharToUpper() : item.Key;
                sb.Append($"{prefix}{p}, ");
            }
            return sb.ToString().Substring(0, sb.ToString().Length - 2);
        }

        /// <summary>
        /// Pour méthode ConvertIntoDto du $Provider
        /// </summary>
        public static string BuildFlatParamsFromRaw()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in EntityFields)
            {
                switch (item.Value)
                {
                    case "int":
                        sb.Append($"int.Parse(raw$entityName$.Value[\"{item.Key}\"]), ");
                        break;
                    case "DateTime":
                        sb.Append($"DateTime.Parse(raw$entityName$.Value[\"{item.Key}\"]), ");
                        break;
                    default:
                        sb.Append($"raw$entityName$.Value[\"{item.Key}\"], ");
                        break;
                }
            }
            return sb.ToString().Substring(0, sb.ToString().Length - 2);
        }

        public static string BuildDtoToEntityConvertion(bool isJsonDto)
        {
            StringBuilder sb = new StringBuilder();
            if (isJsonDto)
            {
                sb.AppendLine("$entityNameLowerCase$Id");
            }
            else
            {
                sb.AppendLine("\t\t\tvar $entityNameLowerCase$ = new $entityName$($entityNameLowerCase$Dto);");
            }
            return sb.ToString();

        }

        public static string BuildUpdateSetOrEqualValues(Dictionary<string, string> cols, bool paramFromDtoObject)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in cols)
            {
                sb.Append(BuildSqlEqualComparison(item, paramFromDtoObject));
            }
            return sb.ToString().Substring(0, sb.ToString().Length - 2);
        }



        public static string BuildSqlEqualComparison(KeyValuePair<string, string> item, bool paramFromDtoObject)
        {
            string dtoParam = paramFromDtoObject ? $"dto.{item.Key.FirstCharToUpper()}" : item.Key;
            switch (item.Value)
            {
                case "int":
                    return $"{item.Key} = {{{dtoParam}}}, ";
                default:
                    return $"{item.Key} = '{{{dtoParam}}}', ";
            }
        }

        public static string BuildInsertValues()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in EntityFields)
            {
                switch (item.Value)
                {
                    case "int":
                        sb.Append($"{{dto.{item.Key.FirstCharToUpper()}}}, ");
                        break;
                    default:
                        sb.Append($"'{{dto.{item.Key.FirstCharToUpper()}}}', ");
                        break;
                }
            }
            return sb.ToString().Substring(0, sb.ToString().Length - 2);
        }

        public static string BuildInteractorParam(bool isJsonDto)
        {
            if (isJsonDto)
            {
                return "$entityNameLowerCase$Id";
            }
            else
            {
                return "$discriminatingFields$";
            }

        }
        public static string BuildInteractorFlatParam(bool isJsonDto)
        {
            if (isJsonDto)
            {
                return "$entityNameLowerCase$Id";
            }
            else
            {
                return "$discriminatingFlatParams$";
            }

        }

        public static string BuildJsonDefaultFields(bool isJsonDto)
        {
            StringBuilder sb = new StringBuilder();
            if (isJsonDto)
            {
                sb.AppendLine("\t\tpublic int Id;");
                sb.AppendLine("\t\tpublic string Title;");
                sb.AppendLine("\t\tpublic string Description;");
            }
            return sb.ToString();
        }

    }
}
