using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float lagAmount = 0f;

    Vector3 _prevCamPosition;
    Transform _camera;
    Vector3 _targetPosition;
    public float parallaxAmount;

    private float ParallaxAmount => 1f - lagAmount;

    private void Awake()
    {
        _camera = Camera.main.transform;
        _prevCamPosition = _camera.position;
    }

    private void LateUpdate()
    {
        Vector3 movement = CameraMovement;
        if (movement == Vector3.zero)
        {
            return;
        }
        _targetPosition = new Vector3(transform.position.x + movement.x * ParallaxAmount, transform.position.y, transform.position.z);
        transform.position = _targetPosition;
    }

    Vector3 CameraMovement
    {
        get
        {
            Vector3 movement = _camera.position - _prevCamPosition;
            _prevCamPosition = _camera.position;
            return movement;
        }
    }
}
