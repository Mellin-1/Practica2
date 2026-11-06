using UnityEngine;

public class Desliza : MonoBehaviour
{

    [SerializeField] private float Velocidad = 1f;

    public void setVel(float val)
    {
        Velocidad = val;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x + Velocidad * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
