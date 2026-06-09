# 🚗 Data Manager : Autonomous Driving Data Integrated Workflow

![.NET](https://img.shields.io/badge/.NET-WinForms-512BD4?style=for-the-badge&logo=dotnet)
![Python](https://img.shields.io/badge/Python-3.x-3776AB?style=for-the-badge&logo=python)
![TensorFlow](https://img.shields.io/badge/TensorFlow-2.13.0-FF6F00?style=for-the-badge&logo=tensorflow)
![Ubuntu](https://img.shields.io/badge/WSL2-Ubuntu_24.04-E95420?style=for-the-badge&logo=ubuntu)

**Data Manager**는 Donkeycar 기반의 자율주행 차량에서 수집된 방대한 주행 데이터를 직관적으로 탐색 및 전처리하고, AI 모델 학습부터 결과 검증(Inference)까지의 전 과정을 단일 인터페이스에서 제어할 수 있는 **WinForms 기반 통합 데스크톱 애플리케이션**입니다. 

기존 CLI 환경에서 파편화되어 있던 데이터 정제와 모델 학습 파이프라인을 GUI로 통합하여, 자율주행 연구 및 개발의 생산성을 극대화하는 것을 목표로 설계되었습니다.

---

## 👥 프로젝트 팀 : 14조 (Team 14)

| 이름 | 학번 | 역할 | 담당 업무 및 GitHub |
| :--- | :--- | :--- | :--- |
| **황규희** | 25017095 | 팀장 / UI | • 기본 프레임워크 및 반응형(Responsive) 레이아웃 아키텍처 설계<br>• 핵심 인터페이스 이벤트 연동 및 데이터 상태 관리 |
| **박주원** | 25017039 | 데이터 담당 | • 로컬 주행 데이터 확보 및 무결성 검증 로직 구현<br>• `System.Text.Json` 기반 Catalog 파일 고속 파싱 및 객체 매핑 |
| **홍시언** | 25017093 | 기능 담당 | • GDI+ 삼각함수 연산을 통한 오버레이(Predict/Actual) 시각화<br>• 비동기 프로세스 제어를 통한 Python(WSL2) 파이프라인 연동<br>|

---

## 🛠 시스템 아키텍처 및 기술 스택

### Frontend (GUI & Visualization)
* **C# / .NET WinForms:** 애플리케이션 기본 런타임 및 GUI 프레임워크
* **GDI+ (Graphics Device Interface):** 주행 영상 위 제어값 오버레이 실시간 커스텀 렌더링
* **MS Chart Controls (`System.Windows.Forms.DataVisualization`):** 시계열 데이터(Angle, Throttle) 추이 및 모델 성능 비교 차트

### Backend & AI (Training & Inference)
* **Python 3.x:** 모델 학습 및 검증 스크립트 실행
* **TensorFlow / Keras:** 자율주행 엔드투엔드(E2E) 조향/속도 예측 딥러닝 모델(`mypilot.h5`) 구현
* **Albumentations / Imgaug:** 이미지 데이터 증강(Data Augmentation) 및 전처리

### Environment & Interop
* **WSL2 (Windows Subsystem for Linux):** `wsl.exe` 서브프로세스 호출을 통한 Linux 파일 시스템 및 연산 자원 활용
* **Conda (`e2e_env`):** 패키지 의존성이 격리된 안전한 Python 가상환경

---

## ✨ 핵심 기능 상세 가이드

### 1. 📂 스마트 데이터 탐색 및 고속 무결성 검증
* **고속 카탈로그 파싱:** 수십~수백 개의 `.catalog` JSON 파일을 `StrCmpLogicalW` API를 통해 논리적 순서로 정렬하여 한 번에 메모리에 로드(`List<DrivingData>`)합니다.
* **사전 무결성 검사 (Integrity Check):** 학습을 돌리기 전, 중복된 인덱스가 존재하거나 카탈로그에 기록된 이미지 파일 경로가 실제 디스크에 없는 경우(Missing file)를 스캔하여 경고 메시지를 출력합니다.
* **다이나믹 재생 컨트롤러:** `System.Windows.Forms.Timer`를 활용하여 프레임 단위 탐색은 물론, 트랙바 조작을 통해 **0.25배속부터 4.0배속**까지 동적 재생 속도 조절 및 역재생 기능을 지원합니다.

### 2. ✂️ 정밀한 데이터 전처리 및 안전한 복구(Undo) 시스템
* **노이즈 데이터 스마트 필터링:** 조향값과 속도가 모두 0인(차량이 조작 없이 멈춰있는) 무의미한 프레임을 배열에서 찾아내 원클릭으로 일괄 제거하여 모델의 오버피팅을 방지합니다.
* **마커(Marker) 기반 구간 삭제:** 탐색 바 위에 시각적인 마커를 2개 배치하여 특정 주행 구간(Start~End)을 설정하고 한 번에 날려버릴 수 있습니다. ListView에서의 다중 선택(드래그) 삭제도 지원합니다.
* **스택 기반 안전 복구 (Safe Rollback):** 데이터를 삭제할 때마다 원본 JSON 카탈로그와 `_manifest` 파일을 `.datamanager_catalog_backups`라는 로컬 숨김 폴더에 자동 백업합니다. 메모리 상에는 `Stack<DeleteAction>` 구조를 활용해 언제든 직전 상태로 완벽히 복구(Undo)할 수 있는 무결성을 보장합니다.

### 3. 🧠 WSL 연동을 통한 끊김 없는 AI 모델 학습 (Async Process)
* **원클릭 WSL 파이프라인:** WinForms 내에서 `System.Diagnostics.Process`를 통해 `wsl.exe`를 백그라운드로 호출합니다. I/O 병목을 줄이기 위해 Windows의 데이터를 `.zip`으로 압축하여 WSL로 넘긴 뒤, 압축을 풀고 `train.py`를 실행합니다.
* **실시간 로그 정규식 파싱:** 학습 진행 상황을 보기 위해 터미널을 열 필요가 없습니다. Python 프로세스의 표준 출력(Stdout)을 가로채어 정규표현식(Regex)으로 파싱하고, GUI 라벨에 **Epoch, Loss, Validation Loss, 모델 개선 상태(Status)** 를 실시간으로 렌더링합니다.

### 4. 📊 GDI+ 렌더링을 활용한 시각적 예측 검증
* **제어값 오버레이 (Control Bars):** 훈련된 모델로 테스트를 돌리면, 실제 차량 화면(PictureBox) 하단에 GDI+ 삼각함수 연산을 통해 **실제 조향/속도(초록색 막대)** 와 **AI의 예측 조향/속도(파란색 막대)** 를 겹쳐 그립니다. 선의 각도(Angle)와 길이(Throttle)로 모델의 정확도를 눈으로 직접 비교할 수 있습니다.
* **시계열 차트 시각화:** 전체 주행 트랙에 대한 실제값과 예측값의 변동 추이를 꺾은선 그래프로 겹쳐서 시각화하여, 특정 코너링 구간에서 모델이 얼마나 오차를 보이는지 한눈에 분석할 수 있습니다.

---

## 🔥 트러블슈팅 및 디버깅 기록 (Troubleshooting)

프로젝트 고도화 과정에서 마주친 주요 기술적 한계와 극복 과정입니다.

### 💡 1. 비동기 로그 수신 시 Cross-Thread UI 업데이트 예외
* **문제 상황:** WSL에서 실행 중인 Python 프로세스의 출력 결과를 실시간으로 GUI 텍스트박스와 라벨에 업데이트하기 위해 `OutputDataReceived` 이벤트를 사용했습니다. 그러나 이 이벤트는 백그라운드 워커 스레드에서 트리거되므로, 여기서 직접 UI 컨트롤에 접근하면 `Cross-thread operation not valid` 예외가 발생했습니다.
* **해결 방법:** 폼이 초기화되는 메인 스레드 시점에 `SynchronizationContext.Current`를 변수(`_uiContext`)에 캡처해 두었습니다. 이후 백그라운드에서 로그가 들어올 때마다 `_uiContext.Post()`를 호출하여 UI 업데이트 로직을 메인 스레드로 안전하게 위임(Dispatch)하는 방식으로 해결했습니다.

### 💡 2. 이미지 파일 점유(File Lock) 현상으로 인한 삭제 에러
* **문제 상황:** 사용자가 데이터를 보면서 불필요한 프레임을 삭제하려 할 때, `PictureBox.Image = Image.FromFile()` 방식을 사용하면 GDI+가 해당 이미지 파일에 OS 레벨의 잠금(Lock)을 걸어버려 "파일이 사용 중이므로 삭제할 수 없습니다"라는 에러가 났습니다.
* **해결 방법:** 디스크의 파일을 직접 물고 있지 않도록 `File.ReadAllBytes`를 통해 이미지를 바이트 배열로 먼저 읽어오고, 이를 `MemoryStream`에 담아 `Bitmap` 객체로 생성하는 `LoadImageWithoutLock()` 커스텀 메서드를 구현하여 파일 점유 문제를 완벽히 우회했습니다. 프레임이 바뀔 때는 이전 이미지를 명시적으로 `Dispose()`하여 메모리 누수도 차단했습니다.

### 💡 3. Windows 환경과 Linux(WSL) 간의 파일 경로 불일치
* **문제 상황:** C# 앱은 Windows(`C:\Users\...`)에서 동작하지만, 모델이 돌아가는 Python 코드는 WSL(Ubuntu)에서 동작합니다. 따라서 Python 스크립트에 Windows 경로를 그대로 넘기면 파일을 찾지 못해 크래시가 발생했습니다.
* **해결 방법:** 두 가지 전략을 사용했습니다.
  1. 단순한 드라이브 경로는 정규식을 이용해 Windows 경로를 WSL 마운트 경로로 변환하는 `ConvertWindowsPathToWslPath()` 함수(`C:\` -> `/mnt/c/`)를 직접 구현했습니다.
  2. 복잡한 애플리케이션 내부 경로 매핑이 필요할 때는, 프로세스를 열어 리눅스 내장 명령어인 `wsl wslpath '{경로}'`를 비동기로 실행시켜 정확한 WSL 절대 경로를 반환받는 스마트한 방식을 적용했습니다. 반환된 경로를 바탕으로 C#에서 Python 추론 스크립트(`predict_frames.py`)를 동적으로 생성하여 실행합니다.

### 💡 4. 외부 프로세스 대기로 인한 메인 UI 프리징(Freezing)
* **문제 상황:** AI 학습이나 테스트 버튼을 누르면 Python 스크립트가 끝날 때까지 기다리기 위해 사용한 `process.WaitForExit()`가 WinForms의 메인 스레드를 블로킹하여, 프로그램이 하얗게 굳어버리는 "응답 없음" 상태가 되었습니다.
* **해결 방법:** 이벤트 핸들러 서명을 `async void`로 변경하고, C#의 비동기 프로그래밍 모델을 적용하여 `await process.WaitForExitAsync()`로 대기 방식을 변경했습니다. 이를 통해 무거운 딥러닝 연산이나 파일 압축(ZipFile) 작업이 진행되는 동안에도 사용자가 다른 UI(데이터 스크롤, 차트 확인 등)를 부드럽게 조작할 수 있는 논블로킹(Non-blocking) 환경을 구축했습니다.