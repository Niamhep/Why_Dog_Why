using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    public bool IsStartMenuTower = false;

    public GameObject Particles;
    public GameObject WinScreen;
    private bool _won = false;

    private int _receivingSignals;

    public void Win()
    {
        if (IsStartMenuTower)
        {
            _receivingSignals++;
        }
        else
        {
            Particles.SetActive(true);
            WinScreen.SetActive(true);
            _won = true;
        }

    }

    private void Update()
    {
        if (!IsStartMenuTower)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void LateUpdate()
    {
        if(IsStartMenuTower)
        {
            Particles.SetActive(_receivingSignals > 0);
            if (_receivingSignals == 4)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                _receivingSignals = 0;
            }
        }
    }
}
