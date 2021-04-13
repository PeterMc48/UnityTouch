using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour, IControlable
{
    public float rotatespeed = 10f;
    private float _startingPosition;
    private Vector3 dragPos;
    // Start is called before the first frame update
    private Renderer render;
    void Start()
    {
        render = GetComponent<Renderer>();
        dragPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = dragPos;
    }
    public void youve_Been_tapped()
    {
        if(render.material.color == Color.red)
        {
            render.material.color = Color.green;
            
        }
        else if(render.material.color == Color.green)
        {
            render.material.color = Color.blue;
        }
        else
        {
            render.material.color = Color.red;
        }    
       
    }
    public void MoveTo(Vector3 distance)
    {
        dragPos = distance;
    }

    public void deactiveSelectedObject()
    {
        render.material.color = Color.grey;
    }

    public void reSize()
    {
        throw new System.NotImplementedException();
    }
    public void rotate()
    {
        Touch touch = Input.GetTouch(0);
              switch (touch.phase)
              {
                case TouchPhase.Began:
                    _startingPosition = touch.position.x;
                    break;
                case TouchPhase.Moved:
                    if (_startingPosition > touch.position.x)
                    {
                        transform.Rotate(Vector3.back, rotatespeed * Time.deltaTime);
                    }
                    else if (_startingPosition < touch.position.x)
                    {
                        transform.Rotate(Vector3.back, rotatespeed * Time.deltaTime);
                    }
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Touch Phase Ended.");
                    break;
              }
    }
}
