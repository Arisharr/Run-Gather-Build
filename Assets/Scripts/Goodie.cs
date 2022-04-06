using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goodie : MonoBehaviour
{
    public enum Goodies { wood, stone, gold }
    public Goodies type;
    public int value;
    public float lerpSpeed = 10f;
    private bool stored = false;
    private Player player;
    private Vector3 stackPose;
    [SerializeField] private float stackingOffset;

    private void Start()
    {
        player = FindObjectOfType<Player>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().Gather(type, value, gameObject);
            stackPose = new Vector3(0, (player.storageCount * stackingOffset), 0);
            stored = true;
        }
    }

    private void Update()
    {
        if (stored)
        {
            transform.SetParent(player.Storage);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, stackPose, lerpSpeed * Time.deltaTime);
        }
    }
}
