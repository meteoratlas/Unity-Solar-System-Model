using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlanetHover : MonoBehaviour {

    private bool tweenActive;
    float tweenVal = 0f;
    Material material;
    readonly int outlineExtrude = Shader.PropertyToID("_OutlineExtrusion");

    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void OnMouseOver()
    {
        if (!tweenActive)
        {
            tweenActive = true;
            DOTween.To(() => tweenVal, x => tweenVal = x, 0.3f, 0.3f);
        }

    }

    void OnMouseExit()
    {
        if (tweenActive)
        {
            tweenActive = false;
            DOTween.To(() => tweenVal, x => tweenVal = x, 0.0f, 0.15f);
        }
    }

    void Update()
    {
        material.SetFloat(outlineExtrude, tweenVal);
    }
}
