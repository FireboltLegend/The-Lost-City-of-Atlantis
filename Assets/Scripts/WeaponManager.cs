using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject spear;
    [SerializeField] private GameObject trident;
    [SerializeField] private GameObject harpoon;
    [SerializeField] private GameObject harpoonSpear;
    [SerializeField] private GameObject landMine;
    private int currentWeapon = 1;
    private KeyCode[] numKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (rb.velocity.x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }

        if (playerController.swimming)
        {
            transform.localPosition = new Vector3(0.1f, 0.4f, 0);
            transform.localEulerAngles = new Vector3(0, transform.eulerAngles.y, 90);
        }
        else
        {
            transform.localPosition = new Vector3(0.1f, 0.15f, 0);
            transform.localEulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(numKeys[i]))
                currentWeapon = i + 1;
        }

        harpoon.SetActive(false);
        switch (currentWeapon)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    StopCoroutine(SwingSword());
                    StartCoroutine(SwingSword());
                }
                break;
            case 2:
                harpoon.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    GameObject newSpear = Instantiate(harpoonSpear, transform.position, Quaternion.identity);
                    newSpear.GetComponent<Rigidbody2D>().velocity = harpoon.transform.right * 20;
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.F) && !spear.activeInHierarchy)
                {
                    StopCoroutine(JabSpear());
                    StartCoroutine(JabSpear());
                }
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.F) && !trident.activeInHierarchy)
                {
                    StopCoroutine(JabTrident());
                    StartCoroutine(JabTrident());
                }
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.F))
                    Instantiate(landMine, transform.position, Quaternion.identity);
                break;
        }
    }

    IEnumerator SwingSword()
    {
        Quaternion swordRotation = sword.transform.localRotation;
        sword.SetActive(true);
        float timer = 0;
        while (timer < 0.25f)
        {
            timer += Time.deltaTime;
            sword.transform.Rotate(Vector3.back * Time.deltaTime * 700);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        sword.SetActive(false);
        sword.transform.localRotation = swordRotation;
        StopCoroutine(SwingSword());
    }

    IEnumerator JabSpear()
    {
        Vector3 spearPos = spear.transform.localPosition;
        spear.SetActive(true);
        float timer = 0;
        while (timer < 0.3f)
        {
            timer += Time.deltaTime;
            spear.transform.localPosition = spearPos + new Vector3(1, 0, 0) * Mathf.Sin(timer * 10);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        spear.SetActive(false);
        spear.transform.localPosition = spearPos;
        StopCoroutine(JabSpear());
    }

    IEnumerator JabTrident()
    {
        Vector3 tridentPos = trident.transform.localPosition;
        trident.SetActive(true);
        float timer = 0;
        while (timer < 0.9f)
        {
            timer += Time.deltaTime;
            trident.transform.localPosition = tridentPos + new Vector3(1, 0, 0) * Mathf.Sin(timer * 4);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        trident.SetActive(false);
        trident.transform.localPosition = tridentPos;
        StopCoroutine(JabTrident());
    }
}
