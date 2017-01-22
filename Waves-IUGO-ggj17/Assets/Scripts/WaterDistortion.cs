using System;
using UnityEngine;

// This class implements simple ghosting type Motion Blur.
// If Extra Blur is selected, the scene will allways be a little blurred,
// as it is scaled to a smaller resolution.
// The effect works by accumulating the previous frames in an accumulation
// texture.
namespace UnityStandardAssets.ImageEffects
{
  [RequireComponent(typeof(Camera))]
  public class WaterDistortion : MonoBehaviour
  {
    [Range(0.1f, 4.0f)]
    public float _Speed = 0.5f;

    [Range(0.1f, 20.0f)]
    public float _Distortion = 5.0f;

    [Range(1.0f, 100.0f)]
    public float _Waves = 20.0f;

    /// Provides a shader property that is set in the inspector
    /// and a material instantiated from the shader
    public Shader shader;

    private Material material;

    protected virtual void Start()
    {
      material = new Material(shader);

      // Disable if we don't support image effects
      if (!SystemInfo.supportsImageEffects)
      {
        enabled = false;
        return;
      }

      // Disable the image effect if the shader can't
      // run on the users graphics card
      if (!shader || !shader.isSupported)
        enabled = false;
    }

    void OnDisable()
    {
      if (material)
      {
        DestroyImmediate(material);
      }
    }

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
      material.SetFloat("_Speed", _Speed);      
      material.SetFloat("_Distortion", _Distortion);
      material.SetFloat("_Waves", _Waves);

      Graphics.Blit(source, destination, material);
    }
  }
}
