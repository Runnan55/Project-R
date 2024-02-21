using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.gameObject.tag == "bullet")
        {
            TakeDamage(100);
        }
    }
    
public void Update()
{
    if (currentHealth <= 0)
    {
                    GameManager.Instance.GetComponent<SoundManager>().PlayFx(AudioFx.death, fxAudioSource, false);

        gameObject.SetActive(false);
    }
}

}