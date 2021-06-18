# 簡介

這個簡單的手冊中，我們會先實作單軸馬達的定位，並在之後的章節完成一個控制兩軸馬達，其中一軸定位，另一軸定速的例子。

以下是在 examples/drive-pp 中馬達定位的腳本程式，這程式以 Botnana 內建的腳本語言實作。在本手冊中我們會說明這腳本中每個指令的意義。並且修改它，使得它能接受新的位置設定。
最後我們會增加一個定速的馬達，並且用同樣的方式使得腳本能接受變速的命令。

要瞭解本手冊的內容應先閱讀「rtForth 入門」。

```
: drive-pp                     \ 定義一個新的，名稱為 drive-pp 的指令，執行這指令時，會做以下一直到分號的事
    1 1 reset-fault            \ 清除從站一的第一顆馬達的異警
    1 1 until-no-fault         \ 等待直到從站一的第一顆馬達的異警被清除
    1 1 drive-on               \ 將從站一的第一顆馬達致能 (servo on, operation enabled)
    1 1 until-drive-on         \ 等待從站一的第一顆馬達完成致能
    1000 ms                    \ 等待 1000 ms，也就是 1 秒
    hm  1 1 op-mode!           \ 設從站一的第一顆馬達的模式為回原點 (hm mode)
    33  1 1 homing-method!     \ 設回原點的方式是第 33 號
    until-no-requests          \ 等待之前的 SDO 設定完成
    1 1 go                     \ 從站一的第一顆馬達回原點
    1 1 until-target-reached   \ 等待從站一的第一顆馬達到達目標位置
    ." log|homed\" cr          \ 送訊息給上位控制器，告知已回原點
    pp  1 1 op-mode!           \ 設從站一的第一顆馬達的模式為點到點運動 (pp mode)
    1000  1 1 profile-v!       \ 設從站一的第一顆馬達的速度 (profile velocity) 為 1000
    until-no-requests          \ 等待之前的 SDO 設定完成
    10000  1 1 target-p!       \ 設從站一的第一顆馬達的目標位置 (target position) 為 10000
    1 1 go                     \ 從站一的第一顆馬達執行點到點的目標位置 (target position) 為 10000
    1 1 until-target-reached   \ 等待從站一的第一顆馬達到達目標位置
;
```
