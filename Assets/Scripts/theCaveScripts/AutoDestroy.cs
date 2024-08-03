using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{

    public float timeDie;
    public void Start()
    {
        Destroy(gameObject,timeDie);
    }
}
