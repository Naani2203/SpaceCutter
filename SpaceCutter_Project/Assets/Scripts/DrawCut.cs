using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCut : MonoBehaviour
{
    Vector3 pointA;
    Vector3 pointB;
    
    Camera cam;
    [HideInInspector]
    public GameObject obj;
    public Material Mat;
    public  bool IsVertical;

    void Start() {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = -cam.transform.position.z;
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {                
                if (hitInfo.transform.gameObject.tag == "Cut")
                {
                    obj = hitInfo.transform.gameObject;
                }

                pointA = cam.ScreenToWorldPoint(mouse);
                if (IsVertical)
                {
                    pointB = new Vector3(pointA.x, pointA.y + 2, pointA.z);
                }
                else
                {
                    pointB = new Vector3(pointA.x + 2, pointA.y, pointA.z);
                }
            }
                CreateSlicePlane();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            if (!IsVertical)
                IsVertical = true;
            else
                IsVertical = false;
        }

       
    }

    void CreateSlicePlane() {
        Vector3 centre = (pointA+pointB)/2;
        Vector3 up = Vector3.Cross((pointA-pointB),(pointA-cam.transform.position)).normalized;
        
        
        Cutter.Cut(obj, centre, up,Mat,true,true);
    }
}
