using UnityEngine;

public class FpsDisplay : MonoBehaviour
{
    private float m_Fps;
    
    private int m_LastFrameCount;

    private float m_LastCalcTime;

    private const float m_RecalculateDelta = 1;

    void Update()
    {
        var t = Time.realtimeSinceStartup - m_LastCalcTime;
        if (t <= m_RecalculateDelta)
        {
            return;
        }

        m_Fps = (Time.frameCount - m_LastFrameCount) / t;
        m_LastFrameCount = Time.frameCount;
        m_LastCalcTime = Time.realtimeSinceStartup;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperCenter;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.green;
        string text = $"FPS: {m_Fps}";
        GUI.Label(rect, text, style);
    }
}