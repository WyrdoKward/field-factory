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
            ConfigInfo.EntityFields = new Dictionary<string, string>()
            {
                {"idPlayer", "string" },
                {"intColumn", "int" },
                {"dateColumn", "DateTime" },
            };
        }

       /* [Test]
        [TestCase("\"idPlayer\" text,\"intColumn\" integer, \"dateColumn\" datetime")]
        public void Test_ParseSQLiteParams(string cols)
        {
            Dictionary<string, string> expected = new Dictionary<string, string>(UtilityLogic.EntityFields);
            ConfigInfo.EntityFields.Clear();
            UtilityLogic.ParseSQLiteParams(cols);
            var actual = ConfigInfo.EntityFields;

            Assert.AreEqual(expected, actual);
        }*/

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
        [TestCase("idPlayer", false, false, "IdPlayer = idPlayer;")]
        [TestCase("intColumn", false, false,  "IntColumn = intColumn;")]
        [TestCase("dateColumn", false, false, "DateColumn = dateColumn;")]
        [TestCase("idPlayer", false, true, "IdPlayer = dto.IdPlayer;")]
        [TestCase("intColumn", false, true, "IntColumn = dto.IntColumn;")]
        [TestCase("dateColumn", false, true, "DateColumn = dto.DateColumn;")]
        [TestCase("idPlayer", true, false, "IdPlayer = idPlayer ?? throw new ArgumentNullException(nameof(idPlayer));")]
        [TestCase("intColumn", true, false, "IntColumn = intColumn ?? throw new ArgumentNullException(nameof(intColumn));")]
        [TestCase("dateColumn", true, false, "DateColumn = dateColumn ?? throw new ArgumentNullException(nameof(dateColumn));")]
        [TestCase("idPlayer", true, true, "IdPlayer = dto.IdPlayer ?? throw new ArgumentNullException(nameof(dto.IdPlayer));")]
        [TestCase("intColumn", true, true, "IntColumn = dto.IntColumn ?? throw new ArgumentNullException(nameof(dto.IntColumn));")]
        [TestCase("dateColumn", true, true, "DateColumn = dto.DateColumn ?? throw new ArgumentNullException(nameof(dto.DateColumn));")]
        public void Test_BuildFieldAssignationLine(string name, bool validateParams, bool fromDto, string expected)
        {
            var actual = UtilityLogic.BuildFieldAssignationLine(name, validateParams, fromDto);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(true, "IdPlayer, IntColumn, DateColumn")]
        [TestCase(false, "idPlayer, intColumn, dateColumn")]
        public void Test_BuildFlatParams(bool firstCharToUpper, string expected)
        {
            Dictionary<string, string> input = new Dictionary<string, string>()
            {
                {"idPlayer", "string" },
                {"intColumn", "int" },
                {"dateColumn", "DateTime" },
            };

            var actual = UtilityLogic.BuildFlatParams(input, firstCharToUpper);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        [TestCase("raw$entityName$.Value[\"idPlayer\"], int.Parse(raw$entityName$.Value[\"intColumn\"]), DateTime.Parse(raw$entityName$.Value[\"dateColumn\"])")]
        public void Test_BuildFlatParamsFromRaw(string expected)
        {
            var actual = UtilityLogic.BuildFlatParamsFromRaw();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(true, "aaa = '{dto.Aaa}', bbb = {dto.Bbb}")]
        [TestCase(false, "aaa = '{aaa}', bbb = {bbb}")]
        public void Test_BuildUpdateSetValues(bool fromdto, string expected)
        {
            UtilityLogic.CurrentDictionary = new Dictionary<string, string>()
            {
                {"aaa", "string" },
                {"bbb", "int" },
            };

            var actual = UtilityLogic.BuildUpdateSetOrEqualValues(fromdto);

            Assert.AreEqual(expected, actual);
        }
    }
}