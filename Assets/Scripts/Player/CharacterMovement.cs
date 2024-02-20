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



void Start() 
{
    Cursor.lockState = CursorLockMode.Locked; 
    Cursor.visible = false; 

      currentBullets = maxBullets;
        animator = GetComponent<Animator>();
   
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

        animator.SetBool("IsMoving", true);

    }

void DisaproJugador()
{

        if (!canShoot || currentBullets <= 0)
         return;

     
    
    if (Input.GetMouseButtonDown(0))
    {
        // Crea una copia del proyectil
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

        // Añade una velocidad al proyectil para que se mueva en la dirección en la que la cámara está mirando
        newProjectile.GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * projectileSpeed;
        

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
