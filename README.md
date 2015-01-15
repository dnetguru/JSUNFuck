# JSUNFuck
The goal of this project is to deobfuscate JavaScript files that were proccessed with JSFuck (http://www.jsfuck.com)

# Usage
You can run the program with the provided test files or create your own using the jsfuck.js located @ https://github.com/aemkei/jsfuck/blob/master/jsfuck.js

_NOTE: the jsfuck.js served by www.jsfuck.com is slightly different than the one posted in their repo. This program will use a simple heuristic approach to detect which file was probably used._ 
```
Usage: JSUNFuck.exe <JSFuck Encrypted File>
       JSUNFuck.exe <JSFuck Encrypted File> <Output Filename>
```
Here's the sample output you should expect from the provided test files ...
```
PS ..\JSUNFucker\JSUNFucker> .\bin\Release\JSUNFuck.exe '.\Test Files\AlertOne.ascii' testRes
PS ..\JSUNFucker\JSUNFucker> type .\testRes
return eval)()(alert("IT WORKS !!!");)
PS ..\JSUNFucker\JSUNFucker> .\bin\Release\JSUNFuck.exe '.\Test Files\SimpleText.ascii' testRes2
PS ..\JSUNFucker\JSUNFucker> type .\testRes2
THIS IS JUST SOME TEXT WITHOUT eval()
PS ..\JSUNFucker\JSUNFucker>
```
Running with Mono (*nix environment)
```
[root@w0rkb3nch ~]# ls
JSUNFuck.exe  SimpleText.ascii
[root@w0rkb3nch ~]# mono JSUNFuck.exe SimpleText.ascii result
[root@w0rkb3nch ~]# cat result 
THIS IS JUST SOME TEXT WITHOUT eval()
[root@w0rkb3nch ~]# 
```
# License
Everything in this repository is licensed under MIT Open Source License and is free to use (without any warranty) and modify with proper attribution.


*TODO: Complete README.md, add LICENSE file, test the project with more JS files*
