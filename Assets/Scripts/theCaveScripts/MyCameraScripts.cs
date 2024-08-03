using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraScripts : MonoBehaviour
{

    public GameObject player;
    public Camera cameraM;

    public void Start()
    {
        player = GameObject.Find("Player");
        cameraM = Camera.main;
    }
    public void Update()
    {
        cameraM.transform.position = new Vector3(player.transform.position.x, 0, -10);
    }
}
