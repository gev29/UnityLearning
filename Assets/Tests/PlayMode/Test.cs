using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Test
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase(6)]
    [TestCase(8)]
    [TestCase(0)]
    public void TestSimplePasses(int param)
    {
        int expected = 5;


        //should be calculated
        int actual = param;

        Assert.Less(expected, actual);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestWithEnumeratorPasses()
    {
        int expected = 5;

        SceneManager.LoadScene(0);

        yield return new WaitForSeconds(5f);


        //should be calculated
        int actual = 8;

        Assert.Less(expected, actual);
    }
}
