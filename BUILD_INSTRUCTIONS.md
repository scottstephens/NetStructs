# Build Instructions

## Visual Studio 2017
* Open NetStructs.sln and build.

# Release Instructions
1. Change the package version in the NetStructs project.
2. Commit the version change.
3. Publish the NetStructs project (which just puts a Nuget package into the pub folder)
4. Tag the version change (for example: git tag -a v0.2.0 -m "Version 0.2.0")
5. Push commits (git push)
6. Push tags (git push --tags)
7. Run the nuget_deploy script (./nuget_deploy.sh 0.2.0)