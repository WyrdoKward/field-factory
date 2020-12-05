using Microsoft.SqlServer.Management.SqlParser.Parser;

namespace FieldFactory.Utility.CreatePipeline
{
    public class TokenInfo
    {
        public int Start;
        public int End;
        public bool IsPairMatch;
        public bool IsExecAutoParamHelp;
        public string Sql;
        public Tokens Token;
        public TokenInfo() { }
    }

}