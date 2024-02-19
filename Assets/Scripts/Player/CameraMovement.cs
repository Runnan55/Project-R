using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 100.0f; // Sensibilidad del movimiento del ratón
    public float clampAngle = 80.0f; // Ángulo máximo de inclinación de la cámara

    private float verticalRotation = 0.0f; // Rotación vertical acumulada
    private float horizontalRotation = 0.0f; // Rotación horizontal acumulada
    private Quaternion initialRotation; // Rotación inicial
    private float targetZRotation; // Rotación objetivo en el eje Z
    private float currentZRotation = 0f;
    private float rotationVelocity = 0f;    
    private float originalYPosition; // Guarda la posición Y original de la cámara
    private float time; // Guarda el tiempo para la interpolación
    public Transform player; // Referencia al jugador
    public Vector3 offset; // La distancia entre la cámara y el jugador
    

    void Start()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        verticalRotation = rotation.x;
        horizontalRotation = rotation.y;
        initialRotation = transform.rotation;

            // Guarda la posición Y original de la cámara
    originalYPosition = transform.position.y;
    time = 0;
    
    }

void Update()
{
    float mouseX = Input.GetAxis("Mouse X");
    float mouseY = -Input.GetAxis("Mouse Y");

    horizontalRotation += mouseX * sensitivity * Time.deltaTime;
    verticalRotation += mouseY * sensitivity * Time.deltaTime;

    verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

    Quaternion localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0.0f);

    if (Input.GetKey(KeyCode.A))
    {
        targetZRotation = 5;
    }
    else if (Input.GetKey(KeyCode.D))
    {
        targetZRotation = -5;
    }
    else
    {
        targetZRotation = 0;
    }

    currentZRotation = Mathf.SmoothDampAngle(currentZRotation, targetZRotation, ref rotationVelocity, 0.5f);



    transform.rotation = localRotation * Quaternion.Euler(0, 0, currentZRotation);

    // Oscilación vertical de la cámara
    time += Time.deltaTime;
    float newYPosition = Mathf.Lerp(-0.5f, 0.5f, Mathf.PingPong(time, 1));

    // Hacer que la cámara siga al jugador y aplique la oscilación
    transform.position = player.position + offset + new Vector3(0, newYPosition, 0);
}

}
