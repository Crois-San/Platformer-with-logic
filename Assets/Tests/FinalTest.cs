using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class FinalTest
{
    [UnityTest]
    public IEnumerator CompleteLevelButHasErrorTest()
    {
        GameObject levelCompleteGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/LevelCompleteTriggerZone"));
        GameObject characterGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Character"));
        OnLevelCompleteAction levelCompleteAction = levelCompleteGameObject.GetComponent<OnLevelCompleteAction>();
        Character character = characterGameObject.GetComponent<Character>();

        var currentScene = SceneManager.GetActiveScene();
        Debug.Log(currentScene.name);

        for (int i = 0; i < 10; i++)
        {
            character.transform.position = levelCompleteAction.transform.position;
            yield return new WaitForSeconds(0.1f);
        }
        
        var newScene = SceneManager.GetActiveScene();
        
        Assert.AreNotEqual(currentScene, newScene);
    }
    
    [UnityTest]
    public IEnumerator NoCompleteLevelTest()
    {
        GameObject levelCompleteGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/LevelCompleteTriggerZone"));
        GameObject characterGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Character"));
        OnLevelCompleteAction levelCompleteAction = levelCompleteGameObject.GetComponent<OnLevelCompleteAction>();
        Character character = characterGameObject.GetComponent<Character>();

        var currentScene = SceneManager.GetActiveScene();

        for (int i = 0; i < 9; i++)
        {
            character.transform.position = levelCompleteAction.transform.position;
            yield return new WaitForSeconds(0.1f);
        }
        
        var newScene = SceneManager.GetActiveScene();
        
        Assert.AreEqual(currentScene, newScene);
    }
}

