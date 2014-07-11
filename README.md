Functester
==========
>A sample tool for C# function testing.

本程序是一个方法测试小工具，动态反射出需要测试的方法及参数。用户只需要给出相应参数，执行此方法就可以得出方法的执行结果。若需要数据库，请在APP.Config中进行配置。

###软件界面
![软件界面](https://raw.githubusercontent.com/jxzly229190/Functester/master/image/show.png)

### Demo运行说明 ###
请下载此项目后配置app.config>>appSettings>>TestBinPath 节点为您需要测试的DLL文件所在的绝对文件夹路径。运行FuncTester即可。

### 项目说明 ###
* FuncTester 为主项目
* TestedDLL 测试项目，只包测试类一个方法

### 环境依赖 ###
* DotNet Framework 4.0+

