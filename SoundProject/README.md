
#### 유니티에서 오디오 넣기

사운드 리소스를 Hierarchy에 던져 넣으면 오디오 파일 삽입
플레이 시 사운드 재생


오디오 소스(Audio Source)
씬에서 오디오 클립을 재생하는 도구

재생을 위해서는 오디오 리스너/ 오디오 믹서를 통해서 재생 가능

믹서는 따로 만들어야 하며,
오디오 리스너는 메인 카메라에 붙어 있다
Main Camera의 Audio Listener 컴포넌트 언체크시 소리를 들을 수 없다

#### 오디오 믹서 컴포넌트의 property

Audio Resource : 재생을 진행할 사운드 클립
Output: 기본적으로 오디오 리스너에 직접 출력, 만든 오디오 믹서가 있다면 그 믹서를 통해 출력
Mute: 음소거 처리
Bypass Effects: 오디오 소스에 적용되어있는 필터 효과를 분리
Bypass Listener Effects: 리스너 효과 on/off
Bypass Reverb Zones: 리버브 존(리스너 위치에 따른 잔향 효과 제어 도구) on/off
Play On Awake: 씬이 실행되는 시점에 재생할지 여부를 on/off
		   비활성화 시 스크립트를 통해 사운드 재생
Loop: 반복 재생 on/off

Priority: 오디오 소스의 우선 순위(0이 최우선, 256이 최하위)

Volume: 리스너 기준으로 거리 기준 소리에 대한 수치
  컴퓨터 내 소리를 재생하는 여러 요소 중 하나를 제어

Pitch: 재생 속도 변화시 피치 변화량(1은 일반 속도, 최대 3배속)

Stereo Pan: 소리 재생 시 좌우 스피커 간 소리 분포 조절 가능(-1: 왼쪽 스피커 0: 균등 1: 오른쪽 스피커)

Spatial Blend: 0일 때 거리와 상관없이 일정한 사운드
    	           1일 때 사운드가 사운드 트는 도구의 거리에 따라 변화

Reverb Zone Mix: 리버브 존에 대한 출력 신호 양 조절(0은 영향x, 1은 최대 영향, 1.1은 10db정도 증폭)

리버브 존을 따로 설계해야 되는 상황
->동굴에서 소리가 울리는 효과, 건물에서 소리가 반사되어 울리는 효과



#### 3D Sound Settings

Doppler Level: 거리에 따른 사운드 표현(0: 없음, 1: 보통 5: 최대)

Spread: 사운드가 퍼지는 각도(0: 한 점에서 사운드 출력, 360: 모든 방향으로 균일하게 사운드 출력)

Rolloff Mode: 그래프 설정(로그, 선형, 커스텀), 마우스 드래그로 커스텀 가능


#### BGM 만들기

play on awake 끄기


#### 스크립트에서 오디오 소스 얻어오기

SFX: 효과음


UnityEngine.AudioSource - Unity 스크립팅 API


