using NUnit.Framework;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerTests
{
    //Check if the player is loaded into the Town scene
    [UnityTest]
    public IEnumerator PlayerLoadedInTownScene()
    {
        SceneManager.LoadScene(EchosCry.Scene.Name.Town, LoadSceneMode.Single);
        //Scene does not finish loading until next frame, so skip frame
        yield return null;
        var Player = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(Player);
    }
}
