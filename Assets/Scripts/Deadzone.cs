using UnityEngine;

public class Deadzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Desliza desliza = collision.GetComponentInParent<Desliza>();

        if (desliza!= null)                                             // Duck Typing para eliminar unicamente los objetos con componente desliza
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.eliminarCerveza();
                GameManager.Instance.Muerte();
            }
            Destroy(desliza.gameObject);
        }

        NPC npc = collision.GetComponentInParent<NPC>();
        if (npc != null)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Muerte();
                GameManager.Instance.NPCEliminado();
            }
            Destroy(npc.gameObject);
        }
    }
}
