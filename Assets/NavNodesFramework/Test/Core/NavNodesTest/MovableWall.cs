using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableWall : MonoBehaviour
{
    private Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.position;
    }

    public void MoveAlongXAxis(float _amount)
    {
        transform.position = originalPos + Vector3.right * _amount;
    }
}
