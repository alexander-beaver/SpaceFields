using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    public float zoomScaleFactor;

    public static Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.GetComponent(typeof(Camera)) as Camera; ;
        
    }

    // Update is called once per frame
    void Update()
    {

        float destZoom = camera.orthographicSize + this.zoomScaleFactor * Input.mouseScrollDelta.y;
        if(destZoom > 0)
        {
            camera.orthographicSize = destZoom;

        }
        else
        {
            camera.orthographicSize = 0+zoomScaleFactor;
        }

      

        


    }
}
