using UnityEngine;

public class Rotation : MonoBehaviour, IRotate
{
    [SerializeField] Transform fireAxis;
    [SerializeField] float rotationSpeed;

    public void Rotate(Transform target)
    {
        if(target == null)
        {
            return;
        }

        Vector3 directionToTarget = target.position - fireAxis.position;

        float angleInRadians = Mathf.Atan2(directionToTarget.y, directionToTarget.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angleInDegrees);

        fireAxis.rotation = Quaternion.RotateTowards(fireAxis.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
