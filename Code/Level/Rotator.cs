using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _angleStep = 10.0f;

    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.AngleAxis(-_angleStep * Time.fixedDeltaTime, Vector3.up);
    }
}
