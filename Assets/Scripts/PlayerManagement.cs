using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    
    public int score = 0;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        playerRenderer = GetComponent<Renderer>();
    }


    void Update()
    {
        Move();
        HandleColorChange();
        
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
    void HandleColorChange()  
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerColor = GetNextColor(playerColor, true);
            SetColor();
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerColor = GetNextColor(playerColor, false);
            SetColor();
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

    private void OnCollisionEnter(Collision other)
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
    }
}