using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bulletCounter : MonoBehaviour
{
    public int ammo;
    public bool isFiring;
    public TextMeshProUGUI ammoDisplay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = ammo.ToString();
        if(Input.GetMouseButtonDown(0) && !isFiring && ammo > 0)
        {
            isFiring = true;
            ammo--;
            isFiring = false;
        }
        if(Input.GetKeyDown("r") && ammo >= 0)
        {
            ammo = ammo = 1;
        }
    }
}
