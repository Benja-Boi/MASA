using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public DartShooter _dartShooter;
    public NetShooter _netShooter;
    bool[] _tools;

    void Start()
    {
        _tools = new bool[2];
        //_dartShooter = GetComponentInChildren<DartShooter>();
        //_netShooter = GetComponentInChildren<NetShooter>();
        _dartShooter.gameObject.SetActive(false);
        _netShooter.gameObject.SetActive(true);
        _tools[1] = true;
        _tools[0] = false;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            SwapWeapon();
        }
    }

    void SwapWeapon()
    {
        if (_tools[0])
        {
            _dartShooter.gameObject.SetActive(false);
            _netShooter.gameObject.SetActive(true);
            _tools[0] = false;
            _tools[1] = true;
        }
        else
        {
            _dartShooter.gameObject.SetActive(true);
            _netShooter.gameObject.SetActive(false);
            _tools[0] = true;
            _tools[1] = false;
        }
    }
}
