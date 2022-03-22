using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrallaxMovement : MonoBehaviour

{
    [SerializeField] private Transform cam;
    [SerializeField] private float matchCamXMovement = 0f;
    [SerializeField] private float matchCamYMovement = 0f;
    [SerializeField] private Vector2 offset;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector2(cam.position.x * matchCamXMovement, cam.position.y * matchCamYMovement) + offset;
    }
}
