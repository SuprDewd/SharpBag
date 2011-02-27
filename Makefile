MonoLocation = /opt/mono-2.10
MonoLibLocation = ${MonoLocation}/lib/mono/4.0
Defines = "DOTNET4;RELEASE"
OutputLocation = SharpBag/bin/Release
MonoLibs := System System.Core System.Drawing System.Numerics System.Data System.Data.DataSetExtensions System.Xml System.Xml.Linq WindowsBase System.Xaml
Docs = false

all:
	dmcs -t:library -d:${Defines} ${foreach Lib, ${MonoLibs}, -r:${MonoLibLocation}/${Lib}} -r:${OutputLocation}/MySql.Data.dll -out:${OutputLocation}/SharpBag.dll -doc:${OutputLocation}/SharpBag.xml SharpBag/*.cs SharpBag/**/*.cs
