using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class MusicTests
{
    MusicManager musicInstance;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        SceneManager.LoadScene(EchosCry.Scene.Name.Town, LoadSceneMode.Single);
        yield return new WaitForSeconds(1);
        musicInstance = MusicManager.Instance;
    }

    // A Test behaves as an ordinary method
    [Test]
    public void MusicTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MusicTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
