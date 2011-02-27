MonoLocation = /opt/mono-2.10
MonoLibLocation = ${MonoLocation}/lib/mono/4.0
Defines = DOTNET4;RELEASE
OutputLocation = SharpBag/bin/Debug
MonoLibs := System System.Core System.Drawing System.Numerics System.Data System.Data.DataSetExtensions System.Xml System.Xml.Linq WindowsBase  System.Xaml

all:
	dmcs -target:library -d:${Defines} ${foreach Lib, ${MonoLibs}, -r:${MonoLibLocation}/${Lib}} -r:${OutputLocation}/MySql.Data.dll -o:${OutputLocation}/SharpBag.dll -doc:${OutputLocation}/SharpBag.xml SharpBag/**/*.cs
