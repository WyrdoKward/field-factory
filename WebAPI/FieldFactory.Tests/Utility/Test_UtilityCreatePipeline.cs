using FieldFactory.Core.Entities.Map.Event;
using FieldFactory.Utility.CreatePipeline;
using NUnit.Framework;
using System.Collections.Generic;

namespace FieldFactory.Tests.Utility
{
    public class Test_UtilityCreatePipeline
    {
        Choice c = new Choice();

        [SetUp]
        public void Setup()
        {
            c.Outcomes = new Outcome[3]
            {
                new Outcome(){NextStepId = 1, Weight = 1 },
                new Outcome(){NextStepId = 2, Weight = 1 },
                new Outcome(){NextStepId = 3, Weight = 2 },
            };
        }

        [Test]
        [TestCase("\"idPlayer\" text,\"intColumn\" integer, \"dateColumn\" datetime")]
        public void Test_ParseSQLiteParams(string cols)
        {
            Dictionary<string, string> expected = new Dictionary<string, string>()
            {
                {"idPlayer", "string" },
                {"intColumn", "int" },
                {"dateColumn", "DateTime" },
            };

            var actual = UtilityLogic.ParseSQLiteParams(cols);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("\"idPlayer\" text,\"intColumn\" integer, \"dateColumn\" datetime")]
        public void Test_BuildConstructorDtoParams(string cols)
        {
            Dictionary<string, string> input = new Dictionary<string, string>()
            {
                {"idPlayer", "string" },
                {"intColumn", "int" },
                {"dateColumn", "DateTime" },
            };

            string expected = "string idPlayer, int intColumn, DateTime dateColumn";

            var actual = UtilityLogic.BuildConstructorDtoParams(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("idPlayer", "string", "public string IdPlayer { get; set; }")]
        [TestCase("intColumn", "int", "public int IntColumn { get; set; }")]
        [TestCase("dateColumn", "DateTime", "public DateTime DateColumn { get; set; }")]
        public void Test_BuildPublicFieldLine(string name, string @type, string expected)
        {
            var actual = UtilityLogic.BuildPublicFieldLine(name, @type);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        [TestCase("idPlayer", false, "IdPlayer = idPlayer;")]
        [TestCase("intColumn", false,  "IntColumn = intColumn;")]
        [TestCase("dateColumn", false, "DateColumn = dateColumn;")]
        [TestCase("idPlayer", true, "IdPlayer = dto.IdPlayer;")]
        [TestCase("intColumn", true, "IntColumn = dto.IntColumn;")]
        [TestCase("dateColumn", true, "DateColumn = dto.DateColumn;")]
        public void Test_BuildFieldAssignationLine(string name, bool fromDto, string expected)
        {
            var actual = UtilityLogic.BuildFieldAssignationLine(name, fromDto);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("IdPlayer, IntColumn, DateColumn")]
        public void Test_BuildFlatParams(string expected)
        {
            Dictionary<string, string> input = new Dictionary<string, string>()
            {
                {"idPlayer", "string" },
                {"intColumn", "int" },
                {"dateColumn", "DateTime" },
            };

            var actual = UtilityLogic.BuildFlatParams(input);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        [TestCase("raw$entityName$.Value[\"idPlayer\"], raw$entityName$.Value[\"intColumn\"], raw$entityName$.Value[\"dateColumn\"]")]
        public void Test_BuildFlatParamsFromRaw(string expected)
        {
            Dictionary<string, string> input = new Dictionary<string, string>()
            {
                {"idPlayer", "string" },
                {"intColumn", "int" },
                {"dateColumn", "DateTime" },
            };

            var actual = UtilityLogic.BuildFlatParamsFromRaw(input);

            Assert.AreEqual(expected, actual);
        }
    }
}