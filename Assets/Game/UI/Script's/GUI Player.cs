using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPlayer : MonoBehaviour
{
    private bool isPause;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            Time.timeScale = isPause ? 0 : 1;
            //pause.gameObject.SetActive(isPause);
            //ButtonOut.gameObject.SetActive(isPause);
            //ButtonRestart.gameObject.SetActive(isPause);
        }
    }
}
