using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bully Austin into commenting this so we can roughly understand whats going on

public class ParallaxEffect : MonoBehaviour
{
    ReadInput readInput;

    public GameObject whaleScreen;
    public GameObject drakeScreen;

    [SerializeField] [Range(0f, 1f)] float lagAmount = 0f;

    Vector3 _prevCamPosition;
    Transform _camera;
    Vector3 _targetPosition;
    public float parallaxAmount;

    private float ParallaxAmount => 1f - lagAmount;

    private void Awake(){ //Called when this object is created, sets the camera position
        _camera = Camera.main.transform;
        _prevCamPosition = _camera.position;

        readInput = FindObjectOfType<ReadInput>();
        whaleScreen = GameObject.Find("brendanfrasercharliethewhale");
        drakeScreen = GameObject.Find("drake");

        if (readInput.whalemode)
        {
            whaleScreen.GetComponent<SpriteRenderer>().color = new Color(140, 140, 140, 0.7f);
            drakeScreen.GetComponent<SpriteRenderer>().color = new Color(140, 140, 140, 0f);
        }

        if (readInput.drakemode)
        {
            whaleScreen.GetComponent<SpriteRenderer>().color = new Color(140, 140, 140, 0f);
            drakeScreen.GetComponent<SpriteRenderer>().color = new Color(140, 140, 140, 0.7f);
        }

        if (readInput.original)
        {
            whaleScreen.GetComponent<SpriteRenderer>().color = new Color(140, 140, 140, 0f);
            drakeScreen.GetComponent<SpriteRenderer>().color = new Color(140, 140, 140, 0f);
        }
    }

    private void LateUpdate(){
        Vector3 movement = CameraMovement;
        if (movement == Vector3.zero){
            return;
        }
        _targetPosition = new Vector3(transform.position.x + movement.x * ParallaxAmount, transform.position.y, transform.position.z);
        transform.position = _targetPosition;
    }

    Vector3 CameraMovement{
        get{
            Vector3 movement = _camera.position - _prevCamPosition;
            _prevCamPosition = _camera.position;
            return movement;
        }
    }
}
