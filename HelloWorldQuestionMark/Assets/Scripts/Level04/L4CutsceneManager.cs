using UnityEngine;

public class L4CutsceneManager : MonoBehaviour
{
    public GameObject player;
    public L4PlayerControls l4PlayerControls;
    public L4Health l4Health;
    public L4StageManager l4StageManager; 

    public GameObject generalCutsceneCanvas;
    public GameObject firstCutscene;
    public GameObject secondCutscene;
    public GameObject thirdCutscene;
    public GameObject fourthCutscene;
    public GameObject fifthCutscene;
    public GameObject imageOrange;
    public GameObject imageBlue;
    public GameObject Narrator;
    public GameObject Boss;
    public GameObject PressE;
    public GameObject Text1_1;
    public GameObject Text1_2;
    public GameObject Text1_3;
    public GameObject Text1_4;
    public GameObject Text2_1;
    public GameObject Text2_2;
    public GameObject Text3_1;
    public GameObject Text3_2;
    public GameObject Text4_1;
    public GameObject Text4_2;
    public GameObject Text5_1;
    public GameObject Text5_2;
    

    public bool inCutscene = true;
    public int whichCutscene = 1;
    private int whichText1 = 1;
    private int whichText2 = 1;
    private int whichText3 = 1;
    private int whichText4 = 1;
    private int whichText5 = 1;


    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log(whichCutscene);
        if (inCutscene)
        {
            switch (whichCutscene)
            {
                case 1:
                Cutscene1();
                break;
                
                case 2:
                Cutscene2();
                break;

                case 3:
                Cutscene3();
                break;

                case 4:
                Cutscene4();
                break;

                case 5:
                Cutscene5();
                break;

            }
        }
    }

    void Cutscene1()
    {
        if (inCutscene)
        {
            player.GetComponent<Rigidbody2D>().linearVelocityX = 0;
            l4PlayerControls.enabled = false;
            l4Health.enabled = false;

            generalCutsceneCanvas.SetActive(true);
            firstCutscene.SetActive(true);
            PressE.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                whichText1++;
            }

            switch (whichText1)
            {
                case 1:
                Narrator.SetActive(true);
                imageOrange.SetActive(true);
                Text1_1.SetActive(true);
                break;

                case 2:
                Narrator.SetActive(false);
                imageOrange.SetActive(false);
                Text1_1.SetActive(false);
                Boss.SetActive(true);
                imageBlue.SetActive(true);
                Text1_2.SetActive(true);
                break;

                case 3:
                Boss.SetActive(false);
                imageBlue.SetActive(false);
                Text1_2.SetActive(false);
                Narrator.SetActive(true);
                imageOrange.SetActive(true);
                Text1_3.SetActive(true);
                break;

                case 4:
                Narrator.SetActive(false);
                Text1_3.SetActive(false);
                imageBlue.SetActive(true);
                Text1_4.SetActive(true);
                break;

                case 5:
                firstCutscene.SetActive(false);
                generalCutsceneCanvas.SetActive(false);
                PressE.SetActive(false);
                l4PlayerControls.enabled = true;
                l4Health.enabled = true;
                inCutscene = false;
                break;
            }
        }
        
    }

    void Cutscene2()
    {
        if (inCutscene)
        {
            player.GetComponent<Rigidbody2D>().linearVelocityX = 0;
            l4PlayerControls.enabled = false;
            l4Health.enabled = false;
            

            generalCutsceneCanvas.SetActive(true);
            secondCutscene.SetActive(true);
            PressE.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                whichText2++;
            }

            switch (whichText2)
            {
                case 1:
                Boss.SetActive(true);
                imageBlue.SetActive(true);
                Text2_1.SetActive(true);
                break;

                case 2:
                Boss.SetActive(false);
                imageBlue.SetActive(false);
                Text2_1.SetActive(false);
                Narrator.SetActive(true);
                imageOrange.SetActive(true);
                Text2_2.SetActive(true);
                break;

                case 3:
                Narrator.SetActive(false);
                imageOrange.SetActive(false);
                Text2_2.SetActive(false);
                secondCutscene.SetActive(false);
                generalCutsceneCanvas.SetActive(false);
                l4PlayerControls.enabled = true;
                l4Health.enabled = true;
                inCutscene = false;
                break;
            }
        }
        
    }

    void Cutscene3()
    {
        if (inCutscene)
        {
            player.GetComponent<Rigidbody2D>().linearVelocityX = 0;
            l4PlayerControls.enabled = false;
            l4Health.enabled = false;
            

            generalCutsceneCanvas.SetActive(true);
            thirdCutscene.SetActive(true);
            PressE.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                whichText3++;
            }

            switch (whichText3)
            {
                case 1:
                Boss.SetActive(true);
                imageBlue.SetActive(true);
                Text3_1.SetActive(true);
                break;

                case 2:
                Boss.SetActive(false);
                imageBlue.SetActive(false);
                Text3_1.SetActive(false);
                Narrator.SetActive(true);
                imageOrange.SetActive(true);
                Text3_2.SetActive(true);
                break;

                case 3:
                Narrator.SetActive(false);
                imageOrange.SetActive(false);
                Text3_2.SetActive(false);
                secondCutscene.SetActive(false);
                generalCutsceneCanvas.SetActive(false);
                l4PlayerControls.enabled = true;
                l4Health.enabled = true;
                inCutscene = false;
                break;
            }
        }
        
    }

    void Cutscene4()
    {
        if (inCutscene)
        {
            player.GetComponent<Rigidbody2D>().linearVelocityX = 0;
            l4PlayerControls.enabled = false;
            l4Health.enabled = false;
            

            generalCutsceneCanvas.SetActive(true);
            fourthCutscene.SetActive(true);
            PressE.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                whichText4++;
            }

            switch (whichText4)
            {
                case 1:
                Boss.SetActive(true);
                imageBlue.SetActive(true);
                Text4_1.SetActive(true);
                break;

                case 2:
                Boss.SetActive(false);
                imageBlue.SetActive(false);
                Text4_1.SetActive(false);
                Narrator.SetActive(true);
                imageOrange.SetActive(true);
                Text4_2.SetActive(true);
                break;

                case 3:
                Narrator.SetActive(false);
                imageOrange.SetActive(false);
                Text4_2.SetActive(false);
                secondCutscene.SetActive(false);
                generalCutsceneCanvas.SetActive(false);
                l4PlayerControls.enabled = true;
                l4Health.enabled = true;
                inCutscene = false;
                break;
            }
        }
        
    }

    void Cutscene5()
    {
        if (inCutscene)
        {
            player.GetComponent<Rigidbody2D>().linearVelocityX = 0;
            l4PlayerControls.enabled = false;
            l4Health.enabled = false;
            

            generalCutsceneCanvas.SetActive(true);
            fifthCutscene.SetActive(true);
            PressE.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                whichText5++;
            }

            switch (whichText5)
            {
                case 1:
                Boss.SetActive(true);
                imageBlue.SetActive(true);
                Text5_1.SetActive(true);
                break;

                case 2:
                Boss.SetActive(false);
                imageBlue.SetActive(false);
                Text5_1.SetActive(false);
                Narrator.SetActive(true);
                imageOrange.SetActive(true);
                Text5_2.SetActive(true);
                break;

                case 3:
                Narrator.SetActive(false);
                imageOrange.SetActive(false);
                Text5_2.SetActive(false);
                secondCutscene.SetActive(false);
                generalCutsceneCanvas.SetActive(false);
                l4PlayerControls.enabled = true;
                l4Health.enabled = true;
                inCutscene = false;
                break;
            }
        }
        
    }
}
