using UnityEngine;

public class RecogeClientes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPC npc = collision.GetComponentInParent<NPC>();
        if (npc != null)
        {
            // destruye al cliente 
            Destroy(npc.gameObject);
            GameManager.Instance.NPCEliminado();
            GameManager.Instance.SumaPuntos(2);
        }
    }
}
