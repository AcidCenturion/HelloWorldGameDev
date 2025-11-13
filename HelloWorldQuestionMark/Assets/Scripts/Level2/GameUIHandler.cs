using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class GameUIHandler : MonoBehaviour
{
    public L2PlayerHealth PlayerHealth;
    public Key Key;
    public UICoinCounter UICoinCounter;
    public UIDocument GameUIDoc;

    public VisualElement Heart1;
    public VisualElement Heart2;
    public VisualElement Heart3;
    public VisualElement KeyOrange;
    public VisualElement KeyBlue1;
    public VisualElement KeyBlue2;
    public Label CoinLabel;

    void Start()
    {
        Heart1 = GameUIDoc.rootVisualElement.Q<VisualElement>("Heart1");
        Heart2 = GameUIDoc.rootVisualElement.Q<VisualElement>("Heart2");
        Heart3 = GameUIDoc.rootVisualElement.Q<VisualElement>("Heart3");

        CoinLabel = GameUIDoc.rootVisualElement.Q<Label>("CoinLabel");

    }

    void Update()
    {
        HealthLost();
    }

    void HealthLost()
    {
        if (PlayerHealth.Health == 3)
        {
            Heart1.style.opacity = 1.0f;
            Heart2.style.opacity = 1.0f;
            Heart3.style.opacity = 1.0f;
        }
        else if (PlayerHealth.Health == 2)
        {
            Heart1.style.opacity = 1.0f;
            Heart2.style.opacity = 1.0f;
            Heart3.style.opacity = 0.0f;
        }
        else if (PlayerHealth.Health == 1)
        {
            Heart1.style.opacity = 1.0f;
            Heart2.style.opacity = 0.0f;
            Heart3.style.opacity = 0.0f;
        }
    }

    void KeyCollected()
    {
        
    }

    void CoinCollected()
    {
        
    }

}
