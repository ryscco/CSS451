using UnityEngine;
public class NodePrimitive : MonoBehaviour
{
    public Color MyColor = new Color(0.1f, 0.1f, 0.2f, 1.0f);
    public Vector3 Pivot;
    private Material _material;
    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }
    public void LoadShaderMatrix(ref Matrix4x4 nodeMatrix)
    {
        Matrix4x4 p = Matrix4x4.TRS(Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 invp = Matrix4x4.TRS(-Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        Matrix4x4 m = nodeMatrix * p * trs * invp;
        _material.SetMatrix("MyXformMat", m);
        _material.SetColor("MyColor", MyColor);
    }
}