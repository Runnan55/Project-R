using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestSave : MonoBehaviour
{
    int coins;
    public TextMeshProUGUI coinsTMP;

    void Start()
    {
        // Obtener la referencia al SaveManager a través del GameManager
    //    SaveManager saveManager = GameManager.Instance.saveManager;

        // Cargar las monedas al inicio
    //    coins = saveManager.Load("coins", 0); // 0 es el valor predeterminado si no hay datos guardados
        UpdateText();
    }

    public void AddCoin()
    {
        // Obtener la referencia al SaveManager a través del GameManager
    //    SaveManager saveManager = GameManager.Instance.saveManager;

        coins++;
        UpdateText();
    //    saveManager.Save("coins", coins); // Guardar las monedas después de incrementarlas
    }

    void UpdateText()
    {
        coinsTMP.text = "Coins: " + coins.ToString();
    }
}

/* {
    int coins;
    public TextMeshProUGUI coinsTMP;

    public void SaveData()
    {
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.Save();
    }

    public void LoadNumber()
    {
        coins = PlayerPrefs.GetInt("coins");
        UpdateText();
        UpdateText();
    }

    public void AddCoin()
    {
        coins++;
        UpdateText();
    }

    public void UpdateText()
    {
        coinsTMP.text = "Coins: " + coins.ToString();
    }
} */