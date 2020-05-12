using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutomatedBuild
{
    //private Scene[] scenes;
    static void Build()
    {
        /*scenes[0] = SceneManager.GetSceneByName("MainMenu");
        for (int i = 1; Application.CanStreamedLevelBeLoaded("Level0" + i); i++)
        {
            scenes[i] = SceneManager.GetSceneByName("Level0" + i);
        }*/
        try
        {
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes,"C:/Program Files/Gates/Gates.exe" , BuildTarget.StandaloneWindows, BuildOptions.None);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
