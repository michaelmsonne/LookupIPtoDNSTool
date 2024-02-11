<#
#Get file v. for last build IPtoDNSLookup.exe file
$FileVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("..\bin\Release\IPtoDNSLookup.exe").FileVersion
#Rename file to v. and buildtime
Get-ChildItem "..\bin\Release\IPtoDNSLookup.exe" | ? {!$_.PSIsContainer -and $_.extension -eq '.exe'} | Rename-Item -NewName {"$($_.BaseName) v. $FileVersion - Build at $(Get-Date -format "ddMMyyyy-HHmmss")$($_.extension)"} -Force
#Delete old IPtoDNSLookup.exe file there not need to be used anymore
#Get-ChildItem "..\bin\Release" -Recurse -File | Where CreationTime -lt (Get-Date).AddSeconds(-2) | Remove-Item -Force
Get-ChildItem "..\bin\Release" -File | Where CreationTime -lt (Get-Date).AddSeconds(-5) | Remove-Item -Force
#>

#Folder for old builds
$ScriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Path -Parent
$ParentDirectory = Split-Path -Path $ScriptDirectory -Parent
$FolderName = Join-Path $ParentDirectory "bin\Release\Old"

if (Test-Path $FolderName -ErrorAction Ignore) {
    Write-Host "Old folder for Release builds Exists: $FolderName"
    Write-Host "Moving old build files in format: IPtoDNSLookup v. x.x.x.x - Build at ddMMyyyy-HHmmss.exe to $FolderName"
    Get-ChildItem -Path "$ParentDirectory\*IPtoDNSLookup*Build at*" -Recurse | Move-Item -Destination $FolderName
    Write-Host "Moved old build files in format: IPtoDNSLookup v. x.x.x.x - Build at ddMMyyyy-HHmmss.exe to $FolderName"
}
else {
    Write-Host "Old folder for Release builds doesn't Exist - Creating it..."
    # PowerShell Create directory if not exists
    New-Item $FolderName -ItemType Directory
    Write-Host "Old folder for Release builds doesn't Exist - Created folder: $FolderName"
}

#Get file v. for the last build IPtoDNSLookup.exe file
$IPtoDNSLookupPath = Join-Path $ParentDirectory "bin\Release\IPtoDNSLookup.exe"
$FileVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($IPtoDNSLookupPath).FileVersion

#Delete old .exe file there not need to be used anymore 2 sec. old or more
Write-Host "Deleting old files in output $ParentDirectory\*IPtoDNSLookup*Build at*"
Get-ChildItem "$ParentDirectory\*IPtoDNSLookup*Build at*" -File | Where-Object CreationTime -lt (Get-Date).AddSeconds(-2) | Remove-Item -Force
Write-Host "Removed old files in output $ParentDirectory\*IPtoDNSLookup*Build at*"

#Rename file to v. and buildtime
Write-Host "Renaming output $IPtoDNSLookupPath to format IPtoDNSLookup v. $FileVersion - Build at $(Get-Date -format "ddMMyyyy-HHmmss").exe"
Rename-Item -Path $IPtoDNSLookupPath -NewName ("IPtoDNSLookup v. $FileVersion - Build at $(Get-Date -format 'ddMMyyyy-HHmmss').exe") -Force
Write-Host "Renamed output $IPtoDNSLookupPath to format IPtoDNSLookup v. $FileVersion - Build at $(Get-Date -format 'ddMMyyyy-HHmmss').exe"

#Show task is done
Write-Host "Build task done! Output file is:"

#Get filename for the new file
$files = Get-ChildItem -Path $ParentDirectory -Filter "*IPtoDNSLookup*Build at*"

foreach ($file in $files) {
    Write-Host $file.Name
}

# Create hash file for output
$exeFile = Get-ChildItem -Path "$ParentDirectory\bin\Release" -Filter "*IPtoDNSLookup*Build at*.exe" | Select-Object -First 1

if ($null -eq $exeFile)
{
    Write-Host "No matching .exe file found."
}
else
{
    # Generate the SHA-256 hash for the .exe file
    $hash = Get-FileHash -Path $exeFile.FullName -Algorithm SHA256

    # Create a dynamic name for the hash file
    $hashFileName = "$ParentDirectory\bin\Release\{0}.sha256" -f $exeFile.BaseName, (Get-Date -format "ddMMyyyy-HHmmss")

    # Ensure that the directory exists
    $directory = [System.IO.Path]::GetDirectoryName($hashFileName)
    if (-not (Test-Path -Path $directory)) {
        New-Item -Path $directory -ItemType Directory
    }

    # Save the hash value to a .sha256 file
    $hashFileContent = "SHA-256 Hash:`r`n" + $hash.Hash
    $hashFileContent | Set-Content -Path $hashFileName

    Write-Host "SHA-256 hash file created: $hashFileName"
}