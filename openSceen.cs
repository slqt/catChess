using System.Collections;
public class Test: MonoBehaviour
｛
	// 电影纹理
	public MovieTexture movTexture;
	void Start ()
	｛
		// 设置电影纹理播放模式为循环
		movTexture.loop = false;
		movTexture.Play ();
	｝
	void Update ()
	｛
		if (Input.GetMouseButtonDown (0)) ｛
			Debug.Log ("当点击屏幕的时候可以触发跳转场景等一些事件");
		｝
	｝
	void OnGUI ()
	｛
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), movTexture);
		if (GUILayout.Button ("播放/继续")) ｛
			// 播放/继续播放视频
				movTexture.Play ();
		｝
		if (GUILayout.Button ("暂停播放")) ｛
			// 暂停播放
			movTexture.Pause ();
		｝
		if (GUILayout.Button ("停止播放")) ｛
			// 停止播放
			movTexture.Stop ();
		｝
	｝
｝
