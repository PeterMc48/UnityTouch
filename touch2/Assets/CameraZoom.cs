using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    
    [SerializeField]
    private float zoomSpeed = .1f;
    
    private float zoomMin = -15f;
    
    private float zoomMax = 150f;

    Camera cam;

    [SerializeField]
    private Vector3 newPos;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevious =  touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevious =  touchOne.position - touchOne.deltaPosition;

        
            float oldTouchDistance = Vector2.Distance(touchZeroPrevious, touchOnePrevious);
            float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

            float deltaDistance = oldTouchDistance - currentDistance;
            
            if((touchZero.position.x < touchZero.deltaPosition.x && touchOne.position.x < touchOne.deltaPosition.x) || 
            (touchZero.position.y < touchZero.deltaPosition.y && touchOne.position.y < touchOne.deltaPosition.y))
            {
                Zoom(deltaDistance, zoomSpeed);
            }
            

        }

        if(cam.fieldOfView < zoomMin)
        {
            cam.fieldOfView = 15f;
        }
        else if(cam.fieldOfView > zoomMax)
        {
            cam.fieldOfView = 150f;
        }

    }

    void Zoom(float DeltaDiff, float speed)
    {
        cam.fieldOfView += DeltaDiff * speed;

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, zoomMin,zoomMax);
    }
    
    
}
