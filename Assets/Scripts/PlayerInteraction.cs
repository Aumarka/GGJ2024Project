using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameDirector gameDirector;
    Baby _baby;

    public Transform Camera;
    public float InteractRange;

    public Item EquippedItem;
    public Transform EquipPoint, LaunchPoint;

    public float ChargeTime, MaxChargeTime;
    public float YeetForce;

    bool IsPickingUpItem = false;

    public TagList interactableTags;

    public bool IsPeakabooing = false;
    public Transform Hands;
    public float HandMoveDuration = 1, HandHoldDuration = 1;
    public float HandMoveSpeed = 0.1f;

    private void Start()
    {
        if (gameDirector == null)
            gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        _baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<Baby>();

        // Pick up baby
        EquippedItem = _baby;
        EquippedItem.PickUp(EquipPoint, Camera);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameDirector)
        {
            gameDirector.gameUIManager.UpdateThrowBar(ChargeTime / MaxChargeTime);
        }

        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit preHit, InteractRange))
        {
            if (preHit.collider.gameObject.TryGetComponent<Item>(out Item item))
            {
                if (gameDirector)
                {
                    gameDirector.gameUIManager.SetItemText(item.itemName);
                }
                else
                {
                    Debug.Log(item.itemName);
                }
                
            }
            else
            {
                if (gameDirector)
                {
                    gameDirector.gameUIManager.SetItemText("");
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && !EquippedItem && !IsPeakabooing)
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, InteractRange))
            {
                if (TagIsInteractable(hit.collider.gameObject.tag))
                {
                    IsPickingUpItem = true;

                    EquippedItem = hit.collider.GetComponent<Item>();

                    EquippedItem.PickUp(EquipPoint, Camera);

                    return;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (EquippedItem)
            {
                ChargeTime += Time.deltaTime;

                if (ChargeTime > MaxChargeTime)
                    ChargeTime = MaxChargeTime;
            } 
        }
        else
            Yeet();

        if (Input.GetMouseButtonDown(1))
        {
            if (EquippedItem)
            {
                EquippedItem.Interact();
                return;
            }

            if (!IsPeakabooing)
                StartCoroutine(Peakaboo());
        }
    }

    IEnumerator Peakaboo()
    {
        IsPeakabooing = true;

        Hands.gameObject.SetActive(true);

        float timer = 0;
        while (timer < HandMoveDuration)
        {
            timer += Time.deltaTime;

            Hands.localPosition = Hands.localPosition + new Vector3(0, HandMoveSpeed, 0) * Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(HandHoldDuration);

        timer = 0;
        while (timer < HandMoveDuration)
        {
            timer += Time.deltaTime;

            Hands.localPosition = Hands.localPosition - new Vector3(0, HandMoveSpeed, 0) * Time.deltaTime;

            yield return null;
        }

        IsPeakabooing = false;

        Hands.gameObject.SetActive(false);

        if (_baby.CurrentChair)
        {
            if (_baby.CurrentChair.CompareTag("High Chair"))
                gameDirector.CompleteTask(7);
        }
    }

    public void Yeet()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (IsPickingUpItem)
            {
                IsPickingUpItem = false;
                return;
            }

            if (ChargeTime <= 0 || !EquippedItem)
                return;

            EquippedItem.Yeet(YeetForce * ChargeTime * Camera.transform.forward, LaunchPoint);

            EquippedItem = null;
            ChargeTime = 0;
        }
    }

    public bool TagIsInteractable(string tag)
    {
        for(int i = 0; i < interactableTags.tagList.Length; i++)
        {
            if(tag == interactableTags.tagList[i])
            {
                return true;
            }
        }

        return false;
    }
}