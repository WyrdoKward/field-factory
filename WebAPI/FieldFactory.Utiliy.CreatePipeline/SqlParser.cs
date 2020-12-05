using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Management.SqlParser.Parser;
using FieldFactory.Core.Utils.Extension;
using System.Text;
using System.IO;

namespace FieldFactory.Utility.CreatePipeline
{
    public static class SqlParser
    {
        public static string _sql;
        public static IEnumerable<TokenInfo> _tokens;
        public static void ExtractInfosFromTokens()
        {
            bool tableFound = false;
            string lastfield = string.Empty;
            foreach (var t in _tokens)
            {
                if (t.Token == Tokens.TOKEN_ID)
                {
                    //le premier c'est la table
                    if (!tableFound)
                    {
                        ConfigInfo.EntityName = CleanToken(t.Sql).ToLower().FirstCharToUpper();
                        tableFound = true;
                    }
                    else
                    {
                        //les TOKEN_ID fonctionnent par paire, le premier est le champ, le 2e est le type
                        if (string.IsNullOrEmpty(lastfield))
                        {
                            lastfield = CleanToken(t.Sql);
                            ConfigInfo.EntityFields.Add(lastfield, string.Empty);
                        }
                        else
                        {
                            ConfigInfo.EntityFields[lastfield] = t.Sql;
                            lastfield = string.Empty;
                        }
                    }
                }
                else if (t.Token == Tokens.TOKEN_PRIMARY)
                {
                    break;
                }

            }

        }

        private static string CleanToken(string sql)
        {
            return sql.Replace('"', ' ').Replace('\'', ' ').Trim();
        }
        public static void ReadSqlFile(string file)
        {
            StringBuilder CreateStatementSb = new StringBuilder();
            using (StreamReader sr = new StreamReader(file))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("CREATE TABLE"))
                        CreateStatementSb.Append(line);

                    if (line.Contains(");"))
                        break;
                }
            }
            _sql = CreateStatementSb.ToString();
        }

        public static void ParseSql()
        {
            ParseOptions parseOptions = new ParseOptions();
            Scanner scanner = new Scanner(parseOptions);

            int state = 0,
                start,
                end,
                lastTokenEnd = -1,
                token;

            bool isPairMatch, isExecAutoParamHelp;

            List<TokenInfo> tokens = new List<TokenInfo>();

            scanner.SetSource(_sql, 0);

            while ((token = scanner.GetNext(ref state, out start, out end, out isPairMatch, out isExecAutoParamHelp)) != (int)Tokens.EOF)
            {
                TokenInfo tokenInfo =
                    new TokenInfo()
                    {
                        Start = start,
                        End = end,
                        IsPairMatch = isPairMatch,
                        IsExecAutoParamHelp = isExecAutoParamHelp,
                        Sql = _sql.Substring(start, end - start + 1),
                        Token = (Tokens)token,
                    };

                tokens.Add(tokenInfo);

                lastTokenEnd = end;
            }

            _tokens = tokens;
        }



    }
}
