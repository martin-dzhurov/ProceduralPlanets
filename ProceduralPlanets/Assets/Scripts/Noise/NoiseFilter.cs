using UnityEngine;

public class NoiseFilter
{
    private Noise noise;
    private NoiseSettings _noiseSettings;

    public NoiseFilter(NoiseSettings noiseSettings)
    {
        noise = new Noise();
        _noiseSettings = noiseSettings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = _noiseSettings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < _noiseSettings.numberOfLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + _noiseSettings.center);
            noiseValue += (v + 1) * 0.5f * amplitude;

            frequency *= _noiseSettings.roughness;
            amplitude *= _noiseSettings.persistance;
        }

        noiseValue = Mathf.Max(0, noiseValue - _noiseSettings.minValue);
        return noiseValue * _noiseSettings.strenght;
    }
}
