using System.Linq;
using UnityEngine;

public class Rotation : MonoBehaviour, IRotate
{
    [SerializeField] Transform fireAxis;
    [SerializeField] float rotationSpeed;

    [SerializeField] 
    private SpriteRenderer[] spriteRenderers;

    public void Rotate(Transform target)
    {
        if(target == null)
        {
            return;
        }

        Vector3 directionToTarget = target.position - fireAxis.position;

        float angleInRadians = Mathf.Atan2(directionToTarget.y, directionToTarget.x);
        Debug.Log($"angle rads: {angleInRadians}");
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        Debug.Log($"angle: {angleInDegrees}");
        //Quaternion targetRotation = Quaternion.Euler(0f, 0f, angleInDegrees);
        //fireAxis.rotation = Quaternion.RotateTowards(fireAxis.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        ShowTurretBasedOnAngle(angleInDegrees);
    }
    private void ShowTurretBasedOnAngle(float angleInDegrees)
    {
        int turrAnimIdx = 0;
        if (angleInDegrees < 0f && angleInDegrees >= -20f)
        {
            turrAnimIdx = 0;
            
        } else if (angleInDegrees < -20f && angleInDegrees >= -80f)
        {
            turrAnimIdx = 1;
        } else if (angleInDegrees < -80f && angleInDegrees >= -100f)
        {
            turrAnimIdx = 2;

        } else if (angleInDegrees < -100f && angleInDegrees >= -140f)
        {
            turrAnimIdx = 3;

        } else if (angleInDegrees < -140f && angleInDegrees >= -180f)
        {
            turrAnimIdx = 4;
        }
        BaseFiring.SetFirePointIdx(turrAnimIdx);
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
