using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] List<CinemachineVirtualCamera> cameraPoints = new List<CinemachineVirtualCamera>();
    [SerializeField] GameManager gameManager;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] List<LineRenderer> lineRenderers;
    LineRenderer manipulateLineRenderer;

    Ray ray;
    RaycastHit hit;
    float rayLength = 1000f;

    public void ChangeCamera()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ResetPriority();
            SoundManager.Instance.Play(1, 0);
            cameraPoints[0].Priority = 10;
            gameManager?.ChangeCameraUI(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ResetPriority();
            SoundManager.Instance.Play(1, 0);
            cameraPoints[1].Priority = 10;
            gameManager?.ChangeCameraUI(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ResetPriority();
            SoundManager.Instance.Play(1, 0);
            cameraPoints[2].Priority = 10;
            gameManager?.ChangeCameraUI(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ResetPriority();
            SoundManager.Instance.Play(1, 0);
            cameraPoints[3].Priority = 10;
            gameManager?.ChangeCameraUI(3);
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
        if (gameManager.State != GameManager.InGameState.Finish)
        {
            ChangeCamera();
            CameraRay();
        }
    }

    private void CameraRay()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            for (int i = 0; i < lineRenderers.Count; i++)//未使用のlineRendererを取得する。
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
                manipulateLineRenderer.SetPosition(1, hit.point);
                var enemy = hit.collider.GetComponent<Enemy>();
                enemy.EnemyBom();
            }
        }
    }

    IEnumerator NonVisibleLine(LineRenderer lineRenderer)
    {
        float alpha1 = 1f;
        float alpha2 = 1f;

        Gradient gradient = new Gradient();

        for (float i = 1; i > 0; i -= 0.1f)
        {
            alpha1 = i;
            alpha2 = i * 1.2f;
            gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0), new GradientColorKey(Color.white, 1) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha1, 0), new GradientAlphaKey(alpha2, 1) });
            lineRenderer.colorGradient = gradient;
            yield return new WaitForSeconds(.1f);
        }
        lineRenderer.enabled = false;
    }
}
