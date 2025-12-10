
using System;
using UnityEngine;

[RequireComponent(typeof (BoxCollider2D))]
public class Box : MonoBehaviour
{
    [SerializeField] float moveDistance = 1.2f;
    [SerializeField] Transform box;
    [SerializeField] BoxPuzzleReset resetButton;
    private Vector2 startingPos;
    public Vector2 originalPos
    {
        get
        {
            return this.startingPos;
        }

        set
        {
            this.startingPos = value;
        }
    }
    
    void Start()
    {
        this.originalPos = this.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 boxContact = contactPoint - (Vector2) this.transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, boxContact);
        Debug.Log("Angle: " + angle);

        // Moves the box opposite to where the Player hit it
        if (angle >= -45 && angle <= 45)
        {
            this.moveBox("west");
        }
        else if (angle > 45 && angle <= 135)
        {
            this.moveBox("south");
        }
        else if (angle > 135 || angle < -135)
        {
            this.moveBox("east");
        }
        else if (angle >= -135 && angle <= -45)
        {
            this.moveBox("north");
        }
    }
    
    // TODO: smooth the movement
    private Boolean moveBox(String direction)
    {
        // Tries to move the box in a direction
        Vector2 newPosition;
        switch (direction)
        {
            case "north":
                newPosition = new Vector2(this.transform.position.x, this.transform.position.y + moveDistance);
                break;
            case "south":
                newPosition = new Vector2(this.transform.position.x, this.transform.position.y - moveDistance);
                break;
            case "east":
                newPosition = new Vector2(this.transform.position.x + moveDistance, this.transform.position.y);
                break;
            case "west":
                newPosition = new Vector2(this.transform.position.x - moveDistance, this.transform.position.y);
                break;
            default:
                return false;
        }

        // Cancels the movement if another object already occupies the space
        Collider2D[] otherObjects = Physics2D.OverlapCircleAll(newPosition, 0.5f);
        foreach (Collider2D obj in otherObjects)
        {
            if (obj.transform != this.transform)
            {
                Debug.Log("Cannot move to: " + newPosition);
                return false;
            }
        }

        this.transform.position = newPosition;
        return true;
    }
}