using UnityEngine;
using System.Collections;

public class LockAspectRatio : MonoBehaviour
{
    [Range(1.0f, 20.0f)]
    public float WidthAspect = 16.0f;
    [Range(1.0f, 20.0f)]
    public float HeightAspect = 9.0f;

    private int m_ScreenWidth = 0;
    private int m_ScreenHeight = 0;
    private float m_AspectRatio = 0.0f;

    // Use this for initialization
    void Start()
    {
        Resize();
    }

    void Update()
    {
        if ((m_ScreenHeight != Screen.height) || (m_ScreenWidth != Screen.width) ||
            (m_AspectRatio != (WidthAspect / HeightAspect)))
        {
            Resize();
        }
    }

    private void Resize()
    {
        m_ScreenWidth = Screen.width;
        m_ScreenHeight = Screen.height;
        m_AspectRatio = WidthAspect / HeightAspect;

        float windowAspect = (float)m_ScreenWidth / (float)m_ScreenHeight;

        float scaleHeight = windowAspect / m_AspectRatio;
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;

        if (scaleHeight < 1.0f)
        {
            // Letterbox
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            // Pillarbox
            float scaleWidth = 1.0f / scaleHeight;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
        }

        camera.rect = rect;
    }
}
