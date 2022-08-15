using System.Collections.Generic;
using UnityEngine;

public class PropsAltar : MonoBehaviour
{
    [SerializeField] private GameObject _MAPArena;
    [SerializeField] private GameObject _MAPBase;
    [SerializeField] private GameObject _battleSound;
    [SerializeField] private GameObject _pointCounter;

    public List<SpriteRenderer> runes;

    private Color curColor;
    private Color targetColor;
    private float timeToTeleport = 0;
    private void OnTriggerExit2D(Collider2D other)
    {
        targetColor = new Color(0, 0, 0, 0);
        timeToTeleport = 0;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        targetColor = new Color(1, 1, 1, 1);

        timeToTeleport += Time.deltaTime;
        Debug.Log(timeToTeleport);
        if (timeToTeleport > 1)
        {
            _MAPArena.SetActive(true);
            _MAPBase.SetActive(false);
            _battleSound.SetActive(true);
        }
    }
    private void Update()
    {
        ColorSwap();
    }
    void ColorSwap()
    {
        curColor = Color.Lerp(curColor, targetColor, 5 * Time.deltaTime);
        foreach (var r in runes) r.color = curColor;
    }
}