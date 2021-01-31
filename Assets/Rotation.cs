using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float DegreePerSec;
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * DegreePerSec);
    }
}
