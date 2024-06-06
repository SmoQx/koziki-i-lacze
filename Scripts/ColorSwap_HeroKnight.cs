﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class ColorSwap_HeroKnight : MonoBehaviour
{
    [SerializeField] Color[] m_sourceColors;
    [SerializeField] Color[] m_newColors;

    Texture2D m_colorSwapTex;
    Color[] m_spriteColors;
    SpriteRenderer m_spriteRenderer;
    bool m_init = false;

    void Awake()
    {
        Debug.Log("Awake called");
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        InitColorSwapTex();

        SwapDemoColors();
    }

    private void OnValidate()
    {
        if (m_init)
        {
            SwapDemoColors();
        }
    }

    public void SwapDemoColors()
    {
        Debug.Log("Swapping demo colors");
        for (int i = 0; i < m_sourceColors.Length && i < m_newColors.Length; i++)
        {
            Debug.Log($"Swapping color {m_sourceColors[i]} to {m_newColors[i]}");
            SwapColor((int)(m_sourceColors[i].r * 255.0f), m_newColors[i]);
        }
        if (m_colorSwapTex)
            m_colorSwapTex.Apply();
    }

    public static Color ColorFromInt(int c, float alpha = 1.0f)
    {
        int r = (c >> 16) & 0x000000FF;
        int g = (c >> 8) & 0x000000FF;
        int b = c & 0x000000FF;

        Color ret = ColorFromIntRGB(r, g, b);
        ret.a = alpha;

        return ret;
    }

    public static Color ColorFromIntRGB(int r, int g, int b)
    {
        return new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);
    }

    public void InitColorSwapTex()
    {
        Debug.Log("Initializing color swap texture");
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();
        Debug.Log("Color swap texture initialized");

        m_spriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

        m_spriteColors = new Color[colorSwapTex.width];
        m_colorSwapTex = colorSwapTex;
        m_init = true;
    }

    public void SwapColor(int index, Color color)
    {
        if (index >= 0 && index < 256)
        {
            m_spriteColors[index] = color;
            m_colorSwapTex.SetPixel(index, 0, color);
        }
    }

    public void SwapColors(List<int> indexes, List<Color> colors)
    {
        for (int i = 0; i < indexes.Count; ++i)
        {
            m_spriteColors[indexes[i]] = colors[i];
            m_colorSwapTex.SetPixel(indexes[i], 0, colors[i]);
        }
        m_colorSwapTex.Apply();
    }

    public void ClearColor(int index)
    {
        Color c = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        m_spriteColors[index] = c;
        m_colorSwapTex.SetPixel(index, 0, c);
    }

    public void SwapAllSpritesColorsTemporarily(Color color)
    {
        for (int i = 0; i < m_colorSwapTex.width; ++i)
            m_colorSwapTex.SetPixel(i, 0, color);
        m_colorSwapTex.Apply();
    }

    public void ResetAllSpritesColors()
    {
        for (int i = 0; i < m_colorSwapTex.width; ++i)
            m_colorSwapTex.SetPixel(i, 0, m_spriteColors[i]);
        m_colorSwapTex.Apply();
    }

    public void ClearAllSpritesColors()
    {
        for (int i = 0; i < m_colorSwapTex.width; ++i)
        {
            m_colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
            m_spriteColors[i] = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        m_colorSwapTex.Apply();
    }
}
