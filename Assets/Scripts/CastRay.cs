using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This was my poor attempt at using raycasts


public class CastRay : MonoBehaviour
{
    public LineRenderer laserLineRenderer;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 5f;

    void Start()
    {
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.SetWidth(laserWidth, laserWidth);
    }

    void Update()
    {
        ShootLaserFromTargetPosition(transform.position, Vector3.up, laserMaxLength);
        laserLineRenderer.enabled = true;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
     /*   else
        {
            laserLineRenderer.enabled = false;
        }
    */
    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
        }

        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, transform.position);
    }
}
