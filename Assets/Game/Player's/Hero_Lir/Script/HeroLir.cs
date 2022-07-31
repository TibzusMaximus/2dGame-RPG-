using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLir : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        HeroMove();
    }

    void HeroMove()
    {    
        new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
