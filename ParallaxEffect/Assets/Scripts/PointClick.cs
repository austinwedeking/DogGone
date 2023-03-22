using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointClick : MonoBehaviour
{
    GameObject cameraObject;
    Camera theCamera;
    Vector2 destinationPosition;

    // Start is called before the first frame update
    void Start()
    {
        cameraObject = GameObject.Find("Main Camera");
        theCamera = cameraObject.GetComponent<Camera>();
        destinationPosition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) //left mouse click
        {
            Debug.Log("Mouse clicked. Mouse is at " + Input.mousePosition);
            Vector3 clickPosition = theCamera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("In world space: " + clickPosition);

            destinationPosition = new Vector2(clickPosition.x, clickPosition.y);

            RaycastHit2D hit = Physics2D.Raycast(destinationPosition, Vector2.up);

            if(hit.transform != null)
            {
                Debug.Log("There is something under the mouse. It is: " + hit.transform.gameObject.name);
            }
            else
            {
                Debug.Log("There is nothing under the mouse.");
            }
        }

        if (Input.GetKey(KeyCode.J))
        {
            Debug.Log("J key detected");
            Debug.Log("Destination is " + destinationPosition);
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, destinationPosition, Time.deltaTime * 5f);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Vector2 raycastDirection = new Vector2(1f, 0f);
            RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, raycastDirection);

            //other workaround
            //Vector2 startPosition = new Vector2(this.gameObject.transform.position.x + 0.7f, this.gameObject.transform.position.y);
            //RaycastHit2D hit = Physics2D.Raycast(startPosition, raycastDirection);

            if (hit.transform != null)
            {
                Debug.Log("There is something to the right of the player. It is: " + hit.transform.gameObject.name);
                float distance = hit.transform.position.x - this.transform.position.x;
            }
            else
            {
                Debug.Log("There is no object to the right of the player.");
            }
        }
    }

    public void OnMouseDown()
    {
        //Debug.Log("The following was clicked: " + this.gameObject.name);
    }
}
