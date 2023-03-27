using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    void Update()
    {
        var dir = Camera.main.transform.position;
        dir.y = 0;

        transform.forward = dir;
    }
}
