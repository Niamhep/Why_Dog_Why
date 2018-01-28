using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    public GameObject Particles;
    public GameObject WinScreen;
    private bool _won = false;

    public void Win()
    {
        Particles.SetActive(true);
        WinScreen.SetActive(true);
        _won = true;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
