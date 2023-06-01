using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RotationAxis
{
    X , Y , Z
}
public class Rotate : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 10f;

    public RotationAxis axis;
    Vector3 targetAxis;

    void Start()
    {

        switch(axis)
        {
            case RotationAxis.X:
                targetAxis = Vector3.right;
                break;
            case RotationAxis.Y:
                targetAxis = Vector3.up;
                break;
            case RotationAxis.Z:
                targetAxis = Vector3.forward;
                break;

             default:
                targetAxis = Vector3.zero;
                break;
        }
    }

   
    void Update()
    {
        transform.Rotate(targetAxis * rotationSpeed * Time.deltaTime);
    }
}
