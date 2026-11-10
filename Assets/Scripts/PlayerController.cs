using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameObject cerveza;
    [SerializeField] private GameObject Spawner;
    [SerializeField] private float Cooldown;
    private PlayerStandPoint[] puntos;

    private InputAction moveAction;
    private InputAction jumpAction;
    private int pos = 0;
    private bool cd = false;

    void Start()
    {
        puntos = FindObjectsByType<PlayerStandPoint>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        if (moveAction == null || jumpAction == null || puntos.Length == 0)
        {
            Debug.Log("Error a la hora de inicializar!");
            Destroy(this.gameObject);
            return;
        }

        movePlayer(0); // inicializa al jugador en el punto 0
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void Update()
    {
        if (moveAction.WasPressedThisFrame())
        {
            if (moveAction.ReadValue<Vector2>().y > 0) // Arriba
                movePlayer(-1);
            else if (moveAction.ReadValue<Vector2>().y < 0) // Abajo
                movePlayer(1);
        }

        if (jumpAction.WasPressedThisFrame())
            spawnBeer();
    }

    void movePlayer(int dir)
    {
        pos += dir;
        if (pos >= puntos.Length)
            pos = 0;
        else if (pos < 0)
            pos = puntos.Length - 1;
        transform.position = GetSpawnPoint(pos).transform.position;
    }

    private PlayerStandPoint GetSpawnPoint(int x)
    {
        bool search = true;
        int i = 0;
        PlayerStandPoint GoalPoint = null;

        while (search && i < puntos.Length)
        {
            if (x == puntos[i].GetIndex())
            {
                GoalPoint = puntos[i];
                search = false;
            }
            i++;
        }

        if (GoalPoint == null)
            Debug.Log("Punto no encontrado");

        return GoalPoint;
    }


    void spawnBeer()
    {
        if (cd != true)
        {
            cd = true;
            GameObject BeerClone = Instantiate(cerveza, Spawner.transform.position, Quaternion.identity);
            BeerClone.GetComponent<Desliza>().setVel(-2);
            // aqui incluir una forma de esperar antes de desactivar el cooldown de usar (utilizando la variable int Cooldown)
            cd = false;
        }
    }
}
