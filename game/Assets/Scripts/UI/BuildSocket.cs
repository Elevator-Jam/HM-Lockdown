using UnityEngine;

public class BuildSocket : MonoBehaviour
{
    [SerializeField] bool isOccupied;
    [SerializeField] GameObject OccupiedBy;
    [SerializeField] string acceptedTag;

    bool CanAccept(GameObject placeable)
    {
        if (isOccupied)
        {
            return false;
        }
        if (placeable.tag != acceptedTag || placeable == null)
        {
            return false;
        }
        return true;
    }
    public void Occupy()
    {
        if(CanAccept(BuildingManager.Instance.GetTurretSelected()))
        {
            GameObject turret = BuildingManager.Instance.GetTurretSelected();
            int value = BuildingManager.Instance.GetTurretValue();
            CurrencyManager.Instance.SubtractScrap(value);
            Instantiate(turret, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }

    public void OnClick() => Occupy();
}
