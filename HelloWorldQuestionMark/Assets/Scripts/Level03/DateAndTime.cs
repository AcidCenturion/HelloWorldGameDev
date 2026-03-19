using UnityEngine;
using TMPro;

public class DateAndTime : MonoBehaviour
{
    string[] Times = {"7 AM", "8 AM", "10 AM",
                      "12 PM", "1 PM", "3 PM", "5 PM"};
    private int currTime = 0;

    public GameObject TimeObject = null;
    private TextMeshProUGUI timeText;

    void Start()
    {
        if (!TimeObject) return;

        timeText = TimeObject.GetComponent<TextMeshProUGUI>();
        timeText.text = Times[currTime];
    }

    public void UpdateTime()
    {
        if (currTime + 1 >= Times.Length) return;
        currTime++;

        timeText.text = Times[currTime];
    }
}
