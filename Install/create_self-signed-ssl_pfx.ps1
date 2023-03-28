# 輸入網站名稱，並且設定網站的 HTTPS 綁定
param (
    [Parameter(Mandatory = $true)]
    [string]$siteName
)

# 取得目前 Script 所在的路徑，並設定伺服器憑證的路徑
$scriptPath = $MyInvocation.MyCommand.Path | Split-Path
$certKeyPath = "$scriptPath\server.pfx"

# 尋找並取得本機憑證中，Subject 包含 localhost 的憑證的 Thumbprint
$certThumbprint = (Get-ChildItem "cert:\LocalMachine\My" `
| where-object { $_.Subject -like "*localhost*" } `
| Select-Object -First 1).Thumbprint

# 如果憑證未定義，則建立新的自簽憑證
if(!$certThumbprint) {
	$cert = New-SelfSignedCertificate -DnsName "*.localhost", "localhost" -CertStoreLocation "cert:\LocalMachine\My" -NotAfter (Get-Date).AddYears(10)
	$certThumbprint = (Get-ChildItem "cert:\LocalMachine\My" `
	| where-object { $_.Subject -like "*localhost*" } `
	| Select-Object -First 1).Thumbprint
}

# 移除原本 443 Port 的 HTTP 綁定，並建立 HTTPS 綁定
Get-WebBinding -Port 443 -Name $siteName | Remove-WebBinding
New-WebBinding -Name $siteName -Port 443 -Protocol https

# 取得 HTTPS 綁定，並且將憑證加入綁定中
$httpsBinding = Get-WebBinding -Name $siteName -Protocol https  
if ($httpsBinding) {
    $httpsBinding.AddSslCertificate($certThumbprint, "My")
}

# 等待使用者輸入任意鍵，程式結束
Write-Host 設定完成"
