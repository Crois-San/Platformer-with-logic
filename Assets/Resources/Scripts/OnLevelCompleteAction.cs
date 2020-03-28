using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLevelCompleteAction : MonoBehaviour
{
    private ISoundSystem ssLevelComplete;
    private float levelTransitionTimer = 1f;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered");
        ssLevelComplete.MakeSound();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        levelTransitionTimer -= Time.deltaTime;
        if (levelTransitionTimer <= 0)
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        levelTransitionTimer = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        ssLevelComplete = new SoundSystemDefault(gameObject, Sounds.VictoryScore, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
