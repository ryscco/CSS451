using UnityEngine;
using System.Collections.Generic;
public class SceneNode : MonoBehaviour
{
    protected Matrix4x4 mCombinedParentXform;
    public Vector3 NodeOrigin = Vector3.zero;
    public List<NodePrimitive> PrimitiveList;
    public GameObject AxisFrame, SmallViewCamera;
    public Vector3 defaultOrigin, cameraOrigin;
    [SerializeField] private bool IsCameraNode;
    public bool IsSelectedNode { get; set; }
    protected void Start()
    {
        InitializeSceneNode();
        AxisFrame = GameObject.Find("AxisFrame");
        SmallViewCamera = GameObject.Find("SmallViewCamera");
        // Debug.Log("PrimitiveList:" + PrimitiveList.Count);
    }
    private void InitializeSceneNode()
    {
        mCombinedParentXform = Matrix4x4.identity;
    }
    // This must be called _BEFORE_ each draw!! 
    public void CompositeXform(ref Matrix4x4 parentXform)
    {
        Matrix4x4 orgT = Matrix4x4.Translate(NodeOrigin);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        mCombinedParentXform = parentXform * orgT * trs;
        // propagate to all children
        foreach (Transform child in transform)
        {
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                cn.CompositeXform(ref mCombinedParentXform);
            }
        }
        // disenminate to primitives
        foreach (NodePrimitive p in PrimitiveList)
        {
            p.LoadShaderMatrix(ref mCombinedParentXform);
        }
        //Compute AxisFrame
        if (IsSelectedNode)
        {
            AxisFrame.transform.localPosition = mCombinedParentXform.MultiplyPoint(defaultOrigin);
            Vector3 up = mCombinedParentXform.GetColumn(1).normalized;
            Vector3 forward = mCombinedParentXform.GetColumn(2).normalized;
            // First align up direction, remember that the default AxisFrame.up is simply the y-axis
            float angle = Mathf.Acos(Vector3.Dot(Vector3.up, up)) * Mathf.Rad2Deg;
            Vector3 axis = Vector3.Cross(Vector3.up, up);
            AxisFrame.transform.localRotation = Quaternion.AngleAxis(angle, axis);
            // Now, align the forward axis
            angle = Mathf.Acos(Vector3.Dot(AxisFrame.transform.forward, forward)) * Mathf.Rad2Deg;
            axis = Vector3.Cross(AxisFrame.transform.forward, forward);
            AxisFrame.transform.localRotation = Quaternion.AngleAxis(angle, axis) * AxisFrame.transform.localRotation;
        }
        if (IsCameraNode)
        {
            SmallViewCamera.transform.localPosition = mCombinedParentXform.MultiplyPoint(cameraOrigin);
            Vector3 up = mCombinedParentXform.GetColumn(1).normalized;
            Vector3 forward = mCombinedParentXform.GetColumn(2).normalized;
            // First align up direction, remember that the default AxisFrame.up is simply the y-axis
            float angle = Mathf.Acos(Vector3.Dot(Vector3.up, up)) * Mathf.Rad2Deg;
            Vector3 axis = Vector3.Cross(Vector3.up, up);
            SmallViewCamera.transform.localRotation = Quaternion.AngleAxis(angle, axis);
            // Now, align the forward axis
            angle = Mathf.Acos(Vector3.Dot(SmallViewCamera.transform.forward, forward)) * Mathf.Rad2Deg;
            axis = Vector3.Cross(SmallViewCamera.transform.forward, forward);
            SmallViewCamera.transform.localRotation = Quaternion.AngleAxis(angle, axis) * SmallViewCamera.transform.localRotation;
        }
    }
}