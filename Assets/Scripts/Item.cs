using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private AudioClip audio;
    [SerializeField] private int puntos;

    public AudioClip GetAudioClip()
    {
        return audio;
    }
    public int GetPoints()
    {
        return puntos;
    }
}
