using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRaycaster : MonoBehaviour
{
    [SerializeField]private LayerMask layerMask;
    [SerializeField]private float maxDistance;
    [SerializeField] private Camera camera;

    public RaycastHit Raycast()
    {
        RaycastHit hit;
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction*100, Color.yellow);
        Physics.Raycast(mouseRay,out hit, maxDistance);
        return hit;
    }

    private void Update()
    {
    }
}
