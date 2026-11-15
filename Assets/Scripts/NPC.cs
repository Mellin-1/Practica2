using UnityEngine;

public class NPC : MonoBehaviour
{
    private enum EstadoNPC
    {EsperandoCerveza, CogiendoCerveza, Bebiendo}
    private EstadoNPC estadoActual = EstadoNPC.EsperandoCerveza;

    [SerializeField] private GameObject prefabVaso;
    [SerializeField] private float speed = 0.25f;
    [SerializeField] private float retrocesoInSecs = 2f; 
    [SerializeField] private float tiempoBebiendo = 1f;

    private float temporizador = 0f;

    void Update()
    {
        switch (estadoActual)
        {
            case EstadoNPC.EsperandoCerveza:
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                break;

            case EstadoNPC.CogiendoCerveza:
                transform.Translate(Vector3.left * (speed * 2f) * Time.deltaTime);

                temporizador += Time.deltaTime;
                if (temporizador >= retrocesoInSecs)
                {
                    temporizador = 0f;
                    estadoActual = EstadoNPC.Bebiendo;
                }
                break;

            case EstadoNPC.Bebiendo:
                temporizador += Time.deltaTime;
                if (temporizador >= tiempoBebiendo)
                {
                    if (prefabVaso != null)
                    {
                        Instantiate(prefabVaso, transform.position, Quaternion.identity);
                    }

                    temporizador = 0f;
                    estadoActual = EstadoNPC.EsperandoCerveza;
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estadoActual == EstadoNPC.EsperandoCerveza && collision.gameObject.CompareTag("Beer"))
        {
            Destroy(collision.gameObject);
            temporizador = 0f;
            estadoActual = EstadoNPC.CogiendoCerveza; 
        }
    }
}