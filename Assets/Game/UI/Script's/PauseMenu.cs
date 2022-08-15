using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _PauseMenu;
    [SerializeField] private GameObject _DeathMenu;
    private bool isPause = false;
    private void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        PauseMenuActivate();
    }
    void PauseMenuActivate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            Time.timeScale = isPause ? 0 : 1;
            _PauseMenu.SetActive(isPause);
        }
    }
    public void DeathMenuActivate()
    {
        //isPause = !isPause;
        //Time.timeScale = isPause ? 0 : 1;
        _DeathMenu.SetActive(true);
    }

    public void ExitInMenu() => SceneManager.LoadScene("Menu Scene");
}
