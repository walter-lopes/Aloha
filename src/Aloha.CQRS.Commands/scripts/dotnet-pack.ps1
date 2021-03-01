New-Variable -Name "version" -Value "1.0.1"
New-Variable -Name "project" -Value "Aloha.CQRS.Commands"
New-Variable -Name "nupkgFile" -Value "$project.$version.nupkg"

Write-Host Triggering Nuget package build

cd ..

dotnet pack Aloha.CQRS.Commands.csproj -c release /p:PackageVersion="$version" --no-restore -o .

Write-Host Uploading Aloha CQRS Commands package to Nuget

dotnet nuget push $nupkgFile -k oy2k4hs53p6kvn4ij7qar4ogpl2i4hvuziirfnsihkvb64 -s https://api.nuget.org/v3/index.json