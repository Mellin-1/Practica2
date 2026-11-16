using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private enum EstadoNPC
    {EsperandoCerveza, CogiendoCerveza, Bebiendo, Saliendo}
    private EstadoNPC estadoActual = EstadoNPC.EsperandoCerveza;

    [SerializeField] private GameObject prefabVaso;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float tiempoBebiendo = 1f;
    [SerializeField] private float retrocesoInSecs = 2f;
    private Vector3 posInicial; // guardamos la posicion inicial para evitar que salga de la barra mientras está cogiendo cerveza
    private SpriteRenderer Renderer;

    private float temporizador = 0f;

    private void Start()
    {
        posInicial = transform.position;
        Renderer = gameObject.GetComponent<SpriteRenderer>();
        if (GameManager.Instance != null)
            GameManager.Instance.registrarCliente();
    }
    void Update()
    {
        switch (estadoActual)
        {
            case EstadoNPC.EsperandoCerveza:
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                break;

            case EstadoNPC.CogiendoCerveza:

                if (transform.position.x > posInicial.x)
                    transform.Translate(Vector3.left * (2f * speed) * Time.deltaTime);

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
                        if (GameManager.Instance != null)
                            GameManager.Instance.registrarCerveza();
                    }

                    temporizador = 0f;
                    estadoActual = EstadoNPC.Saliendo;
                }
                break;
            case EstadoNPC.Saliendo:
                Renderer.flipX = true;
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estadoActual == EstadoNPC.EsperandoCerveza && collision.gameObject.CompareTag("Beer"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.eliminarCerveza();
            Destroy(collision.gameObject);
            temporizador = 0f;
            estadoActual = EstadoNPC.CogiendoCerveza; 
        }
    }
}