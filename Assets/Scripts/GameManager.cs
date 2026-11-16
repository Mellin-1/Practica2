using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // ==== Config ==== //
    private int vidas = 3;
    private int puntos = 0;
    private int clientesPendientes; // 10 porque se empieza con 10 clientes
    private int cervezas;

    // GUI
    [SerializeField] private TextMeshProUGUI textoPuntos;
    [SerializeField] private TextMeshProUGUI textoClientesPendientes;
    [SerializeField] private GameObject fondoVidas;
    [SerializeField] private GameObject imagenVidas;

    [SerializeField] private GameObject PanelFinal;
    [SerializeField] private TextMeshProUGUI textoFindePartida;

    private bool paused = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ActualizarGUI();
    }

    public void SumaPuntos(int points)
    {
        if (!paused)
        {
            puntos += points;
            ActualizarGUI();
        }
    }

    public void Muerte()
    {
        if (!paused)
        {
            vidas--;
            ActualizarGUI();

            if (vidas <= 0)
                gameOver("Derrota");
        }
    }

    public void NPCEliminado()
    {
        if (!paused)
        {
            eliminarCliente();
            ActualizarGUI();
        }
    }

    private void ActualizarGUI()
    {
        if (textoPuntos != null)
            textoPuntos.text = "Puntos: " + puntos;

        if (textoClientesPendientes != null)
            textoClientesPendientes.text = clientesPendientes.ToString();

        if (fondoVidas != null)
            actualizaVida();

        if (clientesPendientes <= 0 && cervezas <= 0)
            gameOver("Victoria");
    }

    private void actualizaVida()
    {
        for (int i = 0; i < fondoVidas.transform.childCount; i++) // vacia el panel
        {
            GameObject sprite = fondoVidas.transform.GetChild(i).gameObject;
            Destroy(sprite);
        }

        for (int i = 0; i < vidas; i++)                           // Genera el numero correcto de vidas
            Instantiate(imagenVidas, fondoVidas.transform);    
    }

    public void reiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void gameOver(string resultado)
    {
        PanelFinal.SetActive(true);
        paused = true;

        if (resultado == "Derrota")
            textoFindePartida.text = "Has perdido!\r\n¿Lo intentamos  de nuevo?";
        else if (resultado == "Victoria")
            textoFindePartida.text = "Enhorabuena, has ganado!";
    }

    public void registrarCerveza()
    {
        cervezas++;
    }
    public void registrarCliente()
    {
        clientesPendientes++;
    }

    public void eliminarCerveza()
    {
        cervezas--;
    }
    public void eliminarCliente()
    {
        clientesPendientes--;
    }
}
