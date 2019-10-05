using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 오브젝트의 위치가 도형안에 있는지 검사하는 클래스
/// </summary>
public class InsideChecker : MonoBehaviour
{
    private static readonly Vector3 CHECK_DIR = Vector3.one.normalized;

    public Text testResultText;

    void Start()
    {
        
    }

    void Update()
    {
        // 매 프레임 검사
        TestAndShowMessage();
    }

    /// <summary>
    /// <p>현재 정점(위치)이 어떤 도형 안에 존재하는지 검사한다.</p>
    /// <br/>
    /// <p>광선 추적 시 충돌 횟수가 홀수면 true, 짝수면 false 리턴</p>
    /// </summary>
    /// <returns>현재 위치가 도형 안에 존재하는가</returns>
    private bool CheckIfInside()
    {
        Ray ray = new Ray(this.transform.position, CHECK_DIR);
        RaycastHit hit;
        int count = 0;

        while (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            ray.origin = hit.point + ray.direction * 0.01f;
            count++;

            // For Debug
            //GameObject test = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //test.transform.position = hit.point
        }

        if (count % 2 == 0)
            return false;
        else
            return true;
    }

    /// <summary>
    /// <p>현재 위치가 도형 안에 존재하는지 검사하고, 결과를 UI에 보여준다.</p>
    /// <br/>
    /// <p>테스트 결과를 표시할 멤버: <see cref="testResultText"/></p>
    /// </summary>
    public void TestAndShowMessage()
    {
        bool result = CheckIfInside();

        testResultText.text = result ? "Inside" : "Outside";
    }
}
