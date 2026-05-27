# Google Forms Setup Guide - HM Lockdown Bug Intake

This guide provides the exact structure and field configuration to set up a Google Form for **HM Lockdown** playtesting. Using Google Forms makes it easy for interns to submit bug reports from any device, which you can then export to Markdown or review in Google Sheets.

---

## Part 1: Form Questions & Configurations

Create a new Google Form and add the following questions in order. We recommend using **Sections** to group related questions for a better user experience.

### Section 1: General Information
*Gather context about the tester and the environment.*

1. **Tester Name**
   - **Type:** Short Answer
   - **Required:** Yes

2. **Date**
   - **Type:** Date
   - **Required:** Yes (Default to current date)

3. **Build Version**
   - **Type:** Multiple Choice (or Dropdown)
   - **Required:** Yes
   - **Options:**
     - `Unity Editor`
     - `Android`
     - `iOS`
     - *Add write-in option:* `Other`

4. **Device / Platform**
   - **Type:** Short Answer
   - **Required:** Yes
   - **Description:** *e.g., MacBook Pro M2, iPhone 15 Pro, iPad Air*

5. **OS Version**
   - **Type:** Short Answer
   - **Required:** Yes
   - **Description:** *e.g., macOS Sonoma 14.4, iOS 17.2*

---

### Section 2: Bug Details
*Identify where and what the issue is.*

6. **Bug Title / Short Summary**
   - **Type:** Short Answer
   - **Required:** Yes
   - **Description:** *A brief, one-sentence description of the issue.*

7. **Game State / Phase**
   - **Type:** Checkboxes (Multiple Select)
   - **Required:** Yes
   - **Options:**
     - `Main Menu`
     - `Preparation Phase (Nighttime - Building / Upgrading)`
     - `Survival Phase (Daytime - Wave Active / Enemies Spawning)`
     - `Pause Menu`
     - `Post-Game Screen (Victory / Lose)`

8. **Affected Game System**
   - **Type:** Checkboxes (Multiple Select)
   - **Required:** Yes
   - **Options:**
     - `Player Controls / Movement / Inputs`
     - `Scrap / Currency Collection`
     - `Turret Building / Placement`
     - `Turret Firing / Rotation / Targeting`
     - `Hoodlin Enemy AI / Movement / Ground Detection`
     - `Air Strike Ability / Enemy Detection / Payload`
     - `UI & HUD (Timer Slider, Health Slider, Wave Tracker)`
     - `Aspect Ratio Scaling (AspectRatioController)`
     - `Audio / SFX / Music`
     - `Level Progression / Scene Restart`
     - `Other / Performance (Lag, low FPS, crash)`

9. **Severity**
   - **Type:** Multiple Choice (Single Select)
   - **Required:** Yes
   - **Options:**
     - `🔴 Critical (Game crashes, freeze, cannot progress)`
     - `🟠 Major (Major feature broken like cannot place turrets, but game runs)`
     - `🟡 Minor (Small visual bug, clipping sprites, minor UI alignment)`
     - `🔵 Tweak/Feedback (Suggestion for improvement/balancing)`

---

### Section 3: Reproduction & Evidence
*Get the step-by-step instructions to debug the issue.*

10. **Steps to Reproduce**
    - **Type:** Paragraph
    - **Required:** Yes
    - **Description:** *How did you make this bug happen? Provide numbered steps.*

11. **Expected Result**
    - **Type:** Paragraph
    - **Required:** Yes
    - **Description:** *What did you expect to happen?*

12. **Actual Result**
    - **Type:** Paragraph
    - **Required:** Yes
    - **Description:** *What actually happened?*

13. **Reproducibility**
    - **Type:** Multiple Choice (Single Select)
    - **Required:** Yes
    - **Options:**
       - `Always (100%)`
       - `Sometimes (50%)`
       - `Rarely (Hard to reproduce)`
       - `Once (Couldn't make it happen again)`

14. **Evidence (Screenshots / Video)**
    - **Type:** File Upload
    - **Required:** No
    - **Configuration:** Allow any file type, Max number of files: 5, Max file size: 100 MB.
    - *Note: Files will be uploaded to the owner's Google Drive.*

---

## Part 2: Working with the Responses

### 1. Link to Google Sheets
Once the form is set up, click on the **Responses** tab at the top and select **Link to Sheets** (or click the green Google Sheets icon). This will create a spreadsheet that updates in real-time as interns submit bugs.

### 2. Auto-Generating Markdown reports from Google Sheets
You can add a custom formula to your Google Sheet to automatically convert the responses into the formatted Markdown of `bug_intake_form.md`.

In the Google Sheet containing your responses, create a new sheet (tab) called **Markdown Generator**. Use a formula in cell `A2` referencing the main responses sheet (e.g., `Form Responses 1`):

```excel
=ARRAYFORMULA(
  "# Bug Intake Form - HM Lockdown" & CHAR(10) & CHAR(10) &
  "## 1. General Information" & CHAR(10) &
  "* **Tester Name:** " & 'Form Responses 1'!B2:B & CHAR(10) &
  "* **Date:** " & TEXT('Form Responses 1'!C2:C, "yyyy-mm-dd") & CHAR(10) &
  "* **Build Version:** " & 'Form Responses 1'!D2:D & CHAR(10) &
  "* **Device / Platform:** " & 'Form Responses 1'!E2:E & CHAR(10) &
  "* **OS Version:** " & 'Form Responses 1'!F2:F & CHAR(10) & CHAR(10) &
  "## 2. Bug Details" & CHAR(10) &
  "* **Bug Title / Short Summary:** " & 'Form Responses 1'!G2:G & CHAR(10) &
  "* **Game State / Phase:** " & 'Form Responses 1'!H2:H & CHAR(10) &
  "* **Affected Game System:** " & 'Form Responses 1'!I2:I & CHAR(10) &
  "* **Severity:** " & 'Form Responses 1'!J2:J & CHAR(10) & CHAR(10) &
  "## 3. Reproduction" & CHAR(10) &
  "* **Steps to Reproduce:**" & CHAR(10) & 'Form Responses 1'!K2:K & CHAR(10) & CHAR(10) &
  "* **Expected Result:** " & 'Form Responses 1'!L2:L & CHAR(10) &
  "* **Actual Result:** " & 'Form Responses 1'!M2:M & CHAR(10) &
  "* **Reproducibility:** " & 'Form Responses 1'!N2:N & CHAR(10) & CHAR(10) &
  "## 4. Evidence" & CHAR(10) &
  "* **Screenshots / Video Links:** " & 'Form Responses 1'!O2:O & CHAR(10) &
  "---"
)
```
*(Adjust the column letters B, C, D... based on the exact columns in your responses sheet.)*

With this sheet active, any new row will automatically generate a clean Markdown report that you or your interns can copy-paste straight into GitHub issues or save as `.md` files!
