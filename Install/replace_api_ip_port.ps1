param (
    [Parameter(Mandatory = $true)]
    [string]$SiteName,
    [Parameter(Mandatory = $true)]
    [int]$SitePort,
    [Parameter(Mandatory = $true)]
    [string]$IpAddress,
    [Parameter(Mandatory = $true)]
    [string]$TargetDir
)

# The config string that need to be replaced
# 1. Web.config
# 2. Front-end JS
$findString = '127.0.0.1:8080'

# Replace the front-end api path
$replaceString = "$($IpAddress):$($SitePort)"
foreach ($fileName in Get-ChildItem -Path $TargetDir -Include *.js -Name)
{
	$filePath = "$TargetDir\$fileName"
	(Get-Content $filePath) | foreach {$_.replace($findString,$replaceString)} | Set-Content $filePath
}