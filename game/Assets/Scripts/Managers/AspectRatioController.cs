using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class AspectRatioController : MonoBehaviour
{
    private float targetAspect = 20.0f / 9.0f;
    private RectTransform topBar, bottomBar, leftBar, rightBar;
    private Canvas barCanvas;
    private Camera cam;

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        cam = GetComponent<Camera>();
        
        // Force Camera Properties for consistent World Space
        cam.orthographic = true;
        cam.orthographicSize = 5f;
        cam.nearClipPlane = 0.3f;
        cam.farClipPlane = 1000f;

        CreateBlackBars();
        ApplyAspectRatio();
        
        if (Application.isPlaying)
        {
            ConfigureAllCanvases();
        }
    }

    [ContextMenu("Master All Canvases")]
    public void ConfigureAllCanvases()
    {
        if (cam == null) cam = GetComponent<Camera>();

        // Find every Canvas in the scene
        Canvas[] allCanvases = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        int count = 0;

        foreach (Canvas c in allCanvases)
        {
            // SKIP: Black bar canvas OR any canvas already intended to be in World Space
            if (c == barCanvas || c.name == "BlackBarsCanvas") continue;
            if (c.renderMode == RenderMode.WorldSpace)
            {
                Debug.Log($"[AspectRatioController] Skipping WorldSpace Canvas: {c.name}");
                continue;
            }

            // Mark for Undo/Save in Editor
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(c, "Master UI Scale");
#endif
            c.renderMode = RenderMode.ScreenSpaceCamera;
            c.worldCamera = cam;
            c.planeDistance = 1;

            CanvasScaler scaler = c.GetComponent<CanvasScaler>();
            if (scaler == null) 
            {
#if UNITY_EDITOR
                scaler = UnityEditor.Undo.AddComponent<CanvasScaler>(c.gameObject);
#else
                scaler = c.gameObject.AddComponent<CanvasScaler>();
#endif
            }

#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(scaler, "Master UI Scale");
#endif
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(2400, 1080);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 1.0f; // Match Height for reliable landscape scaling

            count++;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(c);
            if (scaler != null) UnityEditor.EditorUtility.SetDirty(scaler);
#endif
        }

        Debug.Log($"[AspectRatioController] Mastered {count} Canvases for 20:9 Frame.");
    }

    void CreateBlackBars()
    {
        // Try to find existing bar canvas first to avoid duplicates
        GameObject canvasObj = GameObject.Find("BlackBarsCanvas");
        
        if (canvasObj == null)
        {
            canvasObj = new GameObject("BlackBarsCanvas");
        }

        barCanvas = canvasObj.GetComponent<Canvas>();
        if (barCanvas == null) barCanvas = canvasObj.AddComponent<Canvas>();
        
        barCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        barCanvas.sortingOrder = 9999;
        
        CanvasScaler scaler = canvasObj.GetComponent<CanvasScaler>();
        if (scaler == null) scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        
        if (canvasObj.GetComponent<GraphicRaycaster>() == null)
            canvasObj.AddComponent<GraphicRaycaster>();

        bottomBar = CreateBarIfMissing("BottomBar", canvasObj.transform, ref bottomBar);
        topBar = CreateBarIfMissing("TopBar", canvasObj.transform, ref topBar);
        leftBar = CreateBarIfMissing("LeftBar", canvasObj.transform, ref leftBar);
        rightBar = CreateBarIfMissing("RightBar", canvasObj.transform, ref rightBar);
        
        // Ensure they are correctly positioned immediately
        ApplyAspectRatio();
    }

    RectTransform CreateBarIfMissing(string name, Transform parent, ref RectTransform existing)
    {
        Transform t = parent.Find(name);
        if (t != null) 
        {
            existing = t.GetComponent<RectTransform>();
            return existing;
        }

        GameObject bar = new GameObject(name);
        bar.transform.SetParent(parent);
        Image img = bar.AddComponent<Image>();
        img.color = Color.black;
        existing = bar.GetComponent<RectTransform>();
        return existing;
    }

    public void ApplyAspectRatio()
    {
        if (topBar == null || bottomBar == null) return;

        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        ResetBar(topBar); ResetBar(bottomBar); ResetBar(leftBar); ResetBar(rightBar);

        if (scaleHeight < 1.0f) 
        {
            float barHeight = (1.0f - scaleHeight) / 2.0f;
            SetAnchor(topBar, new Vector2(0, 1 - barHeight), new Vector2(1, 1));
            SetAnchor(bottomBar, Vector2.zero, new Vector2(1, barHeight));
        }
        else 
        {
            float scaleWidth = 1.0f / scaleHeight;
            float barWidth = (1.0f - scaleWidth) / 2.0f;
            SetAnchor(leftBar, Vector2.zero, new Vector2(barWidth, 1));
            SetAnchor(rightBar, new Vector2(1 - barWidth, 0), Vector2.one);
        }
    }

    void SetAnchor(RectTransform rt, Vector2 min, Vector2 max)
    {
        if (rt == null) return;
        rt.anchorMin = min;
        rt.anchorMax = max;
        rt.offsetMin = rt.offsetMax = Vector2.zero;
    }

    void ResetBar(RectTransform bar)
    {
        if (bar == null) return;
        bar.anchorMin = bar.anchorMax = Vector2.zero;
        bar.offsetMin = bar.offsetMax = Vector2.zero;
    }

#if UNITY_EDITOR
    void Update()
    {
        if (cam == null) cam = GetComponent<Camera>();
        if (topBar == null) CreateBlackBars();
        
        ApplyAspectRatio();
        
        if (!Application.isPlaying)
        {
            cam.orthographicSize = 5f;
        }
    }
#endif
}
