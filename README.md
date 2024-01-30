## 😾 NO TIME TO SHOWER
Unity 입문 주차 팀프로젝트 3조, 못 먹어도 쓰리고의 팀 프로젝트입니다.

## 프로젝트 소개
### 씻기 싫어하는 고양이의 입장이 되어보자! 
고양이가 자신을 샤워 시키기 위해 쫓아오는 사람들을 무찔러서 일정 시간동안 생존하면 승리하는 게임입니다.

## 📅 개발 기간
24.01.24 ~ 24.01.30

## 😎 멤버 구성 및 역할 분담
- 금경희(팀장) : Player와 InputSystem 구현, 발표
- 김철우 : Game Logic 구현 및 작업물 Merge, 게임 종료 후 최고 점수 출력
- 정원우 : Enemy 생성 및 Prefabs 구현
- 송상화 : UI와 Audio 구현, 노션과 ReadMe 작성

## 🖥️ 개발 환경
- Visual Studio 2022
- C# .Net 8.0
- Unity 2022.3.2f1
- Github Desktop

## 실행 화면
### 시작 화면(StartScene)
#### 게임 타이틀
<img src="https://github.com/klik075/threeGoGame/assets/151727593/bd2c980e-91c8-44f5-aaa0-b1acd2e1a613" />

#### Player의 이름 입력
<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/151727593/300732857-b16ec3ff-5666-40e5-9f26-0b02471c3c7a.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20240130%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20240130T083258Z&X-Amz-Expires=300&X-Amz-Signature=e625c2a26914ec1a18c81c77a3e2fe4e1dc6738e6c48f91c66026248f04f8ebc&X-Amz-SignedHeaders=host&actor_id=151727593&key_id=0&repo_id=747972898" />

#### 음악, 효과음 볼륨 조절
<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/151727593/300733275-ff4aeb9c-9c98-44bd-8af5-52090e4cf240.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20240130%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20240130T083421Z&X-Amz-Expires=300&X-Amz-Signature=8426a9a71b44a61a2de3093e77cccc35842499c4d85dfabc7e66793f4f32b4b6&X-Amz-SignedHeaders=host&actor_id=151727593&key_id=0&repo_id=747972898" />

--------

### 메인 화면(MainScene)
#### 전투 화면
<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/151727593/300736371-bdefd1d3-5a52-4696-952f-a2f2f84352d0.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20240130%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20240130T084508Z&X-Amz-Expires=300&X-Amz-Signature=696f9da66430bc9c59eaba656c44ab1d02527bf5c0a363bdcbd0fedbfa9f6e88&X-Amz-SignedHeaders=host&actor_id=151727593&key_id=0&repo_id=747972898" />

#### 투사체 발사
<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/151727593/300737086-b616dd35-ff6f-4119-831c-99428ffc8150.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20240130%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20240130T084744Z&X-Amz-Expires=300&X-Amz-Signature=4be9daceea598e83a3bf64a119cd9dda78417495325696bd63f51387e7a77a02&X-Amz-SignedHeaders=host&actor_id=151727593&key_id=0&repo_id=747972898" />

#### 게임 종료
<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/151727593/300737532-c574e337-dd21-4913-9c80-74d1e236389a.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20240130%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20240130T084917Z&X-Amz-Expires=300&X-Amz-Signature=ad01a25a2c3a5c8a465526c123a9592ca63543ae008551958084e23f8817a223&X-Amz-SignedHeaders=host&actor_id=151727593&key_id=0&repo_id=747972898" />

-------

## 🛠️ 주요 기능
- ### 시작 화면(StartScene) :
  - 게임 타이틀, Player 이름 입력, 옵션 설정 및 게임 플레이
  
    
- ### 메인 화면(MainScene)
  - #### Player의 게임 실행 화면입니다.
  - Player, Enemy과 Tilemap의 구현
  
- ### 게임 로직
  - 게임 시작, 플레이어의 생존 여부에 따른 게임 종료, 플레이어와 적의 공격과 피격, 점수를 관리합니다.


### 🔬 세부 기능
- 플레이어, 적의 접촉 및 원거리 공격의 충돌 처리
- 플레이어의 키보드 입력 처리
- 마우스 방향에 따른 캐릭터의 조준점, 방향 변경
- 투사체 발사
- 플레이어 체력 게이지
- 플레이어와 적의 애니메이션
- 적 생성 시 플레이어 목표 설정 기능
- 플레이어 위치에 따른 적의 이동 방향, 스프라이트 회전 기능
- 경험치 획득과 레벨 업
- BGM과 SFX 조절 기능
- 게임 종료 시 최고 결과 1 ~ 3등 출력

## 👨‍💻 팀원 별 구현 기능
- 금경희
   #### Player 구현
  - Player의 애니메이션 설정
  - Player의 InputSystem에 따른 키보드와 마우스 입력 처리
  - Player의 마우스 방향에 따른 투사체 조준점 계산
  - Player의 마우스 위에 따른 캐릭터 스프라이트 회전
  - Player의 투사체 구현 및 Prefab화

- 김철우
  #### Game Logic 설계
  - Player와 Enemy의 스탯 설정
  - 게임의 종료 조건 설정
  - 플레이어의 레벨 업 구현
  - 플레이어의 체력 게이지 구현
  - 게임 종료 시 최고 점수 출력
  - TimeManager 구현을 통한 인게임 시간 관리
  - Enemy의 랜덤한 생성 위치 구현
  - Player의 이름 입력

- 정원우
   #### Enemy 구현
  - 공격 타입에 따른 Enemy 생성
  - Enemy의 애니메이션 설정
  - HealthSystem을 통해 Player와 Enemy의 체력 관리 구현
  - 원거리 공격 Enemy의 투사체 구현 및 Prefab화
  - Enemy 생성 시 Player의 위치에 따른 이동 방향과 스프라이트 회전

- 송상화
  #### UI, Audio 구현
  - StartScene 구현
  - MainScene의 Tilemap과 Tilemap Collider 추가
  - MainScene의 게임 진행 시간 출력
  - 옵션 메뉴의 구현.
  - 게임 내 음악과 효과음 출력
  - 옵션에서 게임 내 음악과 효과음의 볼륨 조절 기능
  - 게임 종료 시 출력되는 결과 화면 구현
  

## 📌 Reference
#### 사용 에셋 :
- Player : <https://opengameart.org/content/tiny-kitten-game-sprite>
- Enemy : <https://assetstore.unity.com/packages/2d/undead-survivor-assets-pack-238068>
- Tilemap : <https://pixelfrog-assets.itch.io/kings-and-pigs>, <https://limezu.itch.io/moderninteriors>
- StartScene Play Button : <https://cupnooble.itch.io/>, <https://cupnooble.itch.io/sprout-lands-ui-pack>
- GameEndPopup :
    - UI : <https://humblepixel.itch.io/pocket-inventory-series-5-player-status>
    - Button : <https://soulofkiran.itch.io/pixel-art-wooden-gui-v1>
- 폰트 : <https://tinyworlds.itch.io/free-pixel-font-thaleah>
- 배경음악 : <https://azakaela.itch.io/froggis-adventure>
- SFX : <https://bbunsik.itch.io/cooknightasset>
    
