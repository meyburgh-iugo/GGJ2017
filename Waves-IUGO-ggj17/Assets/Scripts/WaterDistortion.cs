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
    [Range(0.1f, 1.0f)]
    public float WaveLength = 0.8f;

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
      //clampo wave size
      WaveLength = Mathf.Clamp(WaveLength, 0.1f, 1.0f);

      // Setup the texture and floating point values in the shader

      // Render the image using the water shader
      //RenderTexture savedTex = Instantiate(source);

      material.SetTexture("InTexture", source);
      material.SetFloat("WaveLength", WaveLength);

      Graphics.Blit(source, destination, material);
    }
  }
}
