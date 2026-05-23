using UnityEngine;

public class TextReader : MonoBehaviour
{
    [System.Serializable]
    public class test
    {
        public string name;
        public string gender;
        public string empty;
    }
    [System.Serializable]
    public class testArray{
        public test[] data;
    }

    void Start()
    {
        Debug.Log("Before JSON load");

        TextAsset jsonFile = Resources.Load<TextAsset>("/Scripts/Level3/scenes");

        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found!");
            return;
        }

        string jsonString = jsonFile.text;
        testArray myTest = JsonUtility.FromJson<testArray>(jsonString);

        Debug.Log("After JSON load");

        for (int i = 0; i < myTest.data.Length; i++)
        {
            Debug.Log("Name: " + myTest.data[i].name);
            Debug.Log("gender: " + myTest.data[i].gender);
            Debug.Log("Empty: " + myTest.data[i].empty);
        }
    }
}



/*
using UnityEngine;

[System.Serializable]
public class test
{
    public string name;
    public string gender;
    public string empty;
}
[System.Serializable]
public class testArray{
    public test[] data;
}

public class TextReader : MonoBehaviour
{
    void Start()
    {
        //data path gives the path to the assets folder
        // change the addition to location of JSON
        Debug.Log("Before JSON load");
        string filePath = Application.dataPath + "/Scripts/Level3/scenes.json";
        string jsonString = System.IO.File.ReadAllText(filePath);
        testArray myTest = JsonUtility.FromJson<testArray>(jsonString);
        Debug.Log("After JSON load");
        for (int i = 0; i < myTest.data.Length; i++)
        {
            Debug.Log("Name: " + myTest.data[i].name);
            Debug.Log("gender:" + myTest.data[i].gender);
            Debug.Log("Empty: " + myTest.data[i].empty);
            if (myTest.data[i].empty == null)
            {
                Debug.Log("empty is null");
            }

        }
    }
}
*/