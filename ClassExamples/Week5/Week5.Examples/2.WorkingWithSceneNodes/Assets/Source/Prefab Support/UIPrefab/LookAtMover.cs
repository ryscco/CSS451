using UnityEngine;
using UnityEngine.UI;

public class LookAtMover : MonoBehaviour
{
    public SliderWithEcho X, Y, Z;
    private Transform mSelected;
    private Vector3 mPreviousSliderValues = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        mSelected = GameObject.Find("LookAt").transform;
        X.SetSliderListener(XValueChanged);
        Y.SetSliderListener(YValueChanged);
        Z.SetSliderListener(ZValueChanged);
        SetToTranslation(true);
    }
    //---------------------------------------------------------------------------------
    // Initialize slider bars to specific function
    void SetToTranslation(bool v)
    {
        Vector3 p = ReadObjectXfrom();
        mPreviousSliderValues = p;
        X.InitSliderRange(-30, 30, p.x);
        Y.InitSliderRange(-20, 20, p.y);
        Z.InitSliderRange(-30, 30, p.z);
    }
    //---------------------------------------------------------------------------------

    //---------------------------------------------------------------------------------
    // resopond to sldier bar value changes
    void XValueChanged(float v)
    {
        Vector3 p = ReadObjectXfrom();
        // if not in rotation, next two lines of work would be wasted
        float dx = v - mPreviousSliderValues.x;
        mPreviousSliderValues.x = v;
        Quaternion q = Quaternion.AngleAxis(dx, Vector3.right);
        p.x = v;
        UISetObjectXform(ref p, ref q);
    }
    void YValueChanged(float v)
    {
        Vector3 p = ReadObjectXfrom();
        // if not in rotation, next two lines of work would be wasted
        float dy = v - mPreviousSliderValues.y;
        mPreviousSliderValues.y = v;
        Quaternion q = Quaternion.AngleAxis(dy, Vector3.up);
        p.y = v;
        UISetObjectXform(ref p, ref q);
    }
    void ZValueChanged(float v)
    {
        Vector3 p = ReadObjectXfrom();
        // if not in rotation, next two lines of work would be wasterd
        float dz = v - mPreviousSliderValues.z;
        mPreviousSliderValues.z = v;
        Quaternion q = Quaternion.AngleAxis(dz, Vector3.forward);
        p.z = v;
        UISetObjectXform(ref p, ref q);
    }
    public void ObjectSetUI()
    {
        Vector3 p = ReadObjectXfrom();
        X.SetSliderValue(p.x);  // do not need to call back for this comes from the object
        Y.SetSliderValue(p.y);
        Z.SetSliderValue(p.z);
    }

    private Vector3 ReadObjectXfrom()
    {
        Vector3 p;
        if (mSelected != null)
            p = mSelected.localPosition;
        else
            p = Vector3.zero;
        return p;
    }

    private void UISetObjectXform(ref Vector3 p, ref Quaternion q)
    {
        if (mSelected == null)
            return;
        mSelected.localPosition = p;
    }
}