public class test04 : MonoBehaviour {
    //对象
    private GameObject mainCamera;
    private GameObject camera1;
    private GameObject camera2;
    private GameObject camera3;

    // Use this for initialization
    void Start () {
        mainCamera = GameObject.Find ("Main Camera");
        camera1 = GameObject.Find ("Camera1");
        camera2 = GameObject.Find ("Camera2");
        camera3 = GameObject.Find ("Camera3");

        camera1.active = false;
        camera2.active = false;
        camera3.active = false;
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    void OnGUI() {

        if (GUILayout.Button ("主 摄像机", GUILayout.Height (50))) {
            mainCamera.active = true;
            camera1.active = false;
            camera2.active = false;
            camera3.active = false;
        }
        if (GUILayout.Button ("左 摄像机", GUILayout.Height (50))) {
            mainCamera.active = false;
            camera1.active = true;
            camera2.active = false;
            camera3.active = false;
        }
        if (GUILayout.Button ("右 摄像机", GUILayout.Height (50))) {
            mainCamera.active = false;
            camera1.active = false;
            camera2.active = true;
            camera3.active = false;
        }
        if (GUILayout.Button ("顶部 摄像机", GUILayout.Height (50))) {
            mainCamera.active = false;
            camera1.active = false;
            camera2.active = false;
            camera3.active = true;
        }
    }
}
