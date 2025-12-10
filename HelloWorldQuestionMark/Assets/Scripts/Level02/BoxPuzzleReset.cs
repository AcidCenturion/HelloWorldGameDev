using UnityEngine;

public class BoxPuzzleReset : MonoBehaviour
{
    [SerializeField] Box[] boxes;
    void Update()
    {
        // TODO: get player location and see if it overlaps w/ the button area
        // TODO: reset each box id to the original location
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // TODO: press button
        Debug.Log("Resetting boxes");
        foreach (Box b in boxes)
        {
            b.transform.position = b.originalPos;
        }
    }
}