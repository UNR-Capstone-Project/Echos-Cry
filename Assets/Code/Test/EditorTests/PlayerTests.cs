using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerTests
{
    [UnityTest]
    public IEnumerator PlayerLoadedInTownScene()
    {
        EditorSceneManager.OpenScene("Assets/Level/Scenes/MainScenes/" + EchosCry.Scene.Name.Town + ".unity");
        yield return null;
        yield return null;
        var Player = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(Player);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator calculate_xpWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
