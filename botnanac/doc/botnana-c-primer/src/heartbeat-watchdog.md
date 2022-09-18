# Hearbeat Watchdog

新增監視命令 `mb-heart-beat!` 是否送到的watchdog

在指定的時間內若控制器沒有收到 `mb-heart-beat!` 命令則會緊急停止所有驅動器

## --用法--

此watchdog位於位址40045

若希望控制器於每 `n` 毫秒內沒有收到 `mb-heart-beat!` 命令後將所有驅動器緊急停止

則使用以下命令

```
botnana_mb_set(botnana, 40045, n);
botnana_mb_publish(botnana);
```

即可設定heartbeat watchdog，設定當下即生效

若設定n = 0時，表示將watchdog關閉
