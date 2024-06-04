using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    public TextMeshProUGUI counterText;
    int kills;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        
        counterText.text = enemies.Length.ToString();
    }
}
