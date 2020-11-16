  #!/bin/bash
set -x

if [[ $(git status -s) ]]; then
    echo "You have uncommitted files. Commit and push them before running this script."
  #  exit 1
fi

git fetch --tags

# get latest git tag and increase by one (see https://stackoverflow.com/questions/4485399/how-can-i-bump-a-version-number-using-bash)
VERSION=`git describe --abbrev=0 | awk -F. '/[0-9]+\./{$NF+=1;OFS=".";print}'`

echo "setting version to $VERSION"

XAMARIN_TOOLS=/Library/Frameworks/Mono.framework/Versions/Current/Commands/
NUGET="$XAMARIN_TOOLS/nuget"
MONO="$XAMARIN_TOOLS/mono"

ls $XAMARIN_TOOLS

function setVersion_Nupkg {
  sed -i '' "s/\(<version>\).*\(<\/version>\)/\1$VERSION\2/" $1
}

function packNuGet {
	setVersion_Nupkg $1
	$NUGET pack $1 || exit 1
}

function publishNuGet {
  nuget push $1 -Source https://www.nuget.org/api/v2/package || exit 1
}

function createTag {
  git tag -a $VERSION -m ''  || exit 1
  git push --tags || exit 1
}

$NUGET restore SimpleStorage.sln || exit 1

msbuild /p:Configuration=Release Droid/Droid.csproj || exit 1
msbuild /p:Configuration=Release iOS/iOS.csproj || exit 1
msbuild /p:Configuration=Release Tests/Tests.csproj || exit 1

pushd packages && nuget install NUnit.Console && popd
export MONO_IOMAP=all # this fixes slash, backslash path seperator problems within nunit test runner
NUNIT="mono packages/NUnit.ConsoleRunner.*/tools/nunit3-console.exe"
$NUNIT -config=Release "Tests/Tests.csproj" || exit 1

createTag

packNuGet SimpleStorage.nuspec

if [[ $SKIP_DEPLOYMENT == True ]]; then
  echo "Skipping deployment"
  exit 0
fi

publishNuGet SimpleStorage.$VERSION.nupkg
