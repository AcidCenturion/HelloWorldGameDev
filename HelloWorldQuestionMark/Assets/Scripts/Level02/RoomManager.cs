using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    [Header("All rooms in the level")]
    public List<Room> rooms = new List<Room>();
    private Room currentRoom;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Disable all rooms at startup
        foreach (Room room in rooms)
        {
            room.SetRoomActive(false);
        }

        rooms[0].SetRoomActive(true);
    }

    public void SetRoomActive(Room newRoom)
    {
        if (newRoom == null)
        {
            return;
        }

        // Disable the previous room
        if (currentRoom != null)
        {
            currentRoom.SetRoomActive(false);
        }

        // Enable the new room
        newRoom.SetRoomActive(true);

        currentRoom = newRoom;
    }
}
