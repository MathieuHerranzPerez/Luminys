﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class LightControler : MonoBehaviour
{
    public float maxLightSpeed = 15f;
    public float delta = 0.15f;
    public float multiplier = 1f;

    public GameObject deathEffect;

    public Slider sliderMultiplier;  


    private bool isMoving = false;
    private bool needToMove = false;
    private Vector3 targetPos;

    private Camera cam;
    private Vector3 previousLightPos;
    private Vector3 previousMousePos;
    private Vector3 currentMousePos;

    private Vector3 world;


    private void Awake()
    {
        multiplier = PlayerPrefs.GetFloat("multiplier", multiplier);
        sliderMultiplier.value = multiplier * 2f;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        world = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartMoving();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopMoving();
        }

        if(isMoving)
        {
            //GetCurrentMousePos();

            previousMousePos = currentMousePos;
            currentMousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector2 move = currentMousePos - previousMousePos;

            SetLightTargetPos(move);
        }

        if (needToMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, maxLightSpeed * Time.deltaTime);
        }
    }

    private void GetCurrentMousePos()
    {
        currentMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void StartMoving()
    {
        isMoving = true;
        currentMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        needToMove = true;
    }

    private void StopMoving()
    {
        isMoving = false;
    }

    private void SetLightTargetPos(Vector3 movementVector)
    {
        Vector3 lightPosScreen = transform.position + (movementVector * multiplier);
        // x
        if (lightPosScreen.x >= world.x)
        {
            lightPosScreen.x = world.x;
        }
        else if (lightPosScreen.x <= - world.x)
        {
            lightPosScreen.x = - world.x;
        }
        // y
        if (lightPosScreen.y >= world.y)
        {
            lightPosScreen.y = world.y;
        }
        else if (lightPosScreen.y <= -world.y)
        {
            lightPosScreen.y = -world.y;
        }

        targetPos = lightPosScreen;
        needToMove = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "MovingBlock")   // we get hit by a block
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
        GameManager.GetInstance().SetGameOver();
    }

    public void SetSensitivity()
    {
        multiplier = sliderMultiplier.value / 2f;
        PlayerPrefs.SetFloat("multiplier", multiplier);                 // set in the preferences
    }
}