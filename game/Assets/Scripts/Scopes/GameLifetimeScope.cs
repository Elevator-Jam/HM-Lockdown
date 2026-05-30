using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameLifetimeScope : LifetimeScope
{
    [Header("Managers")]
    [SerializeField]
    private CurrencyManager currencyManager;
    [SerializeField]
    private EntityManager entityManager;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private HealthManager healthManager;
    [SerializeField]
    private AbilityManager abilityManager;
    [SerializeField]
    private BuildingManager buildingManager;

    protected override void Configure(IContainerBuilder builder)
    {
        // Register Managers
        if (currencyManager != null) {
            builder.RegisterComponent(currencyManager);
        }
        if (entityManager != null) {
            builder.RegisterComponent(entityManager);
        }
        if (gameManager != null) {
            builder.RegisterComponent(gameManager);
        }
        if (uiManager != null) {
            builder.RegisterComponent(uiManager);
        }
        if (healthManager != null) {
            builder.RegisterComponent(healthManager);
        }
        if (abilityManager != null) {
            builder.RegisterComponent(abilityManager);
        }
        if (buildingManager != null) {
            builder.RegisterComponent(buildingManager);
        }

        // Register UI Panels
        builder.RegisterComponentInHierarchy<MainMenu>();
        builder.RegisterComponentInHierarchy<PauseMenu>();
        builder.RegisterComponentInHierarchy<PostGameMenu>();
        builder.RegisterComponentInHierarchy<SettingsMenu>();
    }

}
