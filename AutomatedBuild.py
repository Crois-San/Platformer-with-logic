import os
UnityPath = input("Enter path to your unity.exe")
ProjectPath = input("Enter path to the project")
os.system("C:\Program Files\Unity\Hub\Editor\2019.3.5f1\Editor\Unity.exe" -quit -batchmode -projectPath "E:\Unity Projects\LogicalElements" -executeMethod AutomatedBuild.Build)
