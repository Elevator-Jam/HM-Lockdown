# C# Code Style Guide - HM Lockdown

**Version:** 1.0.0  
**Created:** 2026-05-27  
**Last Updated:** 2026-05-27  

This style guide outlines the coding standards and conventions for C# development in the **HM Lockdown** project. Adhering to these standards ensures the codebase remains readable, maintainable, and consistent.

---

## 1. Naming Conventions

### 1.1 Classes and Interfaces
* **Classes & Structs**: Use `PascalCase`.
  ```csharp
  public class BaseTurret : MonoBehaviour {
      // ...
  }
  ```
* **Interfaces**: Use `PascalCase` and prefix with `I`.
  ```csharp
  public interface IAbility {
      // ...
  }
  ```

### 1.2 Methods & Properties
* **Methods**: Use `PascalCase`.
  ```csharp
  public void TakeDamage(int amount) {
      // ...
  }
  ```
* **Local Functions**: Use `camelCase`.
  ```csharp
  void spawn(int index) {
      // ...
  }
  ```
* **Properties**: Use `PascalCase`.
  ```csharp
  public int Health { get; private set; }
  ```

### 1.3 Fields & Variables
* **Serialized Fields**: Use `camelCase`.
  ```csharp
  [SerializeField]
  private int scrapValue = 10;
  ```
* **Private / Protected Fields**: Use `camelCase`.
  ```csharp
  private Vector3 startPos;
  private Collider2D myCollider;
  ```
* **Local Variables**: Use `camelCase`.
  ```csharp
  Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
  ```
* **Constants & Static Readonly**: Use `PascalCase` or `UPPER_CASE` depending on context, keeping it readable.
  ```csharp
  private const float MaxRaycastDistance = 100f;
  ```

### 1.4 Enums
* **Enum Name**: Use `PascalCase`.
* **Enum Values**: Use `PascalCase` (exceptions can exist for legacy states like `GameState.not_started`, but new enums should use PascalCase).
  ```csharp
  public enum Side { 
      Both, 
      Left, 
      Right 
  }
  ```

---

## 2. Formatting & Layout

### 2.1 Bracing Style (K&R / OTBS)
* Place the opening brace `{` on the **same line** as the declaration or control statement.
* Place the closing brace `}` on a **new line** at the same indentation level as the header statement.
  ```csharp
  // Correct
  void Start() {
      if (isPlayerControlled) {
          InitializeController();
      }
  }
  ```

### 2.2 Control Statements (Mandatory Braces)
* Braces `{ }` are **mandatory** for all control flow statements (such as `if`, `else`, `for`, `while`, `foreach`).
* **Do not** write bracket-less single-line statements or inline guards.
  ```csharp
  // Correct
  if (other.gameObject.name.Contains("bullet")) {
      return;
  }

  // Incorrect (DO NOT USE)
  if (other.gameObject.name.Contains("bullet")) return;
  ```

### 2.3 Line Length & Wrapping
* Lines should not exceed **100 characters** in length.
* If a statement, expression, or method signature exceeds this limit, wrap it onto a new line.
* A **newline** is required at the end of every source file.

---

## 3. Unity Best Practices & Attributes

### 3.1 Serialization Attributes
* Attributes such as `[SerializeField]` and `[Header]` **must** be placed on their own line with a newline before the field declaration.
  ```csharp
  // Correct
  [SerializeField]
  private float turretLifetime = 15f;

  [Header("Lifespan Settings")]
  [SerializeField]
  private float customDuration = 10f;

  // Incorrect (DO NOT USE)
  [SerializeField] float turretLifetime = 15f;
  ```

### 3.2 Access Modifiers
* Always specify access modifiers explicitly for fields (e.g., `private`, `public`, `protected`) instead of relying on C# defaults.
  ```csharp
  // Correct
  [SerializeField]
  private int damage = 5;

  // Incorrect
  [SerializeField]
  int damage = 5;
  ```

---

## 4. Code Comments & Documentation

### 4.1 Single-Line Comments
* Use double-slashes `//` followed by a space for inline and block comments explaining logic steps.
  ```csharp
  // Auto-detect side based on position relative to House
  ```

### 4.2 Function headers (XML/Structured Comments)
* Use triple-slashes `///` formatting to document methods. The header should describe:
  - **Function**: The method name
  - **Purpose**: A brief description of what the function accomplishes
  - **Returns**: Explanation of the return value
  - **Remarks/Note** (Optional): Extra details, limitations, or suggestions
  ```csharp
  /// Function: Recycle
  /// <summary>
  /// Purpose: Returns scrap to the currency manager
  /// </summary>
  /// <returns> Nothing </returns>
  /// <remarks> Note: References the currency manager instance </remarks>
  void Recycle<T>(T valueScript) {
      // ...
  }
  ```
