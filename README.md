基于NAudio的录音程序 # RecrodNetMeetting

1.0.0
想录网络会议声音，没找到合适的内录程序，就用 NAudio(https://github.com/naudio/NAudio) 项目Demo中RecordingPanel控件的代码简化改造了一个只有内录功能的程序。
代码基本搬运自该控件，没几行原创。

录音文件保存在当前用户的temp/NAudioDemo 目录，如：C:\Users\ #当前用户名#\AppData\Local\Temp\NAudioDemo 

2.0.0
内录录不了自己的声音，于是Google了同时内外录并混音的方法，基本实现
可选混录，内录，外录
点文件夹图标打开录音文件夹
显示声音的实时波形
