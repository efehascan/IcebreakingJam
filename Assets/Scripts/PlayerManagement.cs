using System;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    public float acceleration = 5f;     // İvme
    public float maxSpeed = 10f;        // Maksimum hız
    public float deceleration = 10f;   // Yavaşlama (fren etkisi)
    private float currentSpeed = 0f;   // Şu anki hız
    private int moveDirection = 0;     // Hareket yönü: -1 (sol), 1 (sağ), 0 (durağan)

    void Update()
    {
        Move();
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
}
