using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D13_Item_Container : MonoBehaviour
{
    public GameObject Target;
    public int cloneCount = 10; //몇 개 clone할 지


    private void Start()
    {
        Instantiate_GameObject();
    }

    void Instantiate_GameObject() //for 반복문을 이용해서 clone 복제
    {
        for (int i = 0; i < cloneCount; i++)
        {
            //복제한 클론의 위치 지정
            Vector3 randomSphere = Random.insideUnitSphere * 2.5f; //insideUnitSphere: 1 unit 이내에서 랜덤한 위치값이 Vector3 타입의 변수에 저장됨
            randomSphere.y = 0f; //높이는 0으로
            Vector3 randomPos = randomSphere + transform.position; //기본적으로는 랜덤한 값이 world의 원점을 기준으로 생성되는데 item의 위치를 기준으로 생성되도록
            
            //복제한 클론의 r각도 지정       
            float randomAngle = Random.value * 360f;
            Quaternion randomRot = Quaternion.Euler(0, randomAngle, 0);
            GameObject Clone = Instantiate(Target, randomPos, randomRot);

            Clone.SetActive(true);
            Clone.name = "Clone-"+string.Format("{0:D4}",i); //1일때 0001로 formating되어 이름이 붙여짐

            Clone.transform.SetParent(transform); 
        }
    }
}
