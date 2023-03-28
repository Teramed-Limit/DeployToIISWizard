try {
    # 安裝 IIS
    $features = @(
        "IIS-DefaultDocument",
        "IIS-ASPNET45",
        "IIS-CommonHttpFeatures",
        "IIS-NetFxExtensibility45",
        "IIS-BasicAuthentication"
    )
    $features | ForEach-Object {
        $featureName = $_
        $featureStatus = Get-WindowsOptionalFeature -Online -FeatureName $featureName | Select-Object -ExpandProperty State
        if ($featureStatus -ne 'Enabled') {
            Write-Host "Enabling feature '$featureName'"
            Enable-WindowsOptionalFeature -Online -FeatureName $featureName -All | Out-Null
        }
    }
}
catch {
    Write-Host "Error: $($_.Exception.Message)"
}