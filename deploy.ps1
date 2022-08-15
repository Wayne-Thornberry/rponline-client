Remove-Item "D:\ProjectOnline\resources\client-core\*"  -Recurse
Copy-Item -Path "E:\OneDrive\Repo\Project Online\data\resources\ProlineCore\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Copy-Item -Path "E:\OneDrive\Repo\RPOnline\data\components\ProlineCore\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Copy-Item -Path "E:\OneDrive\Repo\RPOnline\data\core\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Copy-Item -Path "E:\artifacts\ProlineCore\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Copy-Item -Path "E:\artifacts\RPOnline\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Remove-Item "D:\ProjectOnline\resources\client-core\CitizenFX.Core.*.dll" -Recurse
Remove-Item "D:\ProjectOnline\resources\client-core\*.pdb" -Recurse