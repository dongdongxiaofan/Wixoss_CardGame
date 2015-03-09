using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour
{

    public float RotaionSpeed;
    public Texture2D Texture2D;

    // Use this for initialization
    void Start()
    {
//        m.mainTexture = Texture2D;
//        renderer.material = m;
    }

    // Update is called once per frame
    void Update()
    {
        var rot = Quaternion.Euler(0, 0, Time.time * RotaionSpeed);
        var m = Matrix4x4.TRS(Vector3.zero, rot, new Vector3(1, 1, 1));
        renderer.material.SetMatrix("_Rotation", m);
    }
}
