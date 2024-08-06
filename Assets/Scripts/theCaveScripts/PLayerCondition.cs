using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerCondition : MonoBehaviour
{

    public GameObject player;
    public GameObject objectAct;
    public float zCondition;

    public void Awake()
    {
        player = GameObject.Find("Player");
       
    }
    public void Update()
    {
       
    }
}
