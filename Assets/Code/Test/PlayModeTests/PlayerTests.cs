using NUnit.Framework;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerTests
{
    GameObject Player;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        SceneManager.LoadScene(EchosCry.Scene.Name.Town, LoadSceneMode.Single);
        //Scene does not finish loading until next frame, also wait for other entities of scene to load
        yield return new WaitForSeconds(1);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    [Test]
    public void Check_That_Player_GameObject_Is_Not_Null()
    {
        Assert.That(Player, Is.Not.Null);
    }

    [Test]
    public void Check_That_Player_GameObject_Has_Player_Component()
    {
        Player component = Player.GetComponent<Player>();
        Assert.That(component, Is.Not.Null);
    }

    [UnityTest]
    public IEnumerator Player_Can_Move_Left_Across_10_Frames()
    {
        Player component = Player.GetComponent<Player>();
        Vector3 position = Player.transform.position;
        for (int i = 0; i < 10; i++)
        {
            component.Movement.Move(new Vector2(-1, 0));
            yield return null;
        }
        Assert.That(Player.transform.position.x, Is.LessThan(position.x));
    }

    [UnityTest]
    public IEnumerator Player_Can_Move_Right_Across_10_Frames()
    {
        Player component = Player.GetComponent<Player>();
        Vector3 position = Player.transform.position;
        for (int i = 0; i < 10; i++)
        {
            component.Movement.Move(new Vector2(1, 0));
            yield return null;
        }
        Assert.That(Player.transform.position.x, Is.GreaterThan(position.x));
    }

    [UnityTest]
    public IEnumerator Player_Can_Move_Up_Across_10_Frames()
    {
        Player component = Player.GetComponent<Player>();
        Vector3 position = Player.transform.position;
        for (int i = 0; i < 10; i++)
        {
            component.Movement.Move(new Vector2(0, 1));
            yield return null;
        }
        Assert.That(Player.transform.position.z, Is.GreaterThan(position.z));
    }

    [UnityTest]
    public IEnumerator Player_Can_Move_Down_Across_10_Frames()
    {
        Player component = Player.GetComponent<Player>();
        Vector3 position = Player.transform.position;
        for (int i = 0; i < 10; i++)
        {
            component.Movement.Move(new Vector2(0, -1));
            yield return null;
        }
        Assert.That(Player.transform.position.z, Is.LessThan(position.z));
    }

    [UnityTest]
    public IEnumerator Player_Can_Dash_In_Up_Direction()
    {
        Player component = Player.GetComponent<Player>();
        Vector3 position = Player.transform.position;
        component.RB.linearVelocity = new Vector3(0, 0, 1);
        component.Movement.Dash();
        yield return new WaitForSeconds(1);
        Assert.That(component.transform.position.z, Is.GreaterThan(position.z));
    }

    //TODO:
    //Test player movement and dash functionality (without state machines)
    //Test state machine functionality for each state
    //Test spam prevention system functionality
    //Test if player can be damaged
    //Some other test ideas

    //Other tests outside of player:
    //Testing if managers exist (TickManager, TempoConductor, etc)
    //Testing wave system functionality
}
