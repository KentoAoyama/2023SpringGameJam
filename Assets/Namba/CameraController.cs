using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] List<CinemachineVirtualCamera> cameraPoints = new List<CinemachineVirtualCamera>();
    //[SerializeField] GameManager gameManager;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] List<LineRenderer> lineRenderers;
    [SerializeField] float nonVisibleTime;
    LineRenderer manipulateLineRenderer;

    Ray ray;
    RaycastHit hit;
    float rayLength = 100f;


    private void Awake()
    {
        //cameraPoints[0].Priority = 1;
        //gameManager.ChangeCameraUI(0);
    }
    public void ChangeCamera()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ResetPriority();
            cameraPoints[0].Priority = 10;
            //gameManager.ChangeCameraUI(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ResetPriority();
            cameraPoints[1].Priority = 10;
            // gameManager.ChangeCameraUI(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ResetPriority();
            cameraPoints[2].Priority = 10;
            //gameManager.ChangeCameraUI(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ResetPriority();
            cameraPoints[3].Priority = 10;
            // gameManager.ChangeCameraUI(3);
        }
    }

    private void ResetPriority()
    {
        foreach (var camera in cameraPoints)
        {
            camera.Priority = 0;
        }
    }

    void Update()
    {
        ChangeCamera();
        CameraRay();
    }

    private void CameraRay()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            for (int i = 0; i < lineRenderers.Count; i++)//–¢Žg—p‚ÌlineRenderer‚ðŽæ“¾‚·‚éB
            {
                if (lineRenderers[i].enabled == false)
                {
                    manipulateLineRenderer = lineRenderers[i];
                    manipulateLineRenderer.enabled = true;
                    manipulateLineRenderer.SetPosition(0, Camera.main.transform.position - Vector3.up / 2);

                    StartCoroutine(NonVisibleLine((lineRenderers[i])));
                    break;
                }
            }

            manipulateLineRenderer.SetPosition(1, ray.origin + ray.direction * rayLength);

            if (Physics.Raycast(ray, out hit, rayLength, enemyLayer))
            {
                //Õ“Ëˆ—
                manipulateLineRenderer.SetPosition(1, hit.point);
                //lineRenderer.SetPosition(1, ray.origin + ray.direction * 100);

                var enemy = hit.collider.GetComponent<Enemy>();
                enemy.EnemyBom();
            }
        }
    }

    IEnumerator NonVisibleLine(LineRenderer lineRenderer)
    {
        yield return new WaitForSeconds(nonVisibleTime);
        lineRenderer.enabled = false;

    }
}
