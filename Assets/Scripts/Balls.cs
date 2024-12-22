using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Balls : MonoBehaviour
{
    [SerializeField] public Colors ballColor = Colors.None;
    [SerializeField] private Color[] colors;
    [SerializeField] private Renderer renderer;
    
    float moveSpeed = 1.5f;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        RandomcColor();
    }

    public void Move()
    {
        transform.position += Vector3.down * (moveSpeed * Time.deltaTime);
    }
    
    

    [ContextMenu("RandomColor")]
    void RandomcColor()
    {
        int enumMax = Enum.GetValues(typeof(Colors)).Length - 1;
        var random = UnityEngine.Random.Range(0, enumMax);
        
        ballColor = (Colors)random;
        
        renderer.material.color = colors[random];
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
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
