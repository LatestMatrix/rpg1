using UnityEngine;
using System.Collections;
using engine.core;
using engine.manager;

public class Main : MonoBehaviour
{
    private MoveCtrl _mc;
    private AICtrl _ac;

    private KeyManager _km;
    // Use this for initialization
    void Start()
    {
        LEngine.ma = this;
        LEngine.em = new EventManager();
        LEngine.sm = new SceneManager();
        LEngine.rm = new ResManager();

        _mc = new MoveCtrl();
        _ac = new AICtrl();

        _km = new KeyManager();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
