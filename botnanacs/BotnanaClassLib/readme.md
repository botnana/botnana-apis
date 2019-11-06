### 編譯專案
1. 因為 BotnanaClassLibTest 會引用 BotnanaClassLib 編譯產生的 dll，若直接點工具列上的 建置>重建方案 會出現編譯錯誤。  
   請先右鍵點選方案總管中的 BotnanaClassLib 專案>重建，再右鍵點選 BotnanaClassLibTest 專案>重建。
2. 執行時若出現錯誤:輸出類型為類別庫的專案無法直接起始，請右鍵點選 BotnanaClassLibTest 專案>設定為起始專案。

### 引用類別庫
1. 初次引用 BotnanaClassLib 時工具箱還找不到 Botnana 的控制項。  
   點開工具箱>工具箱中點右鍵>選擇項目>瀏覽選取 BotnanaClassLib.dll>確定，之後便可以在工具箱中找到控制項。