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
    [SerializeField]
    private GameObject _VertAim;
    [SerializeField]
    private GameObject _HorAim;
    [SerializeField]
    private ParticleSystem _ParticleSystem;

    void Start() 
    {
        cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        if (!IsVertical)
        {
            IsVertical = true;
            _VertAim.SetActive(true);
            _HorAim.SetActive(false);
        }
        else
        {
            IsVertical = false;
            _VertAim.SetActive(false);
            _HorAim.SetActive(true);
        }
    }

    void Update()
    {
        
        Vector3 mouse = Input.mousePosition;
        var x = Screen.width / 2;
        var y = Screen.height / 2;
        mouse.z = -cam.transform.position.z;
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast( cam.transform.position, cam.transform.forward, out hitInfo);
            if (hit)
            {                
                if (hitInfo.transform.gameObject.tag == "Cut")
                {
                    obj = hitInfo.transform.gameObject;
                    _ParticleSystem.Play();
                }

                pointA = cam.ScreenToWorldPoint(mouse);
                if (IsVertical)
                {
                    pointB = new Vector3(pointA.x, pointA.y , pointA.z) + transform.up*2;
                }
                else
                {
                    pointB = new Vector3(pointA.x, pointA.y, pointA.z) +transform.right *2;
                }
                CreateSlicePlane();
            }
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            if (!IsVertical)
            {
                IsVertical = true;
                _VertAim.SetActive(true);
                _HorAim.SetActive(false);
            }
            else
            {
                IsVertical = false;
                _VertAim.SetActive(false);
                _HorAim.SetActive(true);
            }
        }

       
    }

    void CreateSlicePlane() {
        Vector3 centre = (pointA+pointB)/2;
        Vector3 up = Vector3.Cross((pointA-pointB),(pointA-cam.transform.position)).normalized;
        
        
        Cutter.Cut(obj, centre, up,Mat,true,true);
    }
}
