using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeGraphics : MonoBehaviour
{
    public UniversalRenderPipelineAsset highQualitySettings;
    public UniversalRenderPipelineAsset lowQualitySettings;

    private bool isHighQuality = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ToggleGraphics();
        }
    }

    void ToggleGraphics()
    {
        if (isHighQuality)
        {
            QualitySettings.SetQualityLevel(0, true);
            GraphicsSettings.defaultRenderPipeline = lowQualitySettings; // Switch to low quality URP asset
            isHighQuality = false;
        }
        else
        {
            QualitySettings.SetQualityLevel(1, true);
            GraphicsSettings.defaultRenderPipeline = highQualitySettings; // Switch to high quality URP asset
            isHighQuality = true;
        }
    }
}
