using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;


public class Photo : MonoBehaviour
{
    public Camera maincamera;
    private bool start = false;
    public int num = 1;
    public string path = "D:/terrain_dataset/scene16";      // path to save images and depth data

    Texture2D CaptureCamera(Camera camera, Rect rect)
    {

        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, -1);
        camera.targetTexture = rt;
        camera.Render();
        RenderTexture.active = rt;

        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();

        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors  
        GameObject.Destroy(rt);

        byte[] bytes = screenShot.EncodeToJPG();

        string front;
        if (num < 10)
            front = "/00000";
        else if (num < 100)
            front = "/0000";
        else
            front = "/000";

        string colorpath = path + front + num;
        string depthpath = path + "/depth" + front + num;
        // save color image
        System.IO.File.WriteAllBytes(colorpath + ".jpg", bytes);
        Debug.Log(string.Format("Saved one color image: {0}", colorpath + ".jpg"));
        // obtain and save dense depth data (optional)
        //GenerateDepthData(camera, depthpath);
        //Debug.Log(string.Format("Saved dense depth data£º{0}", depthpath + ".txt"));

        return screenShot;
    }

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GetComponent<Camera>();
        // camera physical parameters
        float f = maincamera.focalLength;
        int w = maincamera.pixelWidth;
        int h = maincamera.pixelHeight;
        Debug.Log(string.Format("w:" + w + " h:" + h));
        Vector2 size = maincamera.sensorSize;
        // compute camera intrinsic matrix
        float fx = f * (w / size[0]);
        float fy = f * (h / size[1]);
        float cx = (float)w / 2;
        float cy = (float)h / 2;
        float[,] cameramatrix = new float[3, 3] { { fx, 0, cx }, { 0, fy, cy }, { 0, 0, 1 } };
        // save camera intrinsic matrix
        string filepath = path + "/cam.txt";
        StreamWriter sw = new StreamWriter(filepath);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                sw.Write(cameramatrix[i, j]);
                sw.Write(" ");
            }
            sw.Write("\n");
        }
        sw.Close();
        Debug.Log("Saved camera intrinsic matrix");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CaptureCamera(maincamera, new Rect(0, 0, Screen.width, Screen.height));
            // CaptureCamera(maincamera, new Rect(0, 0, 640, 480));
            num++;
        }
    }

    // Obtain the world coordinates of each pixel in the image, 
    // and then convert to the camera coordinate system to obtain 
    // the depth value of each point away from the camera plane.
    void GenerateDepthData(Camera camera, string filepath)
    {
        int width = Screen.width;        int height = Screen.height;
        // int width = (int)rect.width;
        // int height = (int)rect.height;

        StreamWriter sw = new StreamWriter(filepath + ".txt");
        for (int i = height - 1; i >= 0; i--)
        {
            for (int j = 0; j < width; j++)
            {
                Vector3 pos = new Vector3(j, i, 0);
                Ray ray = camera.ScreenPointToRay(pos);
                // Debug.DrawRay(ray.origin, ray.direction * 10, Color.black);
                RaycastHit hit;
                // collision detection
                if (Physics.Raycast(ray, out hit))
                {
                    // from world coordinate to camera coordinate
                    Vector3 world_point = camera.WorldToViewportPoint(hit.point);
                    sw.Write(world_point.z);
                    sw.Write("  ");
                }
                // no collision happened
                else
                {
                    Debug.Log("no collision happened£¡");
                    sw.Write(-1);
                    sw.Write("  ");
                }
            }
            sw.Write("\n");
        }
        sw.Close();
    }
}