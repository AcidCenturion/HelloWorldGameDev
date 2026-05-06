using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public void Break()
    {
        Debug.Log("Object broken!");
        Destroy(gameObject);
    }
}
