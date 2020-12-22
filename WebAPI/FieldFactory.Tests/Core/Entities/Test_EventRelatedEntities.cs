using FieldFactory.Core.Entities.Map.Event;
using NUnit.Framework;

namespace FieldFactory.Tests.Core.Entities
{
    public class Test_EventRelatedEntities
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
        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 3)]
        public void Returns_valid_nextStepId(int rndIndex, int expectedNextstepId)
        {
            int actual = c.ChooseRandomNextStep(rndIndex);
            Assert.AreEqual(expectedNextstepId, actual);
        }
    }
}