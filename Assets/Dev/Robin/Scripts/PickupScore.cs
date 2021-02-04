using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Robin
{
    
}
public class PickupScore : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float fontSizeIncreaseStep;
    [SerializeField] private float lerpStep;
    [SerializeField] private float movementSpeed;
    
    [SerializeField] private float timeStep;
    [SerializeField] private float timer;

    [SerializeField] private Camera _camera;

    [SerializeField] private bool debugReset;
    void Start()
    {
        scoreText = GetComponentInChildren<TMP_Text>();
        scoreText.fontSize = 50;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (scoreText.fontSize < 120f)
        {
            scoreText.fontSize += fontSizeIncreaseStep * Time.deltaTime;
        }

        // if (scoreText.fontSize > 100 && scoreText.color.a > 0.1f)
        // {
        //     //lerpStep -= Time.deltaTime;
        //     Color color = scoreText.color;
        //     Color wantedColor = new Color(color.r, color.g, color.b, 0f);
        //     scoreText.color = Color.Lerp(color, wantedColor, lerpStep);
        // }

        if (timer >= timeStep)
        {
            scoreText.color = SwitchColor();

            timer = 0f;
        }

        if (debugReset)
        {
            SpawnScoreText(150, transform.position);
            debugReset = false;
        }
        
        timer += Time.deltaTime;
    }

    private Color SwitchColor()
    {
        if (scoreText.color == Color.white)
        {
            return Color.yellow;
        }
        else
        {
            return Color.white;
        }
    }
    
    public void SpawnScoreText(int scoreValue, Vector2 spawnPosition)
    {
        lerpStep = 1f;
        scoreText.fontSize = 50;
        scoreText.text = scoreValue.ToString();
        transform.position = _camera.WorldToScreenPoint(new Vector3(spawnPosition.x, spawnPosition.y, 0));
    }
}
