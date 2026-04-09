using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class AspectRatioController : MonoBehaviour
{
    private float targetAspect = 20.0f / 9.0f;
    private RectTransform topBar, bottomBar, leftBar, rightBar;
    private Canvas barCanvas;

    void Start()
    {
        CreateBlackBars();
        ApplyAspectRatio();
    }

    void CreateBlackBars()
    {
        // Create a new Canvas for the black bars
        GameObject canvasObj = new GameObject("BlackBarsCanvas");
        barCanvas = canvasObj.AddComponent<Canvas>();
        barCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        barCanvas.sortingOrder = 9999; // Ensure it's on top of everything
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        // Create the four possible bars
        topBar = CreateBar("TopBar", canvasObj.transform);
        bottomBar = CreateBar("BottomBar", canvasObj.transform);
        leftBar = CreateBar("LeftBar", canvasObj.transform);
        rightBar = CreateBar("RightBar", canvasObj.transform);
    }

    RectTransform CreateBar(string name, Transform parent)
    {
        GameObject bar = new GameObject(name);
        bar.transform.SetParent(parent);
        Image img = bar.AddComponent<Image>();
        img.color = Color.black;
        return bar.GetComponent<RectTransform>();
    }

    public void ApplyAspectRatio()
    {
        if (topBar == null) return;

        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        // Reset all bars first
        ResetBar(topBar); ResetBar(bottomBar); ResetBar(leftBar); ResetBar(rightBar);

        if (scaleHeight < 1.0f) // Letterbox (Bars on Top/Bottom)
        {
            float barHeight = (1.0f - scaleHeight) / 2.0f;

            // Top Bar
            topBar.anchorMin = new Vector2(0, 1 - barHeight);
            topBar.anchorMax = new Vector2(1, 1);
            topBar.offsetMin = topBar.offsetMax = Vector2.zero;

            // Bottom Bar
            bottomBar.anchorMin = Vector2.zero;
            bottomBar.anchorMax = new Vector2(1, barHeight);
            bottomBar.offsetMin = bottomBar.offsetMax = Vector2.zero;
        }
        else // Pillarbox (Bars on Left/Right)
        {
            float scaleWidth = 1.0f / scaleHeight;
            float barWidth = (1.0f - scaleWidth) / 2.0f;

            // Left Bar
            leftBar.anchorMin = Vector2.zero;
            leftBar.anchorMax = new Vector2(barWidth, 1);
            leftBar.offsetMin = leftBar.offsetMax = Vector2.zero;

            // Right Bar
            rightBar.anchorMin = new Vector2(1 - barWidth, 0);
            rightBar.anchorMax = Vector2.one;
            rightBar.offsetMin = rightBar.offsetMax = Vector2.zero;
        }
    }

    void ResetBar(RectTransform bar)
    {
        bar.anchorMin = Vector2.zero;
        bar.anchorMax = Vector2.zero;
        bar.offsetMin = Vector2.zero;
        bar.offsetMax = Vector2.zero;
    }

#if UNITY_EDITOR
    void Update()
    {
        ApplyAspectRatio();
    }
#endif
}
