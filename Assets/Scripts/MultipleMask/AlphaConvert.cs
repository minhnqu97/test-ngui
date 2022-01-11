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

    public void Awake()
    {

        m_kernelIndex = m_computeShader.FindKernel("CSMain");
    }

    public float[] ConvertToFloatArray(List<MaskInfo> mask, int textureSize)
    {
        float[] result = new float[textureSize * textureSize * mask.Count];
        m_outputBuffer = new ComputeBuffer(result.Length, sizeof(float));
        m_outputBuffer.SetData(result);
        m_computeShader.SetBuffer(m_kernelIndex, OUTPUT_BUFFER, m_outputBuffer);

        for(int i = 0; i < mask.Count; ++i)
        {
            m_computeShader.SetTexture(m_kernelIndex, INPUT_TEXTURE, mask[i].texture);
            int index = i;
            m_computeShader.SetInt("index", index);
            m_computeShader.Dispatch(m_kernelIndex, mask[i].texture.width / 30, mask[i].texture.height / 30, 1);
        }

        m_outputBuffer.GetData(result);

        return result;
    }

    public float[] ConvertToFloatArray(Texture2D texture)
    {
        float[] result = new float[texture.width * texture.height];
        m_outputBuffer = new ComputeBuffer(result.Length, sizeof(float));
        m_outputBuffer.SetData(result);
        m_computeShader.SetBuffer(m_kernelIndex, OUTPUT_BUFFER, m_outputBuffer);

        m_computeShader.SetTexture(m_kernelIndex, INPUT_TEXTURE, texture);
        m_computeShader.SetInt("index", 0);
        m_computeShader.Dispatch(m_kernelIndex, texture.width / 30, texture.height / 30, 1);

        m_outputBuffer.GetData(result);

        return result;
    }
}
