# SDO指令

本章節將介紹如何使用SDO指令

首先是讀取


```
#get-sdo ( subindex index position -- )
subindex index position get-sdo
```

以 32 bits 有號整數的型式透過 SDO 讀取 EtherCAT slave `position` 的 Object `index` index: subindex `subindex`。

接著是寫入
```
#set-sdo( data subindex index position -- )
data subindex index position set-sdo
```
將設定值 `data` 以 32 bits 有號整數的型式透過 SDO 寫到 EtherCAT slave `position` 的 Object Index `index`: subindex `subindex`。