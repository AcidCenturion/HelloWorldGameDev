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
        string filePath = Application.dataPath + "/Scripts/Level3/scenes.json";
        string jsonString = System.IO.File.ReadAllText(filePath);
        testArray myTest = JsonUtility.FromJson<testArray>(jsonString);
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