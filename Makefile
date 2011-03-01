Name = SharpBag
Tests = ${Name}.Tests
Defines = "TRACE;DOTNET4;RELEASE"
Docs = true

Output = Bin

Compiler = dmcs
Mono = /opt/mono-2.10
MonoLibs = ${Mono}/lib/mono/4.0
Tester = ${Mono}/bin/mono --runtime=v4.0 ${Output}/NUnit/nunit-console.exe
GuiTester = ${Mono}/bin/mono --runtime=v4.0 ${Output}/NUnit/nunit.exe

References := System System.Core System.Drawing System.Numerics System.Data System.Data.DataSetExtensions System.Xml System.Xml.Linq WindowsBase System.Xaml
TestReferences := ${Output}/nunit.core.dll ${Output}/nunit.framework.dll

all: build

build:
	${Compiler} \
	-t:library \
	-d:${Defines} \
	${foreach Lib, ${References}, -r:${MonoLibs}/${Lib}.dll} \
	-r:${Output}/MySql.Data.dll \
	-out:${Output}/${Name}.dll \
	${if, ${Docs}, -doc:${Output}/${Name}.xml} \
	`find -iname '*.cs' | grep -E '\./${Name}/.+\.cs'`

buildtests:
	${Compiler} \
	-t:library \
	-d:${Defines} \
	${foreach RLib, ${References}, -r:${MonoLibs}/${RLib}.dll} \
	${foreach TLib, ${TestReferences}, -r:${TLib}} \
	-r:${Output}/${Name}.dll \
	-r:${Output}/MySql.Data.dll \
	-out:${Output}/${Tests}.dll \
	`find -iname '*.cs' | grep -E '\./${Tests}/.+\.cs'`

test: buildtests
	${Tester} ${Output}/${Tests}.dll

guitest: buildtests
	${GuiTester} ${Output}/${Tests}.dll

clean:
	rm -f ${Output}/${Name}.dll ${Output}/${Tests}.dll ${Output}/${Name}.xml ${Output}/${Tests}.xml
