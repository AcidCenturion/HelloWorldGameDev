using UnityEngine;
using System.Collections.Generic;

public class CharacterPrereq : MonoBehaviour
{
    private List<bool> prereqIDs = new List<bool>();

    // Add & returns index of new prereq
    public int addPrereq()
    {
        prereqIDs.Add(false);
        return prereqIDs.Count - 1; 
    }

    // Check if prereq at index is complete
    public bool checkPrereq(int index)
    {
        if (index < 0 || index >= prereqIDs.Count)
        {
            Debug.LogError("Index out of bounds: " + index);
            return false; 
        }

        Debug.Log("Checking prereq " + index + " for " + gameObject.name + ": " + prereqIDs[index]);
        
        return prereqIDs[index];
    }

    // Mark prereq at index as complete
    public void markPrereqTrue(int index)
    {
        if (index < 0 || index >= prereqIDs.Count)
        {
            Debug.LogError("Index out of bounds: " + index);
            return; 
        }

        Debug.Log("Marking prereq " + index + " as true for " + gameObject.name);
        
        prereqIDs[index] = true;
    }

    


}
