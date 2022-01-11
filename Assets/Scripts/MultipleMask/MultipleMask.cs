using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleMask : MonoBehaviour
{
    public Texture2D texture;
    public ComputeShader combineMaskShader;
    public AlphaConvert alphaConvert;
    public List<MaskInfo> maskes;
    public UITexture render;
    public int boardSize = 6;
    public int texResolution;
    public string kernelName = "GetMask";

    RenderTexture outputTexture;
    private int kernelHandler;

    ComputeBuffer alphaBuffer;
    ComputeBuffer boardIndexBuffer;

    
    float[] alpha;
    int[] boardIndex;


    public void Start()
    {
        texResolution = boardSize * 100;
        outputTexture = new RenderTexture(texResolution, texResolution, 0);
        outputTexture.enableRandomWrite = true;
        outputTexture.Create();
        InitData();
        InitShader();
    }

    private void InitData()
    {
        //alpha = alphaConvert.ConvertToFloatArray(maskes, 150);
        alpha = alphaConvert.ConvertToFloatArray(maskes[0].texture);
        boardIndex = new int[maskes.Count];
        for(int i = 0; i < maskes.Count; ++i)
        {
            boardIndex[i] = maskes[i].id;
        }
    }

    private void InitShader()
    {
        kernelHandler = combineMaskShader.FindKernel(kernelName);

        combineMaskShader.SetInt("texResolution", texResolution);
        combineMaskShader.SetInt("boardSize", boardSize);
        
        alphaBuffer = new ComputeBuffer(alpha.Length, sizeof(float));
        alphaBuffer.SetData(alpha);
        combineMaskShader.SetBuffer(kernelHandler, "alpha", alphaBuffer);

        boardIndexBuffer = new ComputeBuffer(boardIndex.Length, sizeof(int));
        boardIndexBuffer.SetData(boardIndex);
        combineMaskShader.SetBuffer(kernelHandler, "boardIndexes", boardIndexBuffer);

        combineMaskShader.SetTexture(kernelHandler, "Result", outputTexture);
        DispatchKernels(maskes.Count);
        render.material.SetTexture("_Mask", outputTexture);
    }



    private void DispatchKernels(int count)
    {
        combineMaskShader.Dispatch(kernelHandler, count, 1, 1);
    }

}

[System.Serializable]
public class MaskInfo
{
    public Texture2D texture;
    public int id;
}
