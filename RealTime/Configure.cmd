@SET PATH=%PATH%;C:\Program Files\MSBuild\14.0\Bin;C:\Program Files (x86)\MSBuild\14.0\Bin;C:\Windows\Microsoft.NET\Framework64\v4.0.30319;C:\Windows\Microsoft.NET\Framework\v4.0.30319

msbuild repository.sln /target:Clean /verbosity:minimal

..\Platform\Tools\dist\Allors.Tools.Cmd.exe repository generate repository.sln repository ../platform/base/templates/meta.cs.stg meta/Generated

rmdir /s /q .\Domain\Generated

rmdir /s /q .\Web\Allors\Client\Generated
del /S Web\Allors\Client\*.js
del /S Web\Allors\Client\*.js.map
del /S Web\app\*.js
del /S Web\app\*.js.map

msbuild RealTime.sln /target:Clean /verbosity:minimal

msbuild RealTime.sln /target:Merge:Rebuild /p:Configuration="Debug" /verbosity:minimal

msbuild Resources/Merge.proj /verbosity:minimal

msbuild RealTime.sln /target:Meta:Rebuild /p:Configuration="Debug" /verbosity:minimal

msbuild Domain/Generate.proj /verbosity:minimal
msbuild Web/Generate.proj /verbosity:minimal

msbuild Docs/Generate.proj /verbosity:minimal

msbuild Domain.Diagrams/Generate.proj /verbosity:minimal
msbuild Web.Diagrams/Generate.proj /verbosity:minimal

@pause
