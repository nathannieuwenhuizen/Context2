using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class RandomTexture : MonoBehaviour {
    public Texture2D[] textures;

    private void Start() {
        if (textures.Length > 0) {
            GetComponent<MeshRenderer>().material.mainTexture = (textures[Random.Range(0,textures.Length)]);
        }
    }
}