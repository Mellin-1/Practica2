using UnityEngine;

public class NPC : MonoBehaviour
{
    private enum EstadoNPC
    {EsperandoCerveza, Bebiendo, Satisfecho}
    private EstadoNPC estadoActual = EstadoNPC.EsperandoCerveza;
    [Header("Configuración del Cliente")]
    [SerializeField] private GameObject prefabVaso;
    [SerializeField] private float velocidad = 0.25f;
    [SerializeField] private float tiempoBebiendo = 1f;

    private float temporizador = 0f;

    void Update()
    {
        switch (estadoActual)
        {
            case EstadoNPC.EsperandoCerveza:
                transform.Translate(Vector3.right * velocidad * Time.deltaTime);
                break;

            case EstadoNPC.Bebiendo:
                temporizador += Time.deltaTime;
                if (temporizador >= tiempoBebiendo)
                {
                    if (prefabVaso != null)
                        Instantiate(prefabVaso, transform.position, Quaternion.identity);
                    temporizador = 0f;
                    estadoActual = EstadoNPC.Satisfecho;
                }
                break;

            case EstadoNPC.Satisfecho:
                transform.Translate(Vector3.left * velocidad * Time.deltaTime);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estadoActual == EstadoNPC.EsperandoCerveza && collision.gameObject.CompareTag("Beer"))
        {
            Destroy(collision.gameObject);
            temporizador = 0f;
            estadoActual = EstadoNPC.Bebiendo;
        }
    }
}