#### 오디오 스크립트
![image](https://github.com/user-attachments/assets/661fa7f2-9ab7-4805-9bd5-0a22998d2670)

3번 코드: play시 게임오브젝트 SFX에 객체 ausioSourceSFX를 자동으로 연결
4번 코드: play시 ausioSourceSFX에 연결된 SFX 객체의 Audio Clip 속성에 Resources 폴더 내 Explosion 사운드 클립 자동 연결




#### AudioSource 함수
![image](https://github.com/user-attachments/assets/9cb04733-9b41-49e6-a2d6-eeed22fce674)



#### 코루틴을 이용한 오디오 사용

bgm보다는 일시적으로 플레이하는 사운드 이펙트에 적합
값이 제거되었으므로 BGM의 오디오 리소스 값이 공란으로 바뀐다

#### 과제
UI를 눌러 bgm재생을 켜고 끄는 기능을 만들기
![image](https://github.com/user-attachments/assets/8f6caeed-a064-4d49-aef3-48a0714c515c)

UI 버튼의 OnClick()리스트에 BGM객체와 AudioSource.Play 기능 추가


BGM 객체에 새로 만든 오디오 믹서를 연결해 사운드 출력을 확인할 수 있다


마크다운을 이용한 깃허브 README 작성 

https://namu.wiki/w/%EB%A7%88%ED%81%AC%EB%8B%A4%EC%9A%B4

https://github.com/ShinJiYongyee/MyUnityRepository/blob/main/SoundProject/README.md


html과 유사한 마크다운 기능 지원


#### AudioUI에 등록해 UI내 버튼 이미지들을 리스트로 등록하는 코드
![image](https://github.com/user-attachments/assets/750667f7-41af-47bf-8761-6bffdd0733f2)

play시 
AudioUI의 빈 Boards 배열 내에 Board 이미지 객체 등록
스크립트에 따라 AudioSource 객체 생성
Audio Resource로 AudioUI가 보유 중인 bgm을 등록 후 재생
Output으로  AudioMixer를 등록해 음악의 재생을 확인


#### 재생되는 음악에 따라 움직이는 색깔 기둥 효과(비주얼라이저) 만들기
![image](https://github.com/user-attachments/assets/c1b2c7a6-bcce-472e-8c6a-4ee6532102ad)



5.0f는 변동폭을 증대하기 위한 보정값






#### 오디오 믹서
->오디오 소스에 대한 제어, 균형, 조정을 제공하는 도구

믹서 만들기: Create -> Audio -> AudioMixer를 통해 AudioGroup을 생성

#### 오디오 믹서 그룹 만들기
![image](https://github.com/user-attachments/assets/8d923709-0deb-4736-9c06-9f971aaa7279)

오른쪽의 믹서 인디케이터를 선택시 inspector의 Attention에서 각종 설정 조작 가능

Volume을 우클릭해 Volume을 스크립트상에 노출


노출된 매개변수의 이름을 알아보기 쉽게 변경(우클릭 후 Rename)



#### 볼륨 슬라이더 만들기
![image](https://github.com/user-attachments/assets/71db5e6e-4fcd-4c03-84bd-1d887580b1ea)

슬라이더 수치에 따라 볼륨이 늘거나 줄어야 한다


#### 슬라이더 
![image](https://github.com/user-attachments/assets/46397a82-22b2-4083-84c0-e6d5919f1cdf)

방향, 최대/최소값, 유니티 이벤트 리스트 등 설정

옵션 슬라이더 뿐 아니라 체력바 등 다양한 UI를 만들 수 있음


#### UI 스크립트 만들기

슬라이더는 UI
자동완성으로 만들어지는 슬라이더 조인트 2D의 경우 
Rigidbody 물리 제어를 받은 게임 오브젝트가 공간에서 선을 따라 미끄러지게 하는 설정을 할 때 사용


UI에 연결하는 코드는 이벤트 코드로서 구동되므로 Update()함수가 불필요하다
시작 전 호출되는 Awake를 통해 게임 시작 전에 준비해주자(사실상 Start()와 동일)


메소드 자동 생성을 통해 매개변수의 자료형을 파악할 수 있다


#### 볼륨 설정 함수
![image](https://github.com/user-attachments/assets/41cf1a64-fb26-420e-bc9f-9cd721db1e28)

        //오디오믹서가 보유한 매개변수(Expose)의 수치를 설정하는 기능
        //오디오믹서 내 그룹의 볼륨을 Expose해야 설정 가능
오디오 믹서의 최소-최대 볼륨 값 때문에 로그 함수 사용
최소 -80, 최대 0
수치 변경 시 Mathf.Log10(슬라이더 수치 * 20)을 통해 범위를 설정하고
슬라이더 최소 값이 0.0001일 경우 -80, 최대 값이 1일 경우 0 계산



#### 그룹의 볼륨 expose
![image](https://github.com/user-attachments/assets/e52f7e1c-0709-4b67-a7e3-0f2b021bfb06)
![image](https://github.com/user-attachments/assets/ed83e563-a8fc-4ad6-aefb-4eee9832e535)


노출된 Parameter명이 정확한지 확인


#### 자주 사용되는 Mathf 함수
![image](https://github.com/user-attachments/assets/2edac25d-7ebe-4fe0-afd3-bd89c5c269b9)

#### 유니티 에디터에서 GUI를 보여주는 시스템
![image](https://github.com/user-attachments/assets/d6f02dff-6377-475d-a316-5eff97c2c548)

UIElements는 일반 UI와 다른 시스템이므로 import시 주의 -> UI로 바꿀 것




#### 작업물 녹음하기(유니티 레코더)

![image](https://github.com/user-attachments/assets/4536de59-9147-47de-8ac7-e7e6fa29e93c)

Exit Play Mode: 녹화 종료시 플레이 종료
Recording Mode: 녹화 방법 선택
Playback: 녹화 중 프레임 설정(고정, 변동)
Add Recorder: 녹화 종류 선택



