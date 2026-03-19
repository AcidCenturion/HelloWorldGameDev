using UnityEngine;

public class KingCircle : MonoBehaviour
{
    public GameObject Rowan;
    public GameObject Perri;
    public GameObject Gemini;


    // Called by Load Scene
    public int getNextSceneIndex(int[] affinity)
    {
        if (affinity.Length <= 0) return -1;

        int totalAffinity = getTotalAffinity();

        for (int i = 0; i < affinity.Length; i++)
        {
            if (totalAffinity >= affinity[i])
                return i;
        }

        return -1;
    }

    int getTotalAffinity()
    {
        if (!Rowan || !Perri || !Gemini)
        {
            Debug.Log("King Circle's references aren't assigned");
            return -2;
        }

        int total = 0;
        total += Rowan.GetComponent<Character>().GetAffinity();
        total += Perri.GetComponent<Character>().GetAffinity();
        total += Gemini.GetComponent<Character>().GetAffinity();

        Debug.Log("Returning total affinity of " + total);
        return total;
    }
}
