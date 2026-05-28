# Project Map (Mermaid Chart) - HM Lockdown

**Version:** 1.0.0  
**Created:** 2026-05-27  
**Last Updated:** 2026-05-27  

This document provides a visual representation of the project architecture and the relationships between key systems.

---

## System Dependency Diagram

```mermaid
graph TD
    %% Managers Layer
    subgraph Managers
        GM[GameManager]
        UM[UIManager]
        EM[EntityManager]
        HM[HealthManager]
        CM[CurrencyManager]
        BM[BuildingManager]
        AM[AbilityManager]
    end

    %% UI Layer
    subgraph UI
        HUD[Game HUD]
        Menus[UIPanels / Menus]
        ST[SelectTurret UI]
        SA[SelectAbility UI]
    end

    %% Gameplay Layer
    subgraph Gameplay
        Entities[Enemies / BaseHoodlin]
        Turrets[Turrets / BaseTurret]
        Sockets[BuildSocket]
        Scrap[Scrap Resource]
    end

    %% Relationships
    GM -->|Controls State| EM
    GM -->|Triggers UI| UM
    EM -->|Spawns| Entities
    Entities -->|Attacks| HM
    Entities -->|Drops| Scrap
    Scrap -->|Updates| CM
    
    UM -->|Manages| Menus
    UM -->|Displays| HUD
    
    ST -->|Sets Selection| BM
    SA -->|Sets Selection| AM
    
    BM -->|Coordinates| Sockets
    Sockets -->|Spends Scrap via| CM
    Sockets -->|Instantiates| Turrets
    
    Turrets -->|Targets| Entities
    HM -->|HP=0 -> Lose State| GM
```

---

## Pattern Notes
*   **Centralization:** All arrows pointing to Managers represent a `Singleton.Instance` call.
*   **Event-Driven:** Game state transitions in the `GameManager` trigger reactions in other managers to avoid tight coupling where possible.
*   **Entity Logic:** Enemies are autonomous but rely on global managers (`HealthManager`, `CurrencyManager`) to report their impact on the game world.
