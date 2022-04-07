using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int difficulty;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 1 ,0), Time.deltaTime * difficulty);
    }
}
