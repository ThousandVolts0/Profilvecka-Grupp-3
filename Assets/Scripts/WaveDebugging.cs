using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class WaveDebugger : MonoBehaviour
{
    private WavesSystem wavesSystem;

    public void Start()
    {
        // Find the WavesSystem component in your scene
        wavesSystem = FindObjectOfType<WavesSystem>();

        if (wavesSystem == null)
        {
            Debug.LogError("wavesystem not found bruh");
        }
    }

    public void Update()
    {
        // For example, pressing the 'D' key will trigger the end of the wave for debugging
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (wavesSystem == null)
            {
                Debug.LogWarning("wavesystem not found");
                return;
            }
            DestroyActiveEnemies();
            wavesSystem.CheckWaveCompletion();
            Debug.Log("wave debugging complete, enemies destroyed and next wave has been called");
        }
    }

    public void DestroyActiveEnemies()
    {
        foreach (var enemy in wavesSystem.activeEnemies)
        {
            GameObject.Destroy(enemy);
        }
        wavesSystem.activeEnemies.Clear();
    }
}
