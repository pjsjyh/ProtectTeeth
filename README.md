# penguinAdventure

![image](https://github.com/user-attachments/assets/40209d8e-b6f8-4875-bba2-e1a36d944c1f)


# 📄프로젝트 정보
#### 장르
2D 디펜스 게임 - 치아를 공격하러 오는 세균을 막는 디펜스 게임.

#### 참여인원
개발자 1인

#### 실행 영상
https://www.youtube.com/watch?v=bT7H7SjwhPc



# 📝사용기술 및 구현 기능
1) 디자인 패턴을 활용한 관리
   - 싱글톤 패턴을 활용한 플레이어 데이터 관리
     - 플레이어가 한명인것을 활용해 싱글톤 패턴을 활용해 데이터 관리. [플레이어 스크립트](https://github.com/pjsjyh/ProtectTeeth/blob/master/ProtectTeeth/Assets/Scripts/PlayerSetting.cs)
   - 상태 패턴을 활용해 게임 관리
     - enum과 switch를 활용한 게임 관리 [Gamemanager 스크립트](https://github.com/pjsjyh/ProtectTeeth/blob/master/ProtectTeeth/Assets/Scripts/GameManager.cs)
2) ScriptableObject를 이용한 데이터 관리
   -  인스펙터에서 쉽게 설정할 수 있도록 제작. [몬스터 데이터](https://github.com/pjsjyh/ProtectTeeth/blob/master/ProtectTeeth/Assets/Scripts/Game/Zombie.cs) 
3) 메모리 관리 최적화
   - 오브젝트 풀링을 활용한 몬스터 생성 및 재활용. [몬스터 생성](https://github.com/pjsjyh/ProtectTeeth/blob/master/ProtectTeeth/Assets/Scripts/GamePlayScene/ObjectPool.cs)
   - Addressables를 활용한 메모리 절감.
   - 감지 기능만 활용하기 위해 OverlapBoxNonAlloc 사용. [몬스터 감지](https://github.com/pjsjyh/ProtectTeeth/blob/master/ProtectTeeth/Assets/Scripts/Game/GoodBoxSetting.cs)
4) 효율적인 코드 구성
   - 인터페이스, 상속을 활용해 코드 분리 및 유지보수성 향상 [상속을 이용한 공격코드](https://github.com/pjsjyh/ProtectTeeth/blob/master/ProtectTeeth/Assets/Scripts/Game/GoodTeethSetting.cs) [공통 코드](https://github.com/pjsjyh/ProtectTeeth/blob/master/ProtectTeeth/Assets/Scripts/Game/GoodSetting.cs)
   - static을 활용해 데이터 관리
   - 공통 로직 묶어 활용도 향상
5) 이미지 사용
   - Aseprite를 이용해 직접 이미지 제작
   - sprite sheet로 제작해 메모리 절감
   - Atlas를 이용해 리소스 관리
