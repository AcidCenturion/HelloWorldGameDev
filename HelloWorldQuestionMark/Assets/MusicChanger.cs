using UnityEngine;
public class InitialMusic : MonoBehaviour
{
    public AudioClip music;
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MusicChanger.instance.ChangeMusic(music);
        } 
    }
    //private void OnTriggerExit2D (Collider2D other)
    //{
        //if (other.CompareTag("Player"))
        //{
            //MusicChanger.instance.ReturnInitial();
        //}
    //}
}