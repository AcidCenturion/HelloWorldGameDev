using UnityEngine;

public class BoxPuzzleReset : MonoBehaviour
{
    [SerializeField] Box[] boxes;

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