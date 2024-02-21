using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
public float speed = 10.0f; 
public float VelcocityUPDown = 1.0f; 
public float sensitivity = 0.5f;
public Animator animator;

private Vector3 moveDirection = Vector3.zero;

public Camera playerCamera;

public GameObject projectile; // El objeto de juego que se disparará
 public float projectileSpeed = 20f; // La velocidad a la que se disparará el proyectil
 public int maxBullets = 10; // Máximo número de balas
 private int currentBullets; // Balas actuales
public float reloadTime = 2.5f; // Tiempo de recarga en segundos
private bool canShoot = true; // Si el jugador puede disparar
public GameObject shootOrigin; // Este es el GameObject desde donde se disparará el proyectil

    public AudioSource fxAudioSource; 


    private float mouseSensitivity = 0.5f;

    void Start() 
{
    Cursor.lockState = CursorLockMode.Locked; 
    Cursor.visible = false; 

      currentBullets = maxBullets;
        animator = GetComponent<Animator>();
                   fxAudioSource = GameManager.Instance.GetComponent<SoundManager>().fxAudioSource;

} 

void Update()
{
    MovimientoJugador(); 
    DisaproJugador();
  
}
public void MovimientoJugador()
{
    float moveHorizontal = Input.GetAxis("Horizontal"); 
    float moveVertical = Input.GetAxis("Vertical"); 
    float mouseHorizontal = Input.GetAxis("Mouse X");

    // Rotar el jugador en el eje Y basado en el movimiento horizontal del mouse
    transform.Rotate(0, mouseHorizontal * mouseSensitivity, 0);

    // Mover en la dirección de las teclas presionadas
    Vector3 moveDirection = moveHorizontal * playerCamera.transform.right + moveVertical * playerCamera.transform.forward;
        

        // Si se presiona la tecla de espacio, mover hacia arriba
        if (Input.GetKey(KeyCode.Space))
    {
        moveDirection.y = VelcocityUPDown;
    }

     if (Input.GetKey(KeyCode.LeftShift))
    {
        moveDirection.y = -VelcocityUPDown;
    }

    // Normalizar el vector de movimiento si su longitud es mayor que 1
    if (moveDirection.magnitude > 1)
    {
        moveDirection.Normalize();
    }
       

        moveDirection *= speed;

        transform.position += moveDirection * Time.deltaTime; // Aplicar movimiento

        

    }

void DisaproJugador()
{
    if (!canShoot || currentBullets <= 0)
        return;

    if (Input.GetMouseButtonDown(0))
    {
                            GameManager.Instance.GetComponent<SoundManager>().PlayFx(AudioFx.shoot, fxAudioSource, false);


        // Calcula la posición delante del GameObject shootOrigin
        Vector3 spawnPosition = shootOrigin.transform.position;


        // Crea una copia del proyectil en la posición calculada y con la misma rotación que el objeto
        GameObject newProjectile = Instantiate(projectile, spawnPosition, shootOrigin.transform.rotation);

        // Añade una velocidad al proyectil para que se mueva en la dirección en la que el objeto está mirando
        newProjectile.GetComponent<Rigidbody>().velocity = shootOrigin.transform.forward * projectileSpeed;

        StartCoroutine(Reload());
    }
}

 IEnumerator Reload()
    {
          canShoot = false;
        Debug.Log("Recargando...");

        // Esperar el tiempo de recarga
        yield return new WaitForSeconds(reloadTime);

        // Permitir disparar de nuevo
        canShoot = true;
        Debug.Log("Recarga completa");
    }

}
