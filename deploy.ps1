Remove-Item "D:\ProjectOnline\resources\client-core\*"  -Recurse
Copy-Item -Path ".\data\core\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse 
Copy-Item -Path ".\data\components\ProlineCore\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Copy-Item -Path ".\vendor\ProlineCore\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Copy-Item -Path ".\artifacts\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Remove-Item "D:\ProjectOnline\resources\client-core\CitizenFX.Core.*.dll" -Recurse
Remove-Item "D:\ProjectOnline\resources\client-core\*.pdb" -Recurse