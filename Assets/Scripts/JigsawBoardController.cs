using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawBoardController : MonoBehaviour
{
    public static JigsawBoardController Instance;

    public JigsawScrollView scrollview;
    public GameObject container;
    public int scalableLayer;
    public int fixedLayer;
    public Camera fixedCamera;
    public Camera scalableCamera;

    public void Awake()
    {
        Instance = this;
    }

    public float GetCameraScale => fixedCamera.orthographicSize / scalableCamera.orthographicSize;

}
