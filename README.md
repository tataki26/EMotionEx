### MCS UDP 프로그램(Marshalling ver.)

1. 특정 접점의 주소와 데이터 값을 입력 받으면 네트워크 프레임을 생성한다
2. 접점 종류: Q(W), L(W/R), I(R)
3. Write 접점은 사용자로부터 주소와 데이터를 입력 받는다
4. Read 접점은 ComboBox에서 주어진 주소를 입력 받는다
5. Write 될 때, 실시간으로 변경된 데이터를 받는다
6. EMotionMcsDevice.dll marshalling 적용