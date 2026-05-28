# Feature Design Document: Game Feel Library

**Status:** Draft
**Author:** Taro Omiya
**Date:** 2026-05-27

---

## 1. Overview
### 1.1 Purpose
The game feel library provides the tools for game designers and engineers to define
1. list of events, and
2. actions that are triggered when those events run.

The primary goal is to allow game designers to quickly create a list of game feel triggers, preview their work, and integrate those triggers into the game with minimal engineering contributions (which is to say, to make the engineer less of a bottleneck).

The list of potential game-feel-related actions that can be triggered are collaboratively defined by game designers and engineers.  Some actions include:
* screen shakes
* screen flashes
* play animation
* etc.

### 1.2 Scope
*   **In-Scope:**
    * `ScriptableObjects` that serves as defining the event
        * Maybe add editor for defining arguments to the event?
    * Editor preview of the game feel actions
    * List of game feel actions, including:
        * positional screen-shake
        * rotational screen-shake
        * play one-time particle effect
        * play one-time sound effect
        * hit-pause effect
        * activate animator's trigger
*   **Out-of-Scope:**
    * Other global trigger/actions

---

## 2. User Experience (UX) & Gameplay
### 2.1 Player Interaction
Game feel actions trigger when events are triggered, e.g. an enemy was shot, enemy hit the base, etc.

### 2.2 Visual & Audio Feedback
This feature is mostly engineering-focused.
*   **Visuals:**
    * camera animations triggered by scripts
    * particle effects triggered by scripts
    * animations triggered by scripts
*   **Audio:**
    * Sound effects triggered by scripts

---

## 3. Technical Implementation
### 3.1 Architecture & Class Design
To be determined, based on the list of requirements from game designers.  Below assumes the game designers desire using the Visual Scripting package to trigger game-feel events
*   **New Classes:**
    * `GameFeelManager` - Singleton to handle full-screen effects, e.g. screenshakes
        * **Inheritance:** `SingletonConstructor<GameFeelManager>`
    * `GameFeelEvent` - defines a game feel event to trigger, as well as arguments to feed into the triggers.
        * **Inheritance:** `ScriptableObject`
    * `GameFeelArgs` - a dictionary of arguments for `GameFeelEvent` to process
    * `GameFeelEventUnit` - custom Visual Scripting event node triggered by `GameFeelEvent`.  Always a coroutine
        * **Inheritance:** `EventUnit<GameFeelArgs>`
    * `GameFeelUnit` - abstract Visual Scripting node that defines a specific game feel action
        * **Inheritance:** `Unit`
    * `WaitForSecondsUnit` - coroutine Visual Scripting node that delays the next action by a certain duration
        * **Inheritance:** `Unit`

### 3.2 Key Methods & Logic
Outline the critical logic paths.
```csharp
[SerializedField]
private GameFeelEvent onDamageEffect;

public void PlayDamageEffect() {
    // Fill out the arguments, here
    var args = new GameFeelArgs();
    args.Add<AudioSource>("SoundEffect", GetComponent<AudioSource>());

    // Trigger the effect
    onDamageEffect.Invoke(args);
}
```

### 3.3 Dependencies & Integrations
*   **Existing Systems:** TBD
*   **External Assets:** TBD

---

## 4. Data & Configuration
### 4.1 ScriptableObjects
On `GameFeelEvent`, contains editable field, `GameFeelArgs`.

### 4.2 Serialized Fields
On `GameFeelArgs`, the `Arguments` field will be a list of name-and-object-type field.

---

## 5. Testing & Validation
### 5.1 Unit Tests
TBD

### 5.2 Manual Verification Steps
TBD
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
    * How should the actions be implemented?  A simple list of actions that are ran sequentially?  Through Visual Scripting graph?
    * What are the arguments that we should implement to
*   Potential performance impacts (e.g., high GC allocation, physics overhead)?
    * slightly higher GC allocation