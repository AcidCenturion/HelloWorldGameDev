using UnityEngine;

public class RoomSensor : MonoBehaviour
{
    [Header("Room this sensor belongs to")]
    public Room room;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Tell the RoomManager to activate this room
            RoomManager.Instance.SetRoomActive(room);
        }
    }
}
