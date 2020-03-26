using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private List<Scene> SceneList = new List<Scene>();
    public bool[] LevelsState { get; set; }

    public List<Scene> GetSceneList => SceneList;
    public List<Scene> SetSceneList { set => SceneList = value; }
    //Инициализация списка уровней
    public void GetLevelsList()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scene = SceneManager.GetSceneByBuildIndex(i);
            if(scene.name == "MainMenu") continue;
            SceneList.Add(scene);
        }
        LevelsState = new bool[SceneList.Count];
        for (int i = 0; i < LevelsState.Length; i++)
        {
            LevelsState[i] = false;
        }

        LevelsState[0] = true;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    // Start is called before the first frame update
    void Start()
    {
        //GetLevelsList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
