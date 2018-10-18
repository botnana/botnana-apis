# 前言

botnanacs 為 C# 的範例程式，開發工具式採用 Microsoft Visual Studio Community 2017。

# 目錄結構

    botnanacs
    |-----> BotnanaApi
    |-----> SingleDrive
    |-----> TouchProbe

* BotnanaApi: 將 C 語言函式庫 `botnana.lib` 轉換為 C# 使用的動態連結檔 `BotnanaApi.dll` 。
* SingleDrive: 一個單軸馬達驅動器的測試範例。
* TouchProbe: 馬達驅動器 Touch Probe Function 的測試範例。

# Botnana API
  
編譯 BotnanaAPI 專案需要 C 語言標頭擋 `botnana.h` 與靜態連結檔 `botnana.lib`。 `botnana.h` 從同一個 Git 儲存庫的目錄 `botnanac\src` 中取得。 `botnana.lib` 可以由以下連結位置取得，或是參考 `botnanac\readme.md` 文件自行產生。

* 32 位元 Windows: [https://drive.google.com/drive/u/0/folders/1Vmy9aWYeTMhvJDM3W7UwKuqG4SfyA_n7](https://drive.google.com/drive/u/0/folders/1Vmy9aWYeTMhvJDM3W7UwKuqG4SfyA_n7)
* 64 位元 Windows: [https://drive.google.com/drive/u/0/folders/1sGibKjsuhkt0SMJ1w7id1XlOnoYKyD_W](https://drive.google.com/drive/u/0/folders/1sGibKjsuhkt0SMJ1w7id1XlOnoYKyD_W)

將 `botnana.h` 與 `botnana.lib` 放到 `botnanacs\BotnanaApi\BotnanaApi` 目錄下就可以完成編譯出 `BotnanaApi.dll`。產出 `BotnanaApi.dll` 的目錄位置依編譯的組態設定，通常會在以下的目錄:

* `botnanacs\BotnanaApi\Release`
* `botnanacs\BotnanaApi\Debug`
* `botnanacs\BotnanaApi\x64\Release`
* `botnanacs\BotnanaApi\x64\Debug`

此 BotnanaAPI 專案還需要 `Ws2_32.lib` 與 `Userenv.lib` 函式庫，在此專案的設定檔中已將這兩個函式庫的連結設置完成，如果有自行轉換函式庫的需求，要增加這兩個函式庫的連結設定，設定的方式可以參考 `botnanac\readme.md`。

# Single Drive

![](CS-SingleDrive.PNG) 

使用此範例的前提是第一個 EtherCAT 從站必須是馬達驅動器。

此範例有以下功能:

1. 顯示目前連線的 EtherCAT 從站的連線數。
2. 顯示馬達驅動器的狀態，包含 Drive ON/OFF/Fault, ORG, NEG/POS Limit, Operation Mode, Position 資訊。
3. Drive Control，包含 Drive ON/OFF, Reset Fault。
4. Drive P2P，在驅動器的 PP 模式下進行單點運動。
5. Real Time Program P2P，在驅動器的 PP 模式搭配程式背景執行的功能，進行 3 個目標點的連續運動。 

編譯與執行此專案需要以下兩個檔案:

* BotnanaApi.h : 在 `botnanacs\BotnanaApi\BotnanaApi` 的目錄中可以取得。
* BotnanaApi.dll : 可以由以下連結位置取得，或是參考 Botnana APi 章節自行產生。

* 32 位元 Windows: [https://drive.google.com/drive/u/0/folders/1MAZg9XcLLQ8UlemvOaPnnRXnui_YJEMV](https://drive.google.com/drive/u/0/folders/1MAZg9XcLLQ8UlemvOaPnnRXnui_YJEMV)
* 64 位元 Windows: [https://drive.google.com/drive/u/0/folders/1IZZ1QGJf2xVUvhGGWILW0t5WpiyTczz6](https://drive.google.com/drive/u/0/folders/1IZZ1QGJf2xVUvhGGWILW0t5WpiyTczz6)

將 `BotnanaApi.h` 與 `BotnanaApi.dll` 放到 `botnanacs\SingleDrive\SingleDrive` 目錄下就可以編譯與執行該範例。

# Touch Probe

![](CS-TouchProbe.PNG)

此範例用來測試馬達驅動器 Touch Probe 的功能。使用此範例的前提是第一個 EtherCAT 從站必須是馬達驅動器。

通常符合 CiA402 的馬達驅動器都支援 Touch Probe 的功能，Touch Probe 功能可以利用數位輸入訊號來觸發截取位置的事件。此範例採用 EtherCAT SDO Request 來設定與讀取 Touch Probe 功能相關的暫存器，相關的暫存器如下(以 Panasonic A6B 驅動器內容為例): 

| Index | Sub-index | Name | Data Type | Access |
|-------|-----------|-------|----------|-------| 
| 60B8h | 00h  | Touch probe function | U16  | rw |
| 60B9h | 00h  | Touch probe status   | U16  | ro |
| 60BAh | 00h  | Touch probe pos1 pos value (positive edge)  | I32 | ro |
| 60BBh | 00h  | Touch probe pos1 neg value (negative edge) | I32 | ro |
| 60BCh | 00h  | Touch probe pos2 pos value (positive edge)  | I32 | ro |
| 60BDh | 00h  | Touch probe pos2 neg value (negative edge) | I32 | ro |
    
**Touch probe function bit description (60B8h)**

| Bit | Description |
|-----|-------------|
| 0 | 0 : Switch off touch probe 1 <br> 1 : Enable touch probe 1 |
| 1 | 0 : Trigger first event <br> 1 : Continuous   |
| 2 | 0 : Trigger with touch probe 1 input <br> 1 : Trigger with 0 impulse signal of position encoder |
| 3 | -- | -- |
| 4 | 0 : Switch off sampling at positive edge of touch probe 1 <br> 1 : Enable sampling at positive edge of touch probe 1 |
| 5 | 0 : Switch off sampling at negative edge of touch probe 1 <br> 1 : Enable sampling at negative edge of touch probe 1 |
| 6-7 | -- | -- |
| 8 | 0 : Switch off touch probe 2 <br> 1 : Enable touch probe 2 |
| 9 | 0 : Trigger first event <br> 1 : Continuous   |
| 10 | 0 : Trigger with touch probe 2 input <br> 1 : Trigger with 0 impulse signal of position encoder |
| 11 | -- | -- |
| 12 | 0 : Switch off sampling at positive edge of touch probe 2 <br> 1 : Enable sampling at positive edge of touch probe 2 |
| 13 | 0 : Switch off sampling at negative edge of touch probe 2 <br> 1 : Enable sampling at negative edge of touch probe 2 |
| 14-15 | -- | -- |

**Touch probe status bit description (60B9h)**

| bit | Description |
|-----|-------------|
| 0 | 0 : Switch off touch probe 1 <br> 1 : Enable touch probe 1 |
| 1 | 0 : Trigger first event <br> 1 : Continuous   |
| 2 | 0 : Trigger with touch probe 1 input <br> 1 : Trigger with 0 impulse signal of position encoder |
| 3 | -- | -- |
| 4 | 0 : Switch off sampling at positive edge of touch probe 1 <br> 1 : Enable sampling at positive edge of touch probe 1 |
| 5 | 0 : Switch off sampling at negative edge of touch probe 1 <br> 1 : Enable sampling at negative edge of touch probe 1 |
| 6-7 | -- | -- |
| 8 | 0 : Switch off touch probe 2 <br> 1 : Enable touch probe 2 |
| 9 | 0 : Trigger first event <br> 1 : Continuous   |
| 10 | 0 : Trigger with touch probe 2 input <br> 1 : Trigger with 0 impulse signal of position encoder |
| 11 | -- | -- |
| 12 | 0 : Switch off sampling at positive edge of touch probe 2 <br> 1 : Enable sampling at positive edge of touch probe 2 |
| 13 | 0 : Switch off sampling at negative edge of touch probe 2 <br> 1 : Enable sampling at negative edge of touch probe 2 |
| 14-15 | -- | -- | 


編譯與執行此專案仍需要 `BotnanaApi.h` 與 `BotnanaApi.dll`，請參考 Single Drive 章節。