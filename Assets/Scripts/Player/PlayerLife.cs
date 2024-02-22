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
    void Awake()
    {
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
                // Desbloquea el cursor
    Cursor.lockState = CursorLockMode.None;
    
    // Hace el cursor visible
    Cursor.visible = true;
            SceneManager.LoadScene("Level");

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
