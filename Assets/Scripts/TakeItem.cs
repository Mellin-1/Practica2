using UnityEngine;

public class TakeItem : MonoBehaviour
{
    [SerializeField] private AudioSource speaker;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item && other.tag == "VasoVacio")
        {
            speaker.PlayOneShot(item.GetAudioClip());
            Destroy(item.gameObject);
        }
    }
}
