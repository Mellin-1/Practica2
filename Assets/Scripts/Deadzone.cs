using UnityEngine;

public class Deadzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Desliza desliza = collision.GetComponentInParent<Desliza>();
        if (desliza!= null)                                             // Duck Typing para eliminar unicamente los objetos con componente desliza
        {
            Destroy(desliza.gameObject);
        }
    }
}
