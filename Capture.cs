using UnityEngine;

public class Capture : MonoBehaviour
{
    public KeyCode MakeScreenshot = KeyCode.S;
    public string prefix = "Screenshot_";
    public int Factor = 2;
    void Update()
    {
        if (Input.GetKeyDown(MakeScreenshot))
        {
            string timestamp = System.DateTime.UtcNow.ToString("yyMMdd-HHmmss-fff");
            ScreenCapture.CaptureScreenshot(prefix + timestamp + ".png", Factor);
        }
    }
}
