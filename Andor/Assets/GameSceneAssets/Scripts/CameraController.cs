using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 5f;
    public float scrollSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    public float minY = 2f;
    public float maxY = 10f;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if(Input.GetKey("up") || Input.mousePosition.y >= Screen.height - panBorderThickness){
            pos.z += panSpeed * Time.deltaTime;
        }

        if(Input.GetKey("down") || Input.mousePosition.y <= panBorderThickness){
            pos.z -= panSpeed * Time.deltaTime;
        }

        if(Input.GetKey("left") || Input.mousePosition.x <= panBorderThickness){
            pos.x -= panSpeed * Time.deltaTime;
        }

        if(Input.GetKey("right") || Input.mousePosition.x >= Screen.width - panBorderThickness){
            pos.x += panSpeed * Time.deltaTime;
        }
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y += scroll* scrollSpeed * Time.deltaTime;
        panLimit.x = (-7f/8f)*pos.y+(8.75f);
        panLimit.y = (-5f/8f)*pos.y+(6.25f);

        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}
