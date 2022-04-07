using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysColorChanging : MonoBehaviour
{
    [SerializeField] Material material;

    void Start()
    {
        Black();
    }

    void Black()
    {
        material.color = Color.black;
        Invoke("White", .5F);
    }

    void White()
    {
        material.color = Color.white;
        Invoke("Black", .5F);
    }
}
