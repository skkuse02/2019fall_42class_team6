using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject parent;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        this.transform.position.Set(parent.transform.position.x, this.transform.position.y, parent.transform.position.z);
    }
}
