Compiler = dmcs
MonoLocation = /opt/mono-2.10
MonoLibLocation = ${MonoLocation}/lib/mono/4.0
Defines = "TRACE;DOTNET4;RELEASE"
Name = SharpBag
BinLocation = ${Name}/bin
OutputLocation = ${BinLocation}/Release
MonoLibs := System System.Core System.Drawing System.Numerics System.Data System.Data.DataSetExtensions System.Xml System.Xml.Linq WindowsBase System.Xaml
Docs = false

all: mysql
	${Compiler} \
	-t:library \
	-d:${Defines} \
	${foreach Lib, ${MonoLibs}, -r:${MonoLibLocation}/${Lib}} \
	-r:${OutputLocation}/MySql.Data.dll \
	-out:${OutputLocation}/${Name}.dll \
	-doc:${OutputLocation}/${Name}.xml \
	`find -iname '*.cs'`

mysql:
	cp ${BinLocation}/MySql.Data.dll ${OutputLocation}

clean:
	rm -f ${OutputLocation}/*
