using UnityEngine.SceneManagement;
using UnityEngine;
public class ButtonStart : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadScene("Game Scene");
}
