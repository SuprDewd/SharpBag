Compiler = dmcs
Tester = nunit-console
Mono = /opt/mono-2.10
MonoLibs = ${Mono}/lib/mono/4.0
Defines = "TRACE;DOTNET4;RELEASE"
Name = SharpBag
Tests = ${Name}.Tests
Bin = bin
Output = ${Bin}/Release
NUnit = ${Mono}/lib/monodevelop/AddIns/NUnit
References := System System.Core System.Drawing System.Numerics System.Data System.Data.DataSetExtensions System.Xml System.Xml.Linq WindowsBase System.Xaml
TestReferences := ${References} nunit.core nunit.framework
Docs = false

all: build

build:
	mkdir -p ${Name}/${Output}
	${Compiler} \
	-t:library \
	-d:${Defines} \
	${foreach Lib, ${References}, -r:${MonoLibs}/${Lib}.dll} \
	-r:MySql.Data.dll \
	-out:${Name}/${Output}/${Name}.dll \
	${if, ${Docs}, -doc:${Name}/${Output}/${Name}.xml} \
	`find -iname '*.cs' | grep -E '\./${Name}/.+\.cs'`

buildtests:
	mkdir -p ${Tests}/${Output}
	${Compiler} \
	-t:library \
	-d:${Defines} \
	${foreach Lib, ${TestReferences}, -r:${MonoLibs}/${Lib}.dll} \
	-r:${Name}/${Output}/${Name}.dll \
	-r:MySql.Data.dll \
	-out:${Tests}/${Output}/${Tests}.dll \
	${if, ${Docs}, -doc:${Tests}/${Output}/${Tests}.xml} \
	`find -iname '*.cs' | grep -E '\./${Tests}/.+\.cs'`

test: buildtests
	${Tester} ${Tests}/${Output}/${Tests}.dll

clean:
	rm -rf ${Name}/${Bin} ${Tests}/${Bin}
