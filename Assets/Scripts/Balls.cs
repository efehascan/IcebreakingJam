using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Balls : MonoBehaviour
{
    [SerializeField] private Colors ballColor = Colors.None;
    [SerializeField] private Color[] colors;
    [SerializeField] private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        RandomcColor();
    }
    
    

    [ContextMenu("RandomColor")]
    void RandomcColor()
    {
        int enumMax = Enum.GetValues(typeof(Colors)).Length;
        
        var Random = UnityEngine.Random.Range(0, enumMax);
        
        ballColor = (Colors)Random;
        
        renderer.material.color = colors[(int)ballColor];
    }
}

public enum Colors
{
    None = -1,
    Red = 0,
    Blue = 1,
    Green = 2,
    Yellow = 3
}
