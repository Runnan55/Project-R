using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public AudioSource fxAudioSource; 


    void Start()
    {
        currentHealth = maxHealth;
        fxAudioSource = GameManager.Instance.GetComponent<SoundManager>().fxAudioSource;
    }


    public void TakeDamage(int damage)
    {

        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            Debug.Log("mori");
            Destroy(gameObject);


        }
    }
public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(100);
        }
    }
    
public void Update()
{
    
    if (currentHealth <= 0)
    {
        ActualizarPuntos();
        GameManager.Instance.GetComponent<SoundManager>().PlayFx(AudioFx.death, fxAudioSource, false);
        gameObject.SetActive(false);
    }
}
public void ActualizarPuntos()
{
        SimpleFish simpleFish = GetComponent<SimpleFish>();
        AttackFish attackFish = GetComponent<AttackFish>();
    // Comprueba si el componente SimpleFish se encontró
        if (simpleFish != null)
    {
        // Si SimpleFish está presente, imprime "Holaxd"
        Debug.Log("+1 Azul");
         Text textComponent = GameObject.Find("Canvas/TextPezAzul").GetComponent<Text>();
            if(textComponent != null)
            {
                // Encuentra el objeto que tiene el script CharacterMovement
                CharacterMovement characterMovement = FindObjectOfType<CharacterMovement>();
                if (characterMovement != null)
                {
                    characterMovement.PuntosAzul += 1;
                    // Cambia el texto para mostrar el valor de PuntosAzul
                    textComponent.text = characterMovement.PuntosAzul.ToString();

                 }
            }
            if (attackFish != null)
            {
        // Si SimpleFish está presente, imprime "Holaxd"
        Debug.Log("+1 Red");
                // Si SimpleFish está presente, imprime "Holaxd"
        textComponent = GameObject.Find("Canvas/TextPezRojo").GetComponent<Text>();
            if(textComponent != null)
            {
                // Encuentra el objeto que tiene el script CharacterMovement
                CharacterMovement characterMovement = FindObjectOfType<CharacterMovement>();
                if (characterMovement != null)
                {
                    characterMovement.PuntosRojo += 1;
                    // Cambia el texto para mostrar el valor de PuntosAzul
                    textComponent.text = characterMovement.PuntosRojo.ToString();
                }
            
            }
     }
     }
    }
}
