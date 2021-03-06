#!/bin/bash
# stty -echo

BINARYDIR="./build_output/AutoTest.NET"
DEPLOYDIR="./ReleaseBinaries"
CASTLEDIR="./lib/Castle.Windsor"
VSADDINDIR="./addins/VisualStudio/FilesToDeploy"
RESOURCES="./src/Resources"

if [ ! -d $DEPLOYDIR ]; then
{
	mkdir $DEPLOYDIR
	mkdir $DEPLOYDIR/Icons
}
else
{
	rm -rf $DEPLOYDIR/*
	mkdir $DEPLOYDIR/Icons
}
fi


cp $BINARYDIR/AutoTest.Messages.dll $DEPLOYDIR/AutoTest.Messages.dll
cp $BINARYDIR/AutoTest.Core.dll $DEPLOYDIR/AutoTest.Core.dll
cp $BINARYDIR/AutoTest.Console.exe $DEPLOYDIR/AutoTest.Console.exe
cp $BINARYDIR/AutoTest.WinForms.exe $DEPLOYDIR/AutoTest.WinForms.exe
cp $BINARYDIR/AutoTest.config.template $DEPLOYDIR/AutoTest.config
cp ./README $DEPLOYDIR/README
cp ./LICENSE $DEPLOYDIR/AutoTest.License.txt

cp $BINARYDIR/Castle.Core.dll $DEPLOYDIR/Castle.Core.dll
cp $BINARYDIR/Castle.DynamicProxy2.dll $DEPLOYDIR/Castle.DynamicProxy2.dll
cp $BINARYDIR/Castle.Facilities.Logging.dll $DEPLOYDIR/Castle.Facilities.Logging.dll
cp $CASTLEDIR/Castle.license.txt $DEPLOYDIR/Castle.license.txt
cp $BINARYDIR/Castle.MicroKernel.dll $DEPLOYDIR/Castle.MicroKernel.dll
cp $BINARYDIR/Castle.Windsor.dll $DEPLOYDIR/Castle.Windsor.dll

#cp $VSADDINDIR/AutoTest.VSAddin.AddIn $DEPLOYDIR/AutoTest.VSAddin.AddIn
#cp $VSADDINDIR/AutoTest.VSAddin.dll $DEPLOYDIR/AutoTest.VSAddin.dll
#cp $VSADDINDIR/Install_Visual_Studio_Addin.bat $DEPLOYDIR/Install_Visual_Studio_Addin.bat


cp ./$RESOURCES/* $DEPLOYDIR/Icons
