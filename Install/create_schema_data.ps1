# 啟用 SQL Server 的 TCP 協定並執行兩個 SQL 檔案

# Get access to SqlWmiManagement DLL on the machine with SQL
# we are on, which is where SQL Server was installed.
# Note: this is installed in the GAC by SQL Server Setup.

[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.SqlWmiManagement')

# Instantiate a ManagedComputer object which exposes primitives to control the
# installation of SQL Server on this machine.

$wmi = New-Object 'Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer' localhost

# Enable the TCP protocol on the default instance. If the instance is named, 
# replace MSSQLSERVER with the instance name in the following line.

$tcp = $wmi.ServerInstances['MSSQLSERVER'].ServerProtocols['Tcp']
$tcp.IsEnabled = $true  
$tcp.Alter()  

# You need to restart SQL Server for the change to persist
# -Force takes care of any dependent services, like SQL Agent.
# Note: if the instance is named, replace MSSQLSERVER with MSSQL$ followed by
# the name of the instance (e.g. MSSQL$MYINSTANCE)

Restart-Service -Name MSSQLSERVER -Force

$executePath = $MyInvocation.MyCommand.Path | Split-Path
$schemaFilePath = Join-Path $executePath "sql\schema.sql"
$dataFilePath = Join-Path $executePath "sql\data.sql"

if (Test-Path $schemaFilePath) {
    # 建立資料庫 
    $firstLine = Get-Content $schemaFilePath -TotalCount 1
    $databaseName = $firstLine.Substring($firstLine.IndexOf("[") + 1, $firstLine.IndexOf("]") - $firstLine.IndexOf("[") - 1)
    $sqlcmd = "sqlcmd -S localhost -Q `"CREATE DATABASE $databaseName`""
    Write-Host "建立$databaseName"
    Invoke-Expression $sqlcmd
    # 執行schema.sql
    Write-Host "執行schema.sql"
    sqlcmd -S localhost -i $schemaFilePath
} 
else {
    Write-Host "缺少schema.sql檔案，安裝失敗"
    return
}

if (Test-Path $dataFilePath) {
    Write-Host "執行data.sql"
    sqlcmd -S localhost -i $dataFilePath
}

Write-Host "安裝成功"
