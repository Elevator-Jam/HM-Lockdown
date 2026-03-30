using UnityEngine;

public class SelectAbility : MonoBehaviour
{
    // Sets the selected ability in ability manager to the name of the button
    //  calling this function.
    // This buttons name should be set to whatever the "ability" name is that can
    //  be cast.
    public void OnClick()
    {
        AbilityManager.Instance.SetSelectedAbility(this.name);
    }
}
