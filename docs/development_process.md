# Agile Development & Agentic Engineering Process - HM Lockdown

This document outlines the end-to-end workflow for feature development in the **HM Lockdown** project, integrating traditional Agile methodologies with **Agentic Engineering** principles.

---

## 1. Feature Initiation & Design Phase
### 1.1 The Feature Design Document (FDD)
Every feature begins with a **Feature Design Document** (see `docs/feature_design_template.md`). This document is a collaborative effort between Design, Art, and Engineering.
*   **Perspective:** The doc must address gameplay, visual requirements, and technical architecture.
*   **Review:** A strong focus is placed on engineering and architectural reviews to ensure long-term codebase health.
*   **Approval:** Design, Art, and Engineering must all agree on the FDD before development begins.

### 1.2 Agentic Engineering Integration
Following the principles of [Agentic Engineering](https://addyosmani.com/blog/agentic-engineering/), we utilize AI agents to:
*   Assist in drafting FDDs based on conversational requirements.
*   Perform code style audits and architectural analysis.
*   Scaffold boilerplates and implement surgical code changes.
*   Automate repetitive tasks like license headers or test generation.

---

## 2. Sprint & Development Phase
### 2.1 Task Distribution
Features are divided among participants (Designers, Artists, Tech Artists, Engineers) to facilitate parallel work.

### 2.2 Branching & Coding
*   **Feature Branches:** All work for a specific feature occurs in a dedicated branch created for that feature.
*   **Pair Programming:** Highly encouraged to improve code quality and knowledge sharing.
*   **Agent Collaboration:** Engineers use AI agents as "pair programmers" for research, implementation, and real-time debugging.

### 2.3 Asset & UI Integration
*   **Artists:** Provide finalized assets or mocks.
*   **Designers:** Provide UI mocks and layouts.
*   **Tech Artists:** Act as the bridge, integrating assets and UI directly into Unity. If only mocks are available, placeholders are used to unblock Engineering.

---

## 3. Validation & Quality Assurance
### 3.1 Multi-Disciplinary Testing
Once the feature is integrated, all participants (not just the author) must test it.
*   **Play Mode:** Verify gameplay feel and logic.
*   **Builds:** Perform platform-specific builds (e.g., iOS/Android) to catch environment-specific bugs.
*   **Regression Check:** Ensure no existing features are broken.

### 3.2 Documentation & Evidence
Capture the feature in action via **videos or screenshots or builds** to provide context for reviewers.

---

## 4. Code Review & Merge Process
### 4.1 Pull Requests (PR)
*   Create a PR from the feature branch to the `Development` branch.
*   Include the FDD link, testing evidence (videos/screenshots), and any known issues.

### 4.2 Review Protocol
*   At least **one person** must code review the PR and give it a LGTM (looks good to me meaning I reviewed the code and understand it as well as the person who wrote it and I think it should go in) or 2 people each could give a +1 (it looks good at glance I didn't do a super deep review but nothing wrong pops up) each for a PR to be able to be merged.
*   Iterate on feedback until the reviewer is satisfied and the code is deemed safe for merge.

---

## 5. Sprint Conclusion
### 5.1 Retrospective
At the end of every sprint, hold a meeting to discuss:
*   **What went well?**
*   **What could have gone better?**
*   **How can we improve our process or Agent usage?**

### 5.2 Repeat
Adjust the process based on feedback and start the next sprint.
