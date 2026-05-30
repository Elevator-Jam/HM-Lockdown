using UnityEngine;
using VContainer;

public class SelectAbility : MonoBehaviour
{
    private AbilityManager _abilityManager;

    private void Awake() {
        // Ask the root scope to inject into this object
        // Assuming your LifetimeScope is in the scene, it's accessible globally
        var scope = Object.FindAnyObjectByType<GameLifetimeScope>();
        if (scope != null) {
            scope.Container.Inject(this);
        }
    }

    [Inject]
    public void Construct(AbilityManager abilityManager)
    {
        _abilityManager = abilityManager;
    }

    // Sets the selected ability in ability manager to the name of the button
    //  calling this function.
    // This buttons name should be set to whatever the "ability" name is that can
    //  be cast.
    public void OnClick()
    {
        var manager = _abilityManager;
        if (manager != null)
        {
            manager.SetSelectedAbility(this.name);
        }
    }
}
