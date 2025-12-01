using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum TimeOfDay { Morning, Chemistry, History, Lunch, PE, AfterSchool, FirstDate}

[System.Serializable]
class ScenesArr
{
  public Scene[] morning;     // 7 AM
  public Scene[] chemistry;   // 8 AM
  public Scene[] history;     // 10 AM
  public Scene[] lunch;       // 12 PM
  public Scene[] PE;          // 1 PM
  public Scene[] afterSchool; // 3 PM
  public Scene[] firstDate;   // 5 PM
}

[System.Serializable]
public class Scene
{
  // Required fields
  public string text;               // Italicized text if no name is null

  // Optional Fields
  public string name;               // name of character speaking
  public Option[] options;          // optional field for choices
  public int skip;                  // optional field to skip to a specific scene index
  public NextSceneArr nextSceneArr; // Loads new Scene array from json (for scene transitions)
  public string voiceOver;          // Path to voice over audio file
}
[System.Serializable]
public class NextSceneArr
{
  public string sceneArr;  // which scene array to load (redScenes, greenScenes, etc)
  public string timeslot;       // which day to load from that scene array
  public int startIndex;  // index to start at in that scene array
}
[System.Serializable]
public class Option
{
  public string text;
  public int points;
  public int newIndex;              // jump to new scene index if same timeslot
  public NextSceneArr nextSceneArr; // jump to new scene array if different timeslot
}


public class LoadScene : MonoBehaviour
{
  // Public Variables, used by other scripts
  public Scene currentScene; // get this var when displaying scene

  // Private Variables
  private Scene[] scenes;
  private int sceneIdx = 0;

  
  void Start()
  {
    // Starts by loading 0th scene from defaultScenes.json Morning array
    scenes = LoadFromJSON("defaultScenes", (TimeOfDay)0);
    if (scenes == null || scenes.Length == 0)
    {
      Debug.LogError("No scenes loaded from json");
      return;
    }
  }

  // Public Function used by other scripts to load next scene
  // choice = -1 means no choice made, just go to next scene
  // Other scripts then get currentScene variable to display scene after calling this function
  public void LoadNextScene(int choice)
  {
    // Check if loading bc of a choice, if not, just go to next scene
    if (currentScene.options != null && choice >= 0 && currentScene.options.Length > 0 && currentScene.options.Length > choice)
    {
      // Load based on choice made
      Option selected;
      switch (choice)
      {
        case 0:
          selected = currentScene.options[0];
          break;
        case 1:
          selected = currentScene.options[1];
          break;
        case 2:
          selected = currentScene.options[2];
          break;
        case 3:
          selected = currentScene.options[3];
          break;
        default:
          Debug.LogError("Invalid choice number: " + choice);
          return;
      }

      // Check if next scene is from different scene array
      if (!string.IsNullOrEmpty(selected.nextSceneArr.sceneArr))
      {
        scenes = LoadFromJSON(selected.nextSceneArr.sceneArr, (TimeOfDay) Enum.Parse(typeof(TimeOfDay), selected.nextSceneArr.timeslot));
        sceneIdx = selected.nextSceneArr.startIndex;
        currentScene = scenes[sceneIdx];
        return;
      }

      // Load From Same scene array
      sceneIdx = selected.newIndex != 0 ? selected.newIndex : sceneIdx + 1;
      if (sceneIdx >= scenes.Length)
      {
        Debug.Log("Reached end of scenes array, staying at last scene");
        sceneIdx = scenes.Length - 1;
      }
      currentScene = scenes[sceneIdx];
      return;
    }

    // No choice made.

    // Check if next scene is from different scene array
    if (!string.IsNullOrEmpty(currentScene.nextSceneArr.sceneArr))
    {
      scenes = LoadFromJSON(currentScene.nextSceneArr.sceneArr, (TimeOfDay) Enum.Parse(typeof(TimeOfDay), currentScene.nextSceneArr.timeslot)); 
      sceneIdx = currentScene.nextSceneArr.startIndex;
      currentScene = scenes[sceneIdx];
    } else
    {
      sceneIdx = currentScene.skip != 0 ? currentScene.skip : sceneIdx + 1;
      if (sceneIdx >= scenes.Length)
      {
        Debug.Log("Reached end of scenes array, staying at last scene");
        sceneIdx = scenes.Length - 1;
      }
      currentScene = scenes[sceneIdx];
    }

  }

  // Function for testing loading scenes
  public void TestNextScene(int choice = -1)
  {
    LoadNextScene(choice);
    Debug.Log("Current Scene Text: " + currentScene.text);
    Debug.Log("Current Scene index: " + sceneIdx);
  }

  // Helper Function: Loads specific Scene array from JSON file
  Scene[] LoadFromJSON(string fileName, TimeOfDay day = 0)
  {
    string filePath, jsonString;

    try
    {
      // Reads all scenes from file
      filePath = Application.dataPath + "/Scripts/Level3Scenes/" + fileName + ".json";
      jsonString = System.IO.File.ReadAllText(filePath);
    } catch (Exception e)
    {
      Debug.Log("Error reading file: " + e.Message);
      return null;
    }

    // Returns specific day scenes
    switch (day)
    {
      case TimeOfDay.Morning:
        return JsonUtility.FromJson<ScenesArr>(jsonString).morning;
      case TimeOfDay.Chemistry:
        return JsonUtility.FromJson<ScenesArr>(jsonString).chemistry;
      case TimeOfDay.History:
        return JsonUtility.FromJson<ScenesArr>(jsonString).history;
      case TimeOfDay.Lunch:
        return JsonUtility.FromJson<ScenesArr>(jsonString).lunch;
      case TimeOfDay.PE:
        return JsonUtility.FromJson<ScenesArr>(jsonString).PE;
      case TimeOfDay.AfterSchool:
        return JsonUtility.FromJson<ScenesArr>(jsonString).afterSchool;
      case TimeOfDay.FirstDate:
        return JsonUtility.FromJson<ScenesArr>(jsonString).firstDate;
      default:
        Debug.Log("Passed in invalid day number, returning null");
        return null;
    }

  }
  
   
}
