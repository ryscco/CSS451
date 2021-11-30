using UnityEngine;
using UnityEngine.SceneManagement;
public class TheWorld : MonoBehaviour
{
    public SceneNode TheRoot;
    private void Update()
    {
        Matrix4x4 i = Matrix4x4.identity;
        TheRoot.CompositeXform(ref i);
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Scene0");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }
}