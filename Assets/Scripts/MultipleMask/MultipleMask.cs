using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleMask : MonoBehaviour
{
    public Texture2D texture;
    public ComputeShader combineMaskShader;
    public AlphaConvert alphaConvert;
    public MaskInfo mask;
    public UITexture render;
    public int boardSize = 10;
    public int texResolution = 1080;
    public string kernelName = "GetMask";

    RenderTexture outputTexture;
    private int kernelHandler;
    JigsawMask[] jigsawMask;
    ComputeBuffer buffer;
    
    public float[] alpha;

    struct JigsawMask
    {
        public Color[] pixel;
        public int id;
    }

    public void Start()
    {
        outputTexture = new RenderTexture(texResolution, texResolution, 0);
        outputTexture.enableRandomWrite = true;
        outputTexture.Create();
        InitData();
        InitShader();
    }

    private void InitData()
    {
        alpha = alphaConvert.ConvertToFloatArray(mask.texture);
        
    }

    private void InitShader()
    {
        kernelHandler = combineMaskShader.FindKernel(kernelName);

        combineMaskShader.SetInt("texResolution", texResolution);
        combineMaskShader.SetInt("boardSize", boardSize);
        buffer = new ComputeBuffer(alpha.Length, sizeof(float));
        buffer.SetData(alpha);
        combineMaskShader.SetBuffer(kernelHandler, "mask", buffer);
        
        combineMaskShader.SetTexture(kernelHandler, "Result", outputTexture);
        DispatchKernels(1);
        render.material.SetTexture("_Mask", outputTexture);
    }



    private void DispatchKernels(int count)
    {
        combineMaskShader.Dispatch(kernelHandler, count, 1, 1);
    }


    [MyBox.ButtonMethod]
    public void SetAlpha()
    {
        alphaConvert.GetAplha(mask.texture);
    }
}

[System.Serializable]
public class MaskInfo
{
    public Texture2D texture;
    public int id;
}
