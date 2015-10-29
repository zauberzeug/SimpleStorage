#!/bin/bash

echo "Don't forget"
echo "  * to build in Release mode,"
echo "  * to increase the nuspec version!"
echo "Continue? [y|n]"

read -e -n1 choice
if [ "$choice" != "y" ]
then
    exit
fi

path=`dirname $0`

nuget pack $path/*.nuspec -OutputDirectory $path || exit 1
nuget push $path/*.nupkg
mv $path/*.nupkg $path/releases
