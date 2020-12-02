using FieldFactory.Core.Utils.Extension;
using System.Collections.Generic;
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

        public static Dictionary<string, string> ParseSQLiteParams(string sql)
        {
            var res = new Dictionary<string, string>();
            foreach (var col in sql.Split(','))
            {
                var colName = col.Split('"')[1].Trim();
                var cSharpType = SQLiteTypeMapping[col.Split('"')[2].Trim()];
                res.Add(colName, cSharpType);
            }

            return res;
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
        public static string BuildPublicFieldBlock(Dictionary<string, string> entityFields)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in entityFields)
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

        public static string BuildFieldAssignationBlock(Dictionary<string, string> entityFields, bool fromDto)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in entityFields)
            {
                sb.Append("\t\t\t");
                sb.Append(BuildFieldAssignationLine(item.Key, fromDto));
                sb.Append("\r\n");

            }

            return sb.ToString().Substring(3);
        }

        public static string BuildFieldAssignationLine(string name, bool fromDto)
        {
            string dtoParam = fromDto ? $"dto.{name.FirstCharToUpper()}" : name;
            return $"{name.FirstCharToUpper()} = {dtoParam};";
        }

        public static string BuildEntityConvertToDTOMethods(Dictionary<string, string> entityFields)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t\tpublic $entityName$DTO ConvertToDTO()");
            sb.AppendLine("\t\t{");
            sb.AppendLine($"\t\t\treturn new $entityName$DTO({BuildFlatParams(entityFields)});");
            sb.AppendLine("\t\t}");
            return sb.ToString();
        }

        public static string BuildFlatParams(Dictionary<string, string> entityFields)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in entityFields)
            {
                sb.Append($"{item.Key.FirstCharToUpper()}, ");
            }
            return sb.ToString().Substring(0, sb.ToString().Length - 2);
        }
        public static string BuildDtoToEntityConvertion(bool isJsonDto)
        {
            StringBuilder sb = new StringBuilder();
            if (isJsonDto)
            {
                sb.AppendLine("\t\t\tvar $entityNameLowerCase$ = JsonConvert.DeserializeObject<$entityName$>($entityNameLowerCase$Dto.Json);");
            }
            else
            {
                sb.AppendLine("\t\t\tvar $entityNameLowerCase$ = new $entityName$($entityNameLowerCase$Dto);");
            }
            return sb.ToString();

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
