using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    #region Hız Değişkenleri
    
    public float acceleration = 5f;     // İvme
    public float maxSpeed = 10f;        // Maksimum hız
    public float deceleration = 10f;   // Yavaşlama (fren etkisi)
    private float currentSpeed = 0f;   // Şu anki hız
    private int moveDirection = 0;     // Hareket yönü: -1 (sol), 1 (sağ), 0 (durağan)
    
    

    #endregion

    #region Player Color Değişimleri
    [SerializeField] private Colors playerColor = Colors.None;
    [SerializeField] private Color[] colors;
    [SerializeField] private Renderer playerRenderer;
    #endregion

    #region  Text & Score 
    
    public int score = 0;
    public TextMeshProUGUI scoreText;
    
    #endregion
    
    [SerializeField] private GameObject objectRed;
    [SerializeField] private GameObject objectBlue;
    [SerializeField] private GameObject objectGreen;
    [SerializeField] private GameObject objectYellow;
    

    private void Awake()
    {
        playerRenderer = GetComponent<Renderer>();
    }


    void Update()
    {
        Move();
        OnHandleColorChange();
        
        scoreText.text = "Score: " + score.ToString();
    }

    void Move()
    {
        // Sağ veya sol tuşlara basılıp basılmadığını kontrol et
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = 1; // Sağa doğru hareket
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = -1; // Sola doğru hareket
        }
        else
        {
            moveDirection = 0; // Durağan
        }

        // Hareket hızını güncelle
        if (moveDirection != 0)
        {
            // İvme uygula
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveDirection * maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            // Yavaşlama uygula (sürtünme etkisi)
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        // Pozisyonu güncelle
        transform.position += new Vector3(currentSpeed * Time.deltaTime, 0, 0);
    }

    // Karakterin tuşa bastıktan sonra renk değiştirmesi    
    void OnHandleColorChange()  
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerColor = GetNextColor(playerColor, true);
            EnumColorMatch();
            
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerColor = GetNextColor(playerColor, false);
            EnumColorMatch();
        }
    }

    void EnumColorMatch()
    {
        switch (playerColor)
        {
            case Colors.Red:
                ShowObject(objectRed);
                HideObject(objectBlue, objectGreen, objectYellow);
                break;
            case Colors.Blue:
                ShowObject(objectBlue);
                HideObject(objectRed, objectGreen, objectYellow);
                break;
            case Colors.Green:
                ShowObject(objectGreen);
                HideObject(objectRed, objectBlue, objectYellow);
                break;
            case Colors.Yellow:
                ShowObject(objectYellow);
                HideObject(objectRed, objectBlue, objectGreen);
                break;
            default:
                HideObject(objectRed, objectBlue, objectGreen, objectYellow);
                break;
        }
    }
    
    // Objeleri göster
    void ShowObject(GameObject obj)
    {
        obj.SetActive(true);  // Objeyi aktif yap
    }

// Objeleri gizle
    void HideObject(params GameObject[] objs)
    {
        foreach (var obj in objs)
        {
            obj.SetActive(false);  // Objeyi gizle
        }
    }


    // Enumlar arası yukarı aşağı geçişi sağlar
    Colors GetNextColor(Colors current, bool isUp)
    {
        int firstColor = (int)Colors.Red;
        int lastColor = (int)Colors.Yellow;

        int currentIndex = (int)current;

        if (isUp)
        {
            currentIndex = currentIndex - 1 < firstColor ? lastColor : currentIndex - 1;
            
        }
        else
        {
            currentIndex = currentIndex + 1 > lastColor ? firstColor : currentIndex + 1;
        }

        return (Colors)currentIndex;
    }
    
    // Karaktere renk atamasını sağlar
    
    void SetColor()
    {
        int ColorIndex = (int)playerColor;
        
        if(ColorIndex >= 0 && ColorIndex < colors.Length) playerRenderer.material.color = colors[ColorIndex];
        else Debug.LogWarning("Renk atanamadı!");
    }
    
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        try
        {
            var ball = other.transform.GetComponent<Balls>();
            if (playerColor == ball.ballColor) score++;
            else score--;
            
            Destroy(other.gameObject);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        if (other.gameObject.CompareTag("Duvar"))
            score = score - 10;
    }
}