using UnityEngine;

public class SelectTurret : MonoBehaviour
{
    [SerializeField] GameObject turretPrefab;
    [SerializeField] int turretValue;
    public void OnClick()
    {
        BuildingManager.Instance.SetTurretSelected(turretPrefab);
        BuildingManager.Instance.SetTurretValue(turretValue);
    }
}
