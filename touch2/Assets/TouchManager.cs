using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    
    
    private float speed = 0.01f;
    private float tap;

    private float letGO;
    IControlable selectedObject;
    private Vector3 camPosOrigin;
    private float starting_distance_to_selected_object;
    Vector3 distanceFromCamera;
    // Start is called before the first frame update
    void Start()
    {
        distanceFromCamera = new Vector3(0,Camera.main.transform.position.y,0);
       
    }

    // Update is called once per frame
    void Update()
    {
      letGO = .1f;
      if (Input.touchCount > 0)
      {
        if(Input.touchCount == 2)
        {
            if(selectedObject != null)
            {
              selectedObject.rotate();
            }
            
        }
        else
        {
          Ray ourRay = Camera.main.ScreenPointToRay(Input.touches[0].position);

          Debug.DrawRay(ourRay.origin, 30 * ourRay.direction);

          switch(Input.touches[0].phase)
          {
            case TouchPhase.Began:
              tap = Time.time;
              RaycastHit info;
              if (Physics.Raycast(ourRay, out info))
              {
                  
                IControlable object_hit = info.transform.GetComponent<IControlable>();

                if (object_hit != null)
                {
                    
                  selectedObject = object_hit;
                  starting_distance_to_selected_object = Vector3.Distance(Camera.main.transform.position,transform.position);
                }
              }
              else
              {
                if(selectedObject != null)
                {
                  selectedObject.deactiveSelectedObject();
                  selectedObject = null;
                }
                  
              }
              break;

            case TouchPhase.Moved:

              Ray new_positional_ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
              Vector2 touchPos = Input.touches[0].position;
              Vector3 point = new Vector3();
              point = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x,touchPos.y,starting_distance_to_selected_object));
              if(selectedObject != null)
              {
                selectedObject.MoveTo(point);
              }
              else
              {
                Vector2 TouchDeltaPos = Input.GetTouch(0).deltaPosition;
                Camera.main.transform.Translate(-TouchDeltaPos.x * speed, -TouchDeltaPos.y * speed, 0);
                Camera.main.transform.position = new Vector3(
                  Mathf.Clamp(Camera.main.transform.position.x,-20f, 20f),
                  Mathf.Clamp(Camera.main.transform.position.y,-5f, 5f),
                  Mathf.Clamp(Camera.main.transform.position.z,-25f, 50f)
                );
              }
              break;
            case TouchPhase.Ended:
              tap = Time.time - tap;
              if(selectedObject != null)
              {
                if(tap < letGO)
                {
                  selectedObject.youve_Been_tapped();
                }
              }
              
              break;
          }
        }
      }
    }
  
}
