using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTexture : MonoBehaviour
{
    public Texture[] animatedImages;
    public Material material;
    
    void Start()
    {
    }
    
    void Update()
    {
        material.SetTexture("_MainTex", animatedImages[(int)(Time.time*10)%animatedImages.Length]);
    }
}
