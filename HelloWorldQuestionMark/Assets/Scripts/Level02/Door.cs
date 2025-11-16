using UnityEngine;
public class Door : MonoBehaviour
{
    public int keysObtained;
    public int keysNeeded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keysObtained = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (keysObtained == keysNeeded) {
            Destroy(this.gameObject);
        }
    }
}