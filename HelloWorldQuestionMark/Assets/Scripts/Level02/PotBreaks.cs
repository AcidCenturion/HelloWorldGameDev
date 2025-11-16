using UnityEngine;
using System.Collections;

public class PotBreaks : MonoBehaviour
{
    private Animator anim;
    public GameObject coinObject;
    private int random = 2;

    void Start()
    {
        anim = GetComponent<Animator>();
        random = Random.Range(0, 3);
    }

    void Update()
    {
        
    }

    public void Broken()
    {
        anim.SetBool("broken", true);

        StartCoroutine(breakCo());
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(0.3f);
        
        //Instantiates between 0 and 2 coins (random) when pot Breaks, then destroys pot
        for (int i = 0; i < random; i++)
        {
            GameObject coin = Instantiate(coinObject, transform.position, Quaternion.Euler(0,0,0));
        }

        Destroy(this.gameObject);
    }
}
