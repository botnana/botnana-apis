# 前言

botnanac++builder 為 C++ Builder 的範例程式，開發工具式採用 Embarcadero® C++Builder 10.2。

# 目錄結構

    botnanabcb
    |-----> SingleDrive
    
* SingleDrive: 一個單軸馬達驅動器的測試範例。

C++ builder 範例需要引用兩個檔案 `BotnanaApi.h` 與 `BotnanaApiBCB.lib`      

**BotnanaApi.h**

可以由在 `botnana-api/botnanacs/BotnanaApi/BotnanaApi` 目錄中取得。

**BotnanaApiBCB.lib**:

要先取得 `BotnanaApi.dll`，請參考 `botnana-api/botnanacs/readme.md` 或是在以下網址取得

* 32 位元 Windows: [https://drive.google.com/drive/u/0/folders/1Vmy9aWYeTMhvJDM3W7UwKuqG4SfyA_n7](https://drive.google.com/drive/u/0/folders/1Vmy9aWYeTMhvJDM3W7UwKuqG4SfyA_n7)
* 64 位元 Windows: [https://drive.google.com/drive/u/0/folders/1sGibKjsuhkt0SMJ1w7id1XlOnoYKyD_W](https://drive.google.com/drive/u/0/folders/1sGibKjsuhkt0SMJ1w7id1XlOnoYKyD_W)

取得 `BotnanaApi.dll` 後要利用 C++ Builder 工具 implib 來轉換函式庫格式，其命令如下:

    implib -a vclib.lib botnana-api.dll


# Single Drive

![](CS-SingleDrive.PNG) 

**使用此範例的前提是第一個 EtherCAT 從站必須是馬達驅動器。**

此範例有以下功能:

1. 顯示目前連線的 EtherCAT 從站的連線數。
2. 顯示馬達驅動器的狀態，包含 Drive ON/OFF/Fault, ORG, NEG/POS Limit, Operation Mode, Position 資訊。
3. Drive Control，包含 Drive ON/OFF, Reset Fault。
4. Drive P2P，在驅動器的 PP 模式下進行單點運動。
5. Real Time Program P2P，在驅動器的 PP 模式搭配程式背景執行的功能，進行 3 個目標點的連續運動。 

此範例對於多數的驅動器都是適用。目前遇到比較的的是特別的是:

1. MITSUBISHI MR-J4-10TM : 需要修改驅動器參數才可以使用 PP Mode。
2. OMRON R88D-1SN04H-ECT : 其 PP Mode 只可以設定為 `Change set immediately`，所以在切換到 PP Mode 要另外下 `+pp-imt ( drive-channel slave-position -- )` 指令。

編譯與執行此專案需要 `BotnanaApi.dll`， 可以由以下連結位置取得，或是參考 Botnana APi 章節自行產生。

* 32 位元 Windows: [https://drive.google.com/drive/u/0/folders/1MAZg9XcLLQ8UlemvOaPnnRXnui_YJEMV](https://drive.google.com/drive/u/0/folders/1MAZg9XcLLQ8UlemvOaPnnRXnui_YJEMV)
* 64 位元 Windows: [https://drive.google.com/drive/u/0/folders/1IZZ1QGJf2xVUvhGGWILW0t5WpiyTczz6](https://drive.google.com/drive/u/0/folders/1IZZ1QGJf2xVUvhGGWILW0t5WpiyTczz6)

將 `BotnanaApi.dll` 放到 `botnanacs/SingleDrive/SingleDrive` 目錄下就可以編譯與執行該範例。

# Touch Probe

![](CS-TouchProbe.PNG)

此範例用來測試馬達驅動器 Touch Probe 的功能。**使用此範例的前提是第一個 EtherCAT 從站必須是馬達驅動器。**

通常符合 CiA402 的馬達驅動器都支援 Touch Probe 的功能，Touch Probe 功能可以利用數位輸入訊號來觸發驅動器截取位置的事件。此範例採用 EtherCAT SDO Request 來設定與讀取 Touch Probe 功能相關的暫存器，相關的暫存器如下(以 Panasonic A6B 驅動器內容為例): 

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
| 0 | 0 : Touch probe 1 is switch off <br> 1 : Touch probe 1 is enabled |
| 1 | 0 : Touch probe 1 no positive edge value stored <br> 1 : Touch probe 1 positive edge value stored   |
| 2 | 0 : Touch probe 1 no negative edge value stored <br> 1 : Touch probe 1 negative edge value stored |
| 3-7 | -- | -- |
| 8 | 0 : Touch probe 2 is switch off <br> 1 : Touch probe 2 is enabled |
| 9 | 0 : Touch probe 2 no positive edge value stored <br> 1 : Touch probe 2 positive edge value stored   |
| 10 | 0 : Touch probe 2 no negative edge value stored <br> 1 : Touch probe 2 negative edge value stored |
| 11-15 | -- | -- |

以 Panasonic AB6 驅動器為例，還有一些注意事項:

1. 必須規劃數位輸入 SI5 與 SI6， 
2. 觸發訊號必需維持 2 ms 以上，
3. HM Mode 時不能使用，
4. 不可同時開啟 positive 與 negative edge 觸發，
5. ....等等。

各家的驅動器對於 Touch Probe Function 的使用規範會略有不同，測試前必須要確認相關規定。

編譯與執行此專案仍需要 `BotnanaApi.dll`，請參考 Single Drive 章節。
