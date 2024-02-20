using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Camera mainCamera; 
    
    [SerializeField] private Transform target;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }
    
    private void LateUpdate()
    {
        var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = target.position * 0.7f + mousePos * 0.3f;
        targetPosition.z = -1;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
