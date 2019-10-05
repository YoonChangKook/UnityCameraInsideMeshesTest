# Camera Inside Meshes Test

사용자의 카메라가 매쉬 안에 있는지 검사하는 테스트

### Idea
한 방향으로 광선을 발사했을 때, 부딪힌 횟수가 홀수면 inside, 짝수면 outside로 판단하는 아이디어를 3D로 확장함.

3D 도형이 닫힌 도형이라면 2차원에서의 아이디어를 그대로 사용할 수 있음

* How to check if a given point lies inside or outside a polygon?

https://www.geeksforgeeks.org/how-to-check-if-a-given-point-lies-inside-a-polygon/

이번 테스트에선 Unity 엔진의 Raycast를 이용하여 테스트함.

### Make Backface Mesh
Raycast로 정점이 도형안에 있는지 검사하기 위해선, 도형의 안쪽에도 Mesh가 구성돼있어야 검사가 가능하다.

본 프로젝트에선 MeshDuplicater 클래스가 이 역할을 수행한다.

### Test
플레이어의 카메라가 도형 안에 있는지 리얼타임으로 테스트한다. 화면 좌측 상단에 실시간 테스트 결과가 표시되며, 테스트를 위해 구성한 내용은 아래와 같다.

* Scene

SimpleScene에 다양한 매쉬 오브젝트를 배치함

* Camera

WASD 키를 이용하여 카메라를 이동, 마우스 우클릭 후 드래그를 하여 카메라를 회전한다.
