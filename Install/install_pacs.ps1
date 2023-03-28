param (
    [Parameter(Mandatory = $true)]
    [string]$DatabaseName,

    [Parameter(Mandatory = $true)]
    [string]$ServerName,

    [Parameter(Mandatory = $true)]
    [string]$DBUserID,

    [Parameter(Mandatory = $true)]
    [string]$DBPassword
)


try {
    $executePath = $MyInvocation.MyCommand.Path | Split-Path -Parent
        
    # 安裝 URL Rewrite
    $msiPath = Join-Path -Path $executePath -ChildPath 'Installer\ISoftViewerPACSServer.msi'
    
    if (-not (Test-Path -Path $msiPath)) {
        Write-Host "PACS 安裝文件 '$msiPath' 不存在"
        return
    }
    
    Write-Host "正在安裝 PACS"
    Start-Process -FilePath $msiPath -ArgumentList '/qn' -Wait | Out-Null
    
    Write-Host "PACS 安裝成功"

    
    # 定義要替換的字串和替換後的值
    $find1 = '<add key="PACS_DBName" value="PACSServer" />'
    $replace1 = '<add key="PACS_DBName" value="{0}" />' -f $DatabaseName
    
    $find3 = '"Jacob"'
    $replace3 = '"{0}"' -f $ServerName
    
    $find4 = '<value>PACSServer</value>'
    $replace4 = "<value>{0}</value>" -f $DatabaseName
    
    $find5 = 'victor70394219'
    $replace5 = "{0}" -f $DBPassword
    
    $find6 = '"teramed@1212"'
    $replace6 = '"{0}"' -f $DBPassword
    
    $targetDir = "C:\Program Files\TeraMed\ISoftViewerPACSServer"
    
    # 檢查目標目錄是否存在
    if (-not(Test-Path $targetDir)) {
        Write-Error "目標目錄 $targetDir 不存在"
        return
    }
    
    # 搜尋目錄中的檔案
    $configFiles = Get-ChildItem -Path $targetDir -Include *.config -Name
    
    # 確認是否有搜尋到任何檔案
    if ($configFiles.Count -eq 0) {
        Write-Warning "目錄 $targetDir 中沒有任何 *.config 檔案"
        return
    }
    
    Write-Host "更改PACSServer設定"
    
    # 逐一處理每個檔案
    foreach ($fileName in $configFiles) {
        # 構建檔案的完整路徑
        $filePath = Join-Path -Path $targetDir -ChildPath $fileName
    
        # 檢查檔案是否存在
        if (-not(Test-Path $filePath)) {
            Write-Warning "檔案 $filePath 不存在"
            continue
        }
    
        # 套用所有的替換操作
        (Get-Content $filePath) `
            -replace $find1, $replace1 `
            -replace $find3, $replace3 `
            -replace $find4, $replace4 `
            -replace $find5, $replace5 `
            -replace $find6, $replace6 `
            | Set-Content $filePath -Encoding UTF8
    }
    
    Write-Host "更改PACS Server設定完成"
    
    
    # 檢查服務是否存在
    Write-Host "檢查服務是否存在"
    if (Get-Service -Name "TeraMedArchivingService" -ErrorAction SilentlyContinue) {
        Write-Host "服務存在"
        Stop-Service -Name "TeraMedArchivingService" -ErrorAction SilentlyContinue
        Start-Process -FilePath "C:\Program Files\TeraMed\ISoftViewerPACSServer\UnInstallTeraMedDicomArchivingService.bat" `
        -ArgumentList "-Wait", "-NoNewWindow" -WindowStyle Hidden -ErrorAction SilentlyContinue
        Write-Host "刪除服務"
    }
    
    # 啟外部程式
    Write-Host "安裝PACS Server Service"
    Start-Process -FilePath "C:\Program Files\TeraMed\ISoftViewerPACSServer\InstallTeraMedDicomArchivingService.bat" `
        -ArgumentList "-Wait", "-NoNewWindow" -WindowStyle Hidden -ErrorAction SilentlyContinue
    
    # 啟動服務
    Write-Host "啟動服務中..."
    Start-Sleep -Seconds 3
    Start-Service -Name "TeraMedArchivingService" -ErrorAction SilentlyContinue
    Write-Host "操作完成"
}
catch {
    Write-Host "Error: $($_.Exception.Message)"
}