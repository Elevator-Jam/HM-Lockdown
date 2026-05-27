# Feature Design Document: [Feature Name]

**Version:** 1.0.0  
**Created:** 2026-05-27  
**Last Updated:** 2026-05-27  

**Status:** Draft / Review / Approved  
**Author:** [Your Name]  
**Date:** [YYYY-MM-DD]  

---

## 1. Overview
### 1.1 Purpose
Describe what this feature is and why it is being added to the game. What problem does it solve or what value does it add to the player experience?

### 1.2 Scope
*   **In-Scope:** List the specific functionalities that will be implemented.
*   **Out-of-Scope:** List related items that will NOT be handled in this specific task to prevent scope creep.

---

## 2. User Experience (UX) & Gameplay
### 2.1 Player Interaction
How does the player trigger or interact with this feature? (e.g., UI buttons, keybinds, proximity triggers).

### 2.2 Visual & Audio Feedback
*   **Visuals:** Animations, VFX, UI changes, or color shifts.
*   **Audio:** Sound effects or music changes associated with the feature.

---

## 3. Technical Implementation
### 3.1 Architecture & Class Design
Describe the new classes or structures needed.
*   **New Classes:** List class names and their primary responsibilities.
*   **Inheritance:** Will these inherit from \`MonoBehaviour\`, \`ScriptableObject\`, or existing base classes (e.g., \`BaseTurret\`)?

### 3.2 Key Methods & Logic
Outline the critical logic paths.
\`\`\`csharp
// Example pseudo-code for a core method
public void ProcessFeature() {
    // 1. Check conditions
    // 2. Update state
    // 3. Trigger feedback
}
\`\`\`

### 3.3 Dependencies & Integrations
*   **Existing Systems:** Which current managers (e.g., \`CurrencyManager\`, \`WaveManager\`, \`AbilityManager\`) will this interact with?
*   **External Assets:** List any ThirdParty plugins or specific Unity packages required.

---

## 4. Data & Configuration
### 4.1 ScriptableObjects
Will this feature use \`ScriptableObjects\` for data-driven configuration? Define the data fields.

### 4.2 Serialized Fields
List the key parameters that designers will need to tweak in the Unity Inspector.

---

## 5. Testing & Validation
### 5.1 Unit Tests
List specific logic units that can be tested in isolation via PlayMode or EditMode tests.

### 5.2 Manual Verification Steps
1.  Step 1...
2.  Step 2...

---

## 6. Style & Standards Compliance
*   [ ] **Bracing:** K&R Style (Opening brace on same line).
*   [ ] **Naming:** \`camelCase\` for private fields, \`PascalCase\` for methods.
*   [ ] **Documentation:** \`/// Function/Purpose/Returns\` headers included.
*   [ ] **Access Modifiers:** All fields have explicit \`private\`/\`public\` modifiers.

---

## 7. Open Questions / Risks
*   Are there any technical unknowns?
*   Potential performance impacts (e.g., high GC allocation, physics overhead)?
