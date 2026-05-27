# Feature Design Document: Flying Enemies

**Status:** Draft  
**Author:** Carlos
**Date:** 2026-05-27  

---

## 1. Overview
### 1.1 Purpose
Introduce a new enemy type that moves through the air to increase gameplay variety and force the player to defend against multi-layered threats. Flying enemies provide a vertical challenge. (could bypass ground-based obstacles in the future)

### 1.2 Scope
*   **In-Scope:**
    *   Creation of FlyingHoodlin class mimicking BaseHoodlin.
    *   Fixed-altitude flight logic (direct X-axis pathing at a specific height).
    *   Integration into the existing Wave/EntityManager system.
    *   Support for high-altitude spawn points.
*   **Out-of-Scope:**
    *   Anti-air specific turrets (all current turrets can hit fliers for now).
    *   Complex aerial avoidance or altitude changes.

---

## 2. User Experience (UX) & Gameplay
### 2.1 Player Interaction
Players interact with flying enemies primarily through turret placement. Fliers are visually distinct and occupy the upper portion of the screen.

### 2.2 Visual & Audio Feedback
*   **Visuals:**
    *   Animations: Idle/Fly loop, Take Damage, and a "Death/Crash" animation where the enemy falls out of the sky.
    *   Assets: [To be determined by Art Department].
*   **Audio:**
    *   Distinct hum or wing flap sound while moving.
    *   Unique screech or mechanical failure sound upon death.

---

## 3. Technical Implementation
### 3.1 Architecture & Class Design
*   **New Class:** FlyingHoodlin
    *   **Inheritance:** MonoBehaviour, IEntity.
*   **Responsibilities:**
    *   Manage health and damage.
    *   Implement specific Move() logic for aerial pathing.

### 3.2 Key Methods & Logic
```csharp
/// Function: Move
/// <summary>
/// Purpose: Moves the entity directly toward the target while maintaining a fixed altitude.
/// </summary>
/// <returns> Nothing </returns>
public void Move() {
    var destination = new Vector2(target.position.x, rb2D.position.y);
    Vector2 pos = Vector2.MoveTowards(rb2D.position, destination, speed * Time.fixedDeltaTime);
    rb2D.MovePosition(pos);
}
```

### 3.3 Dependencies & Integrations
*   **EntityManager:** Requires new Transform points in the SpawnPoints list designated for air units.
*   **HealthManager:** Integrates for damaging the player base.

---

## 4. Data & Configuration
### 4.1 ScriptableObjects
*   **EntitySO:** Use existing EntitySO for health, speed, and damage.

### 4.2 Serialized Fields
*   [SerializeField] private float flightAltitude: The Y-coordinate the enemy should maintain.
*   [SerializeField] private float crashGravityScale: How fast the enemy falls when health reaches zero.

---

## 5. Testing & Validation
### 5.1 Unit Tests
*   **Altitude Consistency:** Verify the enemy does not deviate from its Y-coordinate during movement.
*   **Damage Application:** Ensure HealthManager.Instance.TakeDamage is called correctly.

### 5.2 Manual Verification Steps
1.  Assign a FlyingHoodlin to a wave.
2.  Set a high-altitude spawn point.
3.  Confirm all turret types can target and destroy the flyer.

---

## 6. Style & Standards Compliance
*   [x] **Bracing:** K&R Style (Opening brace on same line).
*   [x] **Naming:** camelCase for private fields, PascalCase for methods.
*   [x] **Documentation:** /// Function/Purpose/Returns headers included.
*   [x] **Access Modifiers:** Explicit private/public modifiers used.

---

## 7. Open Questions / Risks
*   **Art Style:** Should these be mechanical (drones), biological (birds/bats), or magical? (Pending Art Team feedback).
*   **Future Expansion:** Should we implement an IsFlying flag on turrets for future Anti-Air balancing?
