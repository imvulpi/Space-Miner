using GdUnit4;
using Godot;
using System.Threading;

[TestSuite]
public class MenuManagerTest
{
    [TestCase]
    public void MenuTest()
    {
        var runner = ISceneRunner.Load("res://src/code/tests/gdunit/MenuManagerTest/MenuManagerTest.tscn", true);
        Thread.Sleep(2000);
    }
}
