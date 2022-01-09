using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawDragDropItem : UIDragDropItem
{
    public UIPanel itemPanel;
    public BoxCollider boxCollider;

    protected override void OnDragDropStart()
    {
        itemPanel.depth = 5;
        Scale(JigsawBoardController.Instance.GetCameraScale);
        SetLayer(JigsawBoardController.Instance.fixedLayer);
        Vector3 position = JigsawBoardController.Instance.fixedCamera.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        position.z = 0;
        this.transform.position = position;
        base.OnDragDropStart();
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        if (JigsawBoardController.Instance.scrollview.CheckInsideScrollView(boxCollider))
        {
            itemPanel.depth = 4;
            this.restriction = Restriction.Vertical;
            Scale(1);
            base.OnDragDropRelease(JigsawBoardController.Instance.scrollview.gameObject);

        }
        else
        {
            
            itemPanel.depth = 1;
            this.restriction = Restriction.None;
            Scale(1);
            SetLayer(JigsawBoardController.Instance.scalableLayer);
            Vector3 position = JigsawBoardController.Instance.scalableCamera.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            position.z = 0;
            this.transform.position = position;
            base.OnDragDropRelease(JigsawBoardController.Instance.scrollview.boardContainer);
        }
    }

    public void Scale(float value)
    {
        this.transform.localScale = Vector3.one * value;
    }

    public void SetLayer(int layer)
    {
        this.gameObject.layer = layer;
        transform.SetChildLayer(layer);
    }
}
