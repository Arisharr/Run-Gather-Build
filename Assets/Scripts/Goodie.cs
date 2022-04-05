using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goodie : MonoBehaviour
{
    public string type;
    public int value;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().Gather(type, value, gameObject);
        }
    }
}
