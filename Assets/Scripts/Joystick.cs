using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the main character control. currently it only works with the mouse but later adaptation needs to include touch control to be 
//compatible with mobile touch screens.

//TO EDIT
//*Make the controller UI stay in the same position relative to screen to avoid it moving out of scene

public class Joystick : MonoBehaviour
{
    public Transform Head;
    public float speed = 5.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    public Transform circle;
    public Transform outerCircle;

    void Update()
    {
        //When mouse left click
        if (Input.GetMouseButtonDown(0))
        {
            //Set mouse position to new point called PointA
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            //Move controller UI to position of mouse
            circle.transform.position = pointA;
            outerCircle.transform.position = pointA;
            circle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
        //While mouse left click is held down
        if (Input.GetMouseButton(0))
        {
            //Set mouse position to new point called PointB
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z)*1.8f);
        }
        else
        {
            //Do nothing
            touchStart = false;
        }

    }
    private void FixedUpdate()
    {
        //If mouse is clicked
        if (touchStart)
        {
            //Create a new vector from pointA to pointB
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

            //Move character with direction of newly created vector
            moveCharacter(direction);

            //Move inner circle to display movement of character
            circle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        }
        else
        {
            //Do not render controller UI
            circle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    //*******************************************************HELPER FUNCTIONS***************************************************************

    //move character with varables preset in update functions
    void moveCharacter(Vector2 direction)
    {
        //Move character in new direction at set speed
        Head.GetComponent<Rigidbody2D>().velocity += direction*speed;
    }
}