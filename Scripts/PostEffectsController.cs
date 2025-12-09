using UnityEngine;

public class PostEffectsController : MonoBehaviour
{
    [SerializeField] private Shader postShader;
    Material postEffectMaterial;

    [SerializeField] private float radius;
    [SerializeField] private float feather;
    [SerializeField] private Color tintColor;

    public void SetRadius(float radius) { this.radius = radius; }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        int width = src.width;
        int height = src.height;

        if(postEffectMaterial == null) { postEffectMaterial = new Material(postShader); }

        RenderTexture startRenderTexture = RenderTexture.GetTemporary(width, height, 0, src.format);

        postEffectMaterial.SetFloat("_Radius", radius);
        postEffectMaterial.SetFloat("_Feather", feather);
        postEffectMaterial.SetColor("_TintColor", tintColor);
        Graphics.Blit(src, startRenderTexture, postEffectMaterial);
        Graphics.Blit(startRenderTexture, dest);
        RenderTexture.ReleaseTemporary(startRenderTexture);
    }
}
