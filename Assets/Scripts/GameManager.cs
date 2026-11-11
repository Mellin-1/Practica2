using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private int vidas = 3;
    [SerializeField] private int puntos = 0;
    [SerializeField] private int clientesPendientes = 10; // 10 porque se empieza con 10 clientes
    [SerializeField] private TextMeshProUGUI textoPuntos;
    [SerializeField] private TextMeshProUGUI textoClientesPendientes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ActualizarGUI();
    }

    public void SumaPuntos(int points)
    {
        puntos += points;
        ActualizarGUI();
        Debug.Log("puntos: " + puntos);
    }

    public void Muerte()
    {
        vidas--;
        ActualizarGUI();
        Debug.Log("vidas: " + vidas);

        if (vidas <= 0)
        {
            Debug.Log("game over"); //
        }
    }

    public void NPCEliminado()
    {
        clientesPendientes--;
        ActualizarGUI();

        if (clientesPendientes <= 0)
        {
            Debug.Log("nivel completado");
        }
    }

    private void ActualizarGUI()
    {
        if (textoPuntos != null)
            textoPuntos.text = "Puntos: " + puntos;

        if (textoClientesPendientes != null)
            textoClientesPendientes.text = "Pendientes: " + clientesPendientes;
    }
}
