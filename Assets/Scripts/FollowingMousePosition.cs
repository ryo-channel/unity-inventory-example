using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingMousePosition : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Input.mousePosition;       
    }
}
