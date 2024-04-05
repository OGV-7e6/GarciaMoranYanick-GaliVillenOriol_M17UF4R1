using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICollectables
{
    [SerializeField] GameObject item;
    private float randY = 0f;

    public void OnTriggerEnter(Collider col)
    {
        item.SetActive(true);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        var randX = Random.Range(-10f, 10f);
        var randZ = Random.Range(-10f, 10f);
        randY += Random.value * 5f;

        //temblor
        transform.rotation = Quaternion.Euler(randX, transform.rotation.y, randZ);
        //rotacion sobre si mismo
        transform.Rotate(0f, randY, 0f); 
        
        if (randY > 360) randY = 0f;
    }
}
