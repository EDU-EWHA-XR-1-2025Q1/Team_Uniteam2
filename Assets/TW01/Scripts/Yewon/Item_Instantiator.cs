using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템을 랜덤한 위치에 복제하는 클래스입니다.
/// Author: Yewon
/// </summary>
public class Item_Instantiator : MonoBehaviour
{
    /// <summary>
    /// 복제할 대상 프리팹입니다.
    /// </summary>
    [Header("Prefab to clone")]
    public GameObject targetPrefab;

    /// <summary>
    /// 생성할 복제본 개수입니다.
    /// </summary>
    [Header("Number of clones")]
    public int numberOfClones = 10;

    private void Start()
    {
        SpawnItems();
    }

    /// <summary>
    /// 지정된 개수만큼 아이템을 랜덤한 위치에 생성하는 함수입니다.
    /// </summary>
    private void SpawnItems()
    {
        for (int i = 0; i < numberOfClones; i++)
        {
            Vector3 offset = Random.insideUnitSphere * 2.5f;
            offset.y = 0f;
            Vector3 spawnPosition = transform.position + offset;

            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            GameObject clone = Instantiate(targetPrefab, spawnPosition, randomRotation);
            clone.SetActive(true);
            clone.name = $"Clone_{i:D4}";
            clone.transform.SetParent(transform);
        }

        Debug.Log($"[Item_Instantiator] {numberOfClones} clones successfully created.");
    }
}