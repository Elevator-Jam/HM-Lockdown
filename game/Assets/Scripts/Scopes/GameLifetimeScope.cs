using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private EntityManager entityManager;

    protected override void Configure(IContainerBuilder builder)
    {
        // Register the existing CurrencyManager component from the scene
        if (currencyManager != null)
        {
            builder.RegisterComponent(currencyManager);
        }

        // Register the existing EntityManager component from the scene
        if (entityManager != null)
        {
            builder.RegisterComponent(entityManager);
        }
    }
}
