using UnityEngine;

[ExecuteAlways] // Ch?y ngay c? trong c?a s? Scene khi thi?t k?
public class AutoTiling : MonoBehaviour
{
    [SerializeField] private Vector2 textureScale = new Vector2(1f, 1f); // T? l? l?p texture

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;

    private void OnValidate()
    {
        UpdateTiling();
    }

    private void Update()
    {
        // C?p nh?t ngay trong Scene View khi thay ??i Scale
        if (!Application.isPlaying)
        {
            UpdateTiling();
        }
    }

    private void UpdateTiling()
    {
        if (_renderer == null) _renderer = GetComponent<Renderer>();
        if (_propBlock == null) _propBlock = new MaterialPropertyBlock();

        _renderer.GetPropertyBlock(_propBlock);

        // Tính toán Tiling d?a trên Scale c?a Object nhân v?i t? l? texture
        Vector3 scale = transform.lossyScale;
        Vector4 tilingOffset = new Vector4(scale.x * textureScale.x, scale.z * textureScale.y, 0, 0);

        // ??t thu?c tính BaseMap (URP/Lit Shader)
        _propBlock.SetVector("_BaseMap_ST", tilingOffset);
        _renderer.SetPropertyBlock(_propBlock);
    }
}