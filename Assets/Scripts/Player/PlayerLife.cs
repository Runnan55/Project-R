using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void awake()
    {
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("mainMenu");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Me golpearon" + damage);
        if (currentHealth <= 0)
        {
            
        }
    }
}
