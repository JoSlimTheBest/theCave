using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LVLmanager : MonoBehaviour
{
    public void OpenScene(int numberScene)
    {

        SceneManager.LoadScene(numberScene);

    }
}