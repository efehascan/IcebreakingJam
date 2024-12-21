using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Balls : MonoBehaviour
{
    [SerializeField] public Colors ballColor = Colors.None;
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
        int enumMax = Enum.GetValues(typeof(Colors)).Length - 1;
        var random = UnityEngine.Random.Range(0, enumMax);
        
        ballColor = (Colors)random;
        
        renderer.material.color = colors[random];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
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
