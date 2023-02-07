artifacts - build artifacts from build.cake
code - contains source code for the main project to work
config - contains config files for resource to work
data - contains data files under the subdirector data in the resource
ext - contains any non CLR projects that maybe deployed in the resource or used to make the resource
package - contains a full final copy of the resource before deployment
vendor - contains any 3rd party libraries or dependencies that otherwise cannot be gotten via nugets

To build yourself, ensure that you have Cake as a tool in powershell. Open build-debug and or build-release and change the path to deploy to your resources path
then just run either build-debug.bat or build-release.bat 
