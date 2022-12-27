New-Variable -Name "version" -Value "1.0.2"
New-Variable -Name "project" -Value "Aloha.CQRS.Events"
New-Variable -Name "nupkgFile" -Value "$project.$version.nupkg"

Write-Host Triggering Nuget package build

cd ..

dotnet pack Aloha.CQRS.Events.csproj -c release /p:PackageVersion="$version" --no-restore -o .

Write-Host Uploading Aloha.CQRS.Events package to Nuget

dotnet nuget push $nupkgFile -k oy2px6vyvtquegqjyz6qr4lsryl3riwurce4ymjrbp6b5q -s https://api.nuget.org/v3/index.json