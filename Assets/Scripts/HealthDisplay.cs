using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private GameObject healthDisplay;
    
    public void SetDisplay(float amount)
    {
        healthDisplay.GetComponent<TextMeshProUGUI>().text = amount.ToString();
    }

}