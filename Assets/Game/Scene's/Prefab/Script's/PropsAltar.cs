using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsAltar : MonoBehaviour
{
    [SerializeField] private GameObject _MAPArena;
    [SerializeField] private GameObject _MAPBase;

    public List<SpriteRenderer> runes;
    public float lerpSpeed;

    private Color curColor;
    private Color targetColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        targetColor = new Color(1, 1, 1, 1);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        targetColor = new Color(1, 1, 1, 0);
        _MAPArena.SetActive(true);
        _MAPBase.SetActive(false); 
    }
    private void Update()
    {
        curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

        foreach (var r in runes)
        {
           r.color = curColor;

        }
    }
}