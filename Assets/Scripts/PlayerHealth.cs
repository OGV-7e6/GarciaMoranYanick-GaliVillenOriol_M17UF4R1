using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public HealthDisplay healthDisplay;
    // Start is called before the first frame update
    void Start()
    {
        healthDisplay.SetDisplay(maxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthDisplay.SetDisplay(currentHealth);
    }
}
