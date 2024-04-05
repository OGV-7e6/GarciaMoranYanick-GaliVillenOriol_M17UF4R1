using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectables
{
    //This trigger enables the gameobject attached to the character.
    public void OnTriggerEnter(Collider col);
}


