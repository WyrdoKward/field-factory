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
                sb.Append(BuildPublicFieldLine(item.Key, item.Value));
            }

            return sb.ToString();
        }

        public static string BuildPublicFieldLine(string name, string @type)
        {
            name = name.FirstCharToUpper();
            return $"public {type} {name} {{ get; set; }}";
        }

        public static string BuildFieldAssignationBlock(Dictionary<string, string> entityFields)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in entityFields)
            {
                sb.Append(BuildPublicFieldLine(item.Key, item.Value));
            }

            return sb.ToString();
        }

        public static string BuildFieldAssignationLine(string name)
        {
            return $"{name.FirstCharToUpper()} = {name};";
        }
    }
}
