using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum TimeOfDay { morning, chemistry, history, lunch, PE, afterSchool, firstDate}

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
  public string emotion;            // How the character's face will look (Happy, Sad, etc)
  public Option[] options;          // optional field for choices
  public int skip;                  // optional field to skip to a specific scene index
  public Prerequisite prereq;       // optional field for prerequisite logic
  public NextSceneArr nextSceneArr; // Loads new Scene array from json (for scene transitions)
  public string voiceOver;          // Path to voice over audio file
  public string location;           // Loads new background Image
  public string bgm;                // Changes BGM to specified track
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

[System.Serializable]
public class Prerequisite
{
  public bool exists;     // required; unity needs this to check if prereq field is null or not
  public bool isPrereq;   // false = this scene requires a prereq, true = this scene is a prereq for another scene; required
  public int prereqSkip;  // if needs prereq, skip to this scene
  public int prereqIndex; // index of prereq in character's prereq list; required
}


public class LoadScene : MonoBehaviour
{
  private Scene currentScene; // get this var when displaying scene

  // Private Variables
  private Scene[] scenes;
  private int sceneIdx = 0;

  // Character References
  private GameObject rowan = null;
  private GameObject gemini = null;
  private GameObject perri = null;


  
  void Start()
  {
    // Starts by loading 0th scene from defaultScenes.json Morning array
    scenes = LoadFromJSON("defaultScenes", (TimeOfDay)0);
    if (scenes == null || scenes.Length == 0)
    {
      Debug.LogError("No scenes loaded from json");
      return;
    }
    currentScene = scenes[sceneIdx];
    // LoadNextScene(-1); // Load first scene
    GameObject.Find("sceneManager").GetComponent<DisplayScene>().UpdateScene();

    // Get all character references
    rowan = GameObject.Find("Rowan");
    gemini = GameObject.Find("Gemini");
    perri = GameObject.Find("Perri");

    if (!rowan || !gemini || !perri)
    {
      Debug.LogError("Failed to find character game objects in LoadScene");
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
        if (scenes == null || scenes.Length == 0)
        {
          Debug.LogError("No scenes loaded from json");
          return;
        }
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

      // check for prereqs
      if (currentScene.prereq != null && currentScene.prereq.exists) CheckPrerequisites();

      currentScene = scenes[sceneIdx];
      return;
    }

    // No choice made.

    // Check if next scene is from different scene array
    if (!string.IsNullOrEmpty(currentScene.nextSceneArr.sceneArr))
    {
      scenes = LoadFromJSON(currentScene.nextSceneArr.sceneArr, (TimeOfDay) Enum.Parse(typeof(TimeOfDay), currentScene.nextSceneArr.timeslot)); 
      if (scenes == null || scenes.Length == 0)
      {
        Debug.LogError("No scenes loaded from json");
        return;
      }
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

      // check for prereqs
      if (currentScene.prereq != null && currentScene.prereq.exists) CheckPrerequisites();

      currentScene = scenes[sceneIdx];
    }

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
      case TimeOfDay.morning:
        return JsonUtility.FromJson<ScenesArr>(jsonString).morning;
      case TimeOfDay.chemistry:
        return JsonUtility.FromJson<ScenesArr>(jsonString).chemistry;
      case TimeOfDay.history:
        return JsonUtility.FromJson<ScenesArr>(jsonString).history;
      case TimeOfDay.lunch:
        return JsonUtility.FromJson<ScenesArr>(jsonString).lunch;
      case TimeOfDay.PE:
        return JsonUtility.FromJson<ScenesArr>(jsonString).PE;
      case TimeOfDay.afterSchool:
        return JsonUtility.FromJson<ScenesArr>(jsonString).afterSchool;
      case TimeOfDay.firstDate:
        return JsonUtility.FromJson<ScenesArr>(jsonString).firstDate;
      default:
        Debug.Log("Passed in invalid day number, returning null");
        return null;
    }

  }

  public Scene getCurrentScene()
  {
    return currentScene;
  }

  private void CheckPrerequisites()
  {
    // Implement logic to check if prerequisites for current scene are met
    // If not, use prereqSkip to jump to a different scene
    Debug.Log("Checking prerequisites for scene: " + currentScene.text);
    if (currentScene.prereq == null) return;

    GameObject character = this.GetComponent<DisplayScene>().findCharacter(currentScene.name);
    if (character == null)
    {
      Debug.LogError("Character not found in scene: " + currentScene.name);
      return;
    }

    // If this scene requires a prereq
    if (!currentScene.prereq.isPrereq)
    {
      if (currentScene.prereq.prereqIndex < 0) return;

      // check if prereq is met, if not, skip to different scene
      if (character == rowan)
        if (rowan.GetComponent<RowanPrereq>().checkPrereq(currentScene.prereq.prereqIndex))
          sceneIdx = currentScene.prereq.prereqSkip;

      if (character == gemini)
        if (gemini.GetComponent<GeminiPrereq>().checkPrereq(currentScene.prereq.prereqIndex))
          sceneIdx = currentScene.prereq.prereqSkip;
      
      if (character == perri)
        if (perri.GetComponent<PerriPrereq>().checkPrereq(currentScene.prereq.prereqIndex))
          sceneIdx = currentScene.prereq.prereqSkip;

      return;
    }

    // If this scene is a prereq for another scene
    if (currentScene.prereq.prereqIndex < 0) return;
    character.GetComponent<CharacterPrereq>().markPrereqTrue(currentScene.prereq.prereqIndex);
    
  }
  
   
}
