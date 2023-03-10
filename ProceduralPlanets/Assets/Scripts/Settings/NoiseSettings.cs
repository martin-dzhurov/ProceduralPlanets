using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public float strenght = 1f;
    [Range(1, 8)]
    public int numberOfLayers = 1;
    public float baseRoughness = 1f;
    public float roughness = 2f;
    public float persistance = 0.5f;
    public Vector3 center;
    public float minValue;
}
