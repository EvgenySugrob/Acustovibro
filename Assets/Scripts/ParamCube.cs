using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMiltiplier;
    public bool _userBuffer;

    private void Update()
    {
        if (_userBuffer)
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._bandBuffer[_band] * _scaleMiltiplier) + _startScale, transform.localScale.z);
        if (!_userBuffer)
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._freqBand[_band] * _scaleMiltiplier) + _startScale, transform.localScale.z);

    }
}
