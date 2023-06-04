using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] Vector3 rotateAmount;
    [SerializeField] float rotateSpeed;
    [SerializeField] [Range(0,1)]float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }


    void Update()
    {
        PositionOscillator();
        Rotator();
    }

    private void PositionOscillator()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2; // constant value of 6.2283

        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 tp 1

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 positionOffset = movementVector * movementFactor;
        transform.position = startingPosition + positionOffset;
    }

    void Rotator()
    {
        transform.Rotate(rotateAmount * Time.deltaTime * rotateSpeed);
    }
}
