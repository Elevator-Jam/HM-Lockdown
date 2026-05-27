using System.Linq;
using UnityEngine;

public class Rotation : MonoBehaviour, IRotate
{
    [SerializeField] Transform fireAxis;
    [SerializeField] float rotationSpeed;

    [SerializeField] 
    private SpriteRenderer[] spriteRenderers;
    private BaseFiring fireScript;

    private void Start()
    {
        fireScript = GetComponent<BaseFiring>();
    }

    public void Rotate(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - fireAxis.position;

        float angleInRadians = Mathf.Atan2(directionToTarget.y, directionToTarget.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        if (spriteRenderers.Count() == 0) {
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angleInDegrees);
            fireAxis.rotation = Quaternion.RotateTowards(fireAxis.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        } else {
            ShowTurretBasedOnAngle(angleInDegrees);
        }
    }
    private void ShowTurretBasedOnAngle(float angleInDegrees)
    {
        float angle = angleInDegrees;
        if (angle < 0) 
        {
            angle += 360;
        }

        int turrAnimIdx = 4; // Default to "Left" (covers 90 to 220 degrees)

        if (angle < 90 || angle >= 340) 
        {
            turrAnimIdx = 0; // Top-Right + Bottom-Right Start
        }
        else if (angle >= 280) 
        {
            turrAnimIdx = 1;          // Bottom-Right Diagonal
        }
        else if (angle >= 260) 
        {
            turrAnimIdx = 2;          // Center Bottom
        }
        else if (angle >= 220) 
        {
            turrAnimIdx = 3;          // Bottom-Left Diagonal
        }

        if (fireScript != null) 
        {
            fireScript.SetFirePointIdx(turrAnimIdx);
        }
        for (int i = 0; i < spriteRenderers.Count(); ++i)
        {
            if (i == turrAnimIdx)
            {
                spriteRenderers[i].enabled = true;
            } else
            {
                spriteRenderers[i].enabled = false;
            }
        }
    }
}
