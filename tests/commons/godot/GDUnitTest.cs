using GdUnit4;
using Godot;
using SpaceMiner.src.code.components.commons.godot.position_locator;
using System.Collections.Generic;

namespace SpaceMinerTests.tests.commons.godot
{
    [TestSuite]
    class GDUnitTest
    {
        [Before]
        public void InitializeTest()
        {

        }

        [TestCase]
        public void GetChildNodeAtPositionTest()
        {
            Assertions.AssertThat("This").Equals("This");
        }

        [After]
        public void CleanupTest()
        {

        }
    }
}
