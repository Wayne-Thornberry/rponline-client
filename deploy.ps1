#Remove-Item "D:\ProjectOnline\resources\client-core\*"  -Recurse
#Copy-Item -Path ".\vendor\ProjectOnline\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
#Copy-Item -Path ".\vendor\OnlineEngine\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Copy-Item -Path ".\data\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse 
Copy-Item -Path ".\artifacts\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Remove-Item "D:\ProjectOnline\resources\client-core\CitizenFX.Core.*.dll" -Recurse
Remove-Item "D:\ProjectOnline\resources\client-core\*.pdb" -Recurse