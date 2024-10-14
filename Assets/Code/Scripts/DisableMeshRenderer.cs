using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMeshRenderer : MonoBehaviour
{
    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer.enabled)
        {
            _renderer.enabled = false;
        }
    }
}