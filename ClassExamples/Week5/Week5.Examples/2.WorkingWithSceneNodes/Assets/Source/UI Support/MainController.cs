using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle
public partial class MainController : MonoBehaviour
{
    // reference to all UI elements in the Canvas
    public Camera MainCamera = null;
    public TheWorld TheWorld = null;
    public SceneNodeControl NodeControl = null;
    void Awake()
    {
        Debug.Assert(NodeControl != null);
        NodeControl.TheRoot = TheWorld.TheRoot;
    }
    void Start()
    {
        Debug.Assert(MainCamera != null);
        Debug.Assert(TheWorld != null);
    }
    void Update()
    {
        // ProcessMouseEvents();
    }
}