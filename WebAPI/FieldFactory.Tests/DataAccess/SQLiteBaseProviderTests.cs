using System;
using System.Collections.Generic;
using System.Text;
using FieldFactory.DataAccess.Providers;
using FieldFactory.DataAccess.SQLite;
using FieldFactory.Tests.DataAccess.Mock;
using NUnit.Framework;

namespace FieldFactory.Tests.DataAccess
{
    public class SQLiteBaseProviderTests
    {
        [Test]
        public void ReadColumnByName()
        {
            TestProvider provider = new TestProvider();
            string query = "SELECT * FROM _TestTable";

            var res = provider.ReadColumnsMock(query);

            Assert.AreEqual("1", res[0]["id"]);
            Assert.AreEqual("2", res[0]["col1"]);
            Assert.AreEqual("truc", res[0]["col2"]);
        }

        [Test]
        public void ReadLocationColumns()
        {            
            LocationProvider locatioProvider = new LocationProvider();
            var loc = locatioProvider.Get("dummyLocation");
            string expectedJson = "{\"Id\":1,\"Title\":\"Dummy Location\",\"Description\":\"This is a place where things can happen.\",\"LocationType\":\"mine\",\"Verbs\":[\"Explorer\",\"Méditer\"],\"RandomEvents\":[\"dummyLocationEvent\"]}";
            Assert.AreEqual("dummyLocation", loc.Id);
            Assert.AreEqual(expectedJson, loc.Json);
        }

        [Test]
        public void ReadExploreColumns()
        {
            ExploreProvider exploreProvider = new ExploreProvider();
            var explo = exploreProvider.Get("nono", "dummyLocation");

            Assert.AreEqual("nono", explo.IdPlayer);
            Assert.AreEqual("Gustav", explo.IdFollower);
            Assert.AreEqual("dummyLocation", explo.IdLocation);
            Assert.AreEqual("dummyLocationEvent", explo.IdEvent);
            Assert.AreEqual(0, explo.IdStep);
            Assert.AreEqual(1, explo.IdChoice);
        }

        [Test]
        public void ReadEventColumns()
        {
            EventProvider eventProvider = new EventProvider();
            var evt = eventProvider.Get("dummyLocationEvent");
            Assert.AreEqual("dummyLocationEvent", evt.Id);
        }


        [Test]
        public void ReadPlayerColumns()
        {
            PlayerProvider PlayerProvider = new PlayerProvider();
            var player = PlayerProvider.GetById("nono");

            Assert.AreEqual("nono", player.IdPlayer);
            Assert.AreEqual("no@no.com", player.Email);
            Assert.AreEqual("pwd", player.Hashpwd);
            Assert.AreEqual("O7P06l/u0UukSubJhfFYsA==", player.Token);
        }
    }
}
