using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    [Header("Enemies in this room")]
    public List<GameObject> enemies = new List<GameObject>();
    private bool isActive = false;

    void Awake()
    {
        // Automatically gather enemies if not manually assigned
        if (enemies.Count == 0)
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Enemy"))
                {
                    enemies.Add(child.gameObject);
                }
            }
        }

        // Start with room inactive
        SetRoomActive(false);
    }

    public void SetRoomActive(bool active)
    {
        isActive = active;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(active);
            }
        }
    }
}
