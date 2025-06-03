using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSJ_ItemContainer : MonoBehaviour
{
    /// <summary>
    /// The clone count
    /// </summary>
    public int cloneCount = 10;
    /// <summary>
    /// The target object
    /// </summary>
    public GameObject TargetObject;
    /// <summary>
    /// Starts this instance.
    /// </summary>
    void Start()
    {
        InstantiateSugar();
    }

    /// <summary>
    /// Instantiates the sugar.
    /// </summary>
    void InstantiateSugar()
    {
        for (int i = 0; i < cloneCount; i++)
        {
            Vector3 randomSphere = Random.insideUnitSphere * 2.5f; //기준점을 중심으로 구 영역을 만들고 영역 내 임의의 좌표값
            //*2.5f는 구의 크기를 5배 늘림
            randomSphere.y = 0f; //y값은 0으로 통일 

            //cf. insideUnitCircle은 z값 존재 x --> z값 쓰려면 입체인 sphere 써야됨

            Vector3 randomPos = randomSphere + transform.position; //insideUnitSphere의 기준점을 원점이 아닌 게임오브젝트로 설정(transform.position)


            float randomAngle = Random.value * 360f;
            Quaternion randomRot = Quaternion.Euler(0, randomAngle, 0); //y축으로만 randomAngle만큼 회전하기


            GameObject clone = Instantiate(TargetObject, randomPos, randomRot);

            clone.SetActive(true); //게임 시작하면 비활성화 풀기

            clone.name="Clone-"+string.Format("{0:D4}",i); //{0:D4}-> 0001, 0002 이렇게 자릿수 맞춰줌

            clone.transform.SetParent(transform); //clone의 부모를 transform(이 스크립트가 장착되어 있는 오브젝트)으로 설정
        }
    }
}
