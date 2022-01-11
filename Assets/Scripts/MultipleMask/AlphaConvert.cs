using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaConvert : MonoBehaviour
{
    public const string INPUT_TEXTURE = "InputTexture";
    public const string OUTPUT_BUFFER = "OutputBuffer";
    public ComputeShader m_computeShader;
    int m_kernelIndex;
    ComputeBuffer m_outputBuffer;

    public float[] alpha;

    public void Awake()
    {

        m_kernelIndex = m_computeShader.FindKernel("CSMain");
    }

    public float[] ConvertToFloatArray(Texture2D renderTexture)
    {
        m_computeShader.SetTexture(m_kernelIndex, INPUT_TEXTURE, renderTexture);
        
        float[] result = new float[renderTexture.width * renderTexture.height];
        m_outputBuffer = new ComputeBuffer(result.Length, sizeof(float));
        m_outputBuffer.SetData(result);
        m_computeShader.SetBuffer(m_kernelIndex, OUTPUT_BUFFER, m_outputBuffer);

        m_computeShader.Dispatch(m_kernelIndex, renderTexture.width / 30, renderTexture.height / 30, 1);

        m_outputBuffer.GetData(result);

        return result;
    }
    
    
    public void GetAplha(Texture2D renderTexture)
    {
        alpha = new float[renderTexture.width * renderTexture.height];
        Color[] color = renderTexture.GetPixels();
        for(int i = 0; i < renderTexture.width; ++i) 
            for(int j = 0; j < renderTexture.height; ++j)
            {
                alpha[i + j * renderTexture.width] = color[i + j * renderTexture.width].a;
            }
    }
}
