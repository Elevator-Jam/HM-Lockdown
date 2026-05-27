# Architecture Overview - HM Lockdown

**Version:** 1.0.0  
**Created:** 2026-05-27  
**Last Updated:** 2026-05-27  

This document describes the high-level architecture of the **HM Lockdown** project, detailing the patterns, components, and data flows that govern gameplay.

---

## 1. Core Architectural Pattern
### 1.1 Singleton Managers
The project primarily follows a **Manager-centric architecture** utilizing the **Singleton pattern**. Most global systems inherit from `SingletonConstructor<T>`, ensuring a single point of access and a consistent initialization lifecycle.

*   **Base Class:** `Assets/Scripts/Managers/SingletonConstructor.cs`
*   **Pattern:** Provides global access via `ManagerName.Instance` while ensuring the object is registered during `Awake()`.

---

## 2. Key Components

### 2.1 The Management Layer
*   **GameManager:** The central orchestrator. Manages game states (Preparation, Survival, Win, Lose) and the global round timer.
*   **UIManager:** A stack-based UI controller. Manages `UIPanels` (Menus, HUD, Game Over) and handles transitions.
*   **EntityManager:** Controls enemy spawning and wave logic. Bridges game state transitions into physical enemy placement.
*   **HealthManager:** Tracks the House health. Acts as the primary win/loss condition tracker.
*   **CurrencyManager:** Manages the "Scrap" resource, tracking player income and expenditures.
*   **BuildingManager & AbilityManager:** Specialized controllers for player actions (placing turrets and triggering abilities).

### 2.2 The Entity System
*   **IEntity:** An interface that defines the contract for all mobile units (e.g., `Move()`, `Attack()`, `TakeDamage()`).
*   **BaseHoodlin:** The base ground enemy implementation. Uses Rigidbody2D for movement and interacts with `HealthManager` to damage the player's base.

### 2.3 Gameplay & Building
*   **BaseTurret:** The base class for defensive structures. Handles target detection and automated firing.
*   **BuildSocket:** World-space interaction points where players can spend Scrap to instantiate turrets.

---

## 3. Data Flow & Interactions

### 3.1 The Game Loop
1.  **GameManager** starts the `Preparation` state.
2.  Player interacts with **UI** to select turrets via **BuildingManager**.
3.  Timer expires; **GameManager** switches to `Survival`.
4.  **EntityManager** begins spawning enemies (`BaseHoodlin`) at designated `SpawnPoints`.
5.  Enemies move toward the House; **Turrets** detect and attack them.
6.  If an enemy reaches the House, it calls `HealthManager.Instance.TakeDamage()`.

### 3.2 Resource Cycle
1.  Enemies are destroyed and call `DropScrap()`.
2.  Player clicks on Scrap objects in the world.
3.  **CurrencyManager** updates the global balance.
4.  **UI** updates to reflect the new balance, enabling/disabling turret purchases.
