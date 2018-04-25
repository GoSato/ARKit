using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ARFakeIBL : MonoBehaviour
{
    [SerializeField]
    // ARKitで用意されているYUVMaterialを設定
    private Material _yuvMat;

    [SerializeField]
    // 疑似IBLスカイボックス用マテリアルとして使用
    private Material _yuvSkyboxMat;

    [SerializeField]
    private int _frameInterval;

    private int _frameCount;

    void Update()
    {
        if (_frameCount == _frameInterval)
        {
            Texture textureY = _yuvMat.GetTexture("_textureY");
            Texture textureCbCr = _yuvMat.GetTexture("_textureCbCr");

            _yuvSkyboxMat.SetTexture("_TextureY", textureY);
            _yuvSkyboxMat.SetTexture("_TextureCbCr", textureCbCr);

            DynamicGI.UpdateEnvironment();

            _frameCount = 0;
        }
        else
        {
            _frameCount++;
        }
    }
}