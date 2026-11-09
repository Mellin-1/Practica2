using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameObject cerveza;
    [SerializeField] private GameObject Spawner;
    [SerializeField] private float Cooldown;
    private PlayerStandPoint[] puntos;
    private int pos = 0;
    private bool cd = false;

    void Start() // Debug para comprobar la funcion spawnBeer() y para asignar los puntos donde aparece el jugador
    {
        puntos = FindObjectsByType<PlayerStandPoint>();
        spawnBeer();
    }

    // Conectar la accion Jump del Input System de Unity con spawnBeer();
    // Conectar la accion Up con movePlayer(-1); y Down con movePlayer(1);

    void movePlayer(int dir)
    {
        pos += dir;
        if (pos >= puntos.Length)
            pos = 0;

        transform.position = puntos[pos].transform.position;
    }


    void spawnBeer()
    {
        if (cd != true)
        {
            cd = true;
            GameObject BeerClone = Instantiate(cerveza, Spawner.transform.position, Quaternion.identity);
            BeerClone.GetComponent<Desliza>().setVel(-1);
            // aqui incluir una forma de esperar antes de desactivar el cooldown de usar (utilizando la variable int Cooldown)
            cd = false;
        }
    }
}
