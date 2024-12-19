using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidad de movimiento de los tubos
    public bool stopMovement = false; // Bandera global para detener el movimiento

    void Update()
    {
        if (stopMovement)
            return; // Si el movimiento está detenido, no hacemos nada

        // Mueve la tubería hacia la izquierda
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // Destruye la tubería si sale de la pantalla
        if (transform.position.x < -10f) // Ajusta según el tamaño de tu pantalla
        {
            Destroy(gameObject);
        }
    }

    // Método estático para detener el movimiento de los tubos
    public void StopPipes()
    {
        stopMovement = true;
    }
}
