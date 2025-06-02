# 치과는 싫어

![image](https://github.com/user-attachments/assets/40209d8e-b6f8-4875-bba2-e1a36d944c1f)


# 📄프로젝트 정보
#### 장르
2D 디펜스 게임 - 치아를 공격하러 오는 세균을 막는 디펜스 게임.

#### 참여인원
개발자 1인

#### 실행 영상
https://youtu.be/G8Vo-QkZw0g



# 📝사용기술
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



# 구현 기능
#### 1) Tilemap 제작
#### 2) 인벤토리 시스템
 ####  ![image](https://github.com/user-attachments/assets/636399d3-742d-4d4b-9926-55fd00eb7e53)
   가지고 있는 아이템 리스트가 우측 화면에 배치. 길이가 길어지면 자동 스크롤.
   아이템을 클릭하면 좌측 상단의 아이템 리스트에 배치된다.
#### 3) 라운드 제작
####   ![image](https://github.com/user-attachments/assets/ddd3e185-a0f2-455e-b37b-cd651dfc1849)
   라운드로 진행되는 게임.
   각 라운드는 이전 라운드를 클리어 하지 못하면 진행되지 않는다.
#### 4) 마우스 클릭으로 오브젝트 배치
####   ![image](https://github.com/user-attachments/assets/cd8b466d-e34b-45ea-8872-0756f14612eb)
   좌측상단 오브젝트를 클릭해 타일맵에 배치. 우측상단 코인을 사용하여 구매가 가능하다.
   이미 설치되어 있는곳이 아니라면 어디든 설치가 가능하며 각 오브젝트들 마다 공격 타입이 다르다.
#### 5) 방어 오브젝트 제작
   - 버블 타입
 ####    -  ![image](https://github.com/user-attachments/assets/a4fa6d92-ba93-4781-a05d-9d666cba5fc6)
     -  방울을 쏘아 공격하는 타입. 일정 거리 내에 몬스터가 감지되면 공격을 시작한다. 감지의 경우 OverlapBoxNonAlloc를 사용해 몬스터를 파악한다.
     -  범위 내 몬스터가 모두 소멸 시 공격 중단.
   - 폭탄 타입
####     - ![image](https://github.com/user-attachments/assets/315db01d-7a12-4053-8362-637d3c0bc041)
     - 일정 범위 내의 몬스터에게 피해을 입힌다. 방어형 폭탄으로 HP가 0으로 감소하면 폭탄의 효과를 나타낸다.
     - 공격 범위는 코드 내에서 설정.
   - 방어 타입
    - 램프
####   -![image](https://github.com/user-attachments/assets/ebc397c3-fad1-4611-941c-ffd3bf4e38f0)

        
        어두운 밤 라운드 일 때 시야를 확보하기 위한 오브젝트
        
        (우측 하단 라이트는 마우스로 클릭 후 이동 중)
#### 6) 공격 오브젝트 생성
####   ![image](https://github.com/user-attachments/assets/40132db5-b1aa-444c-b27f-085faac11a15)
   방어 오브젝트를 마주치면 공격 시작한다. 피격시 색을 깜빡여 피격 효과를 나타낸다.
#### 7) 게임 오버
#### ![image](https://github.com/user-attachments/assets/ed15011d-7e12-44f3-9c9f-78d254838230)
   - 게임 클리어(몬스터 모두 사망)
     - 다음 라운드를 도전 할 수 있다. 점수의 일부를 얻게 된다.
   - 게임 실패(치아 파괴)
     - 다음 라운드를 도전 할 수 없다. 
