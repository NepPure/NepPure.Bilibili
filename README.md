# NepPure.Bilibili
![NetFramework.CI](https://github.com/NepPure/NepPure.Bilibili/workflows/NetFramework.CI/badge.svg)

> Bilibili便捷小工具，目前作为赫萝酱的舰长排队小工具开发~~

## 环境依赖.NET Framework 4.8
下载地址：https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-web-installer

## 设计说明
所有信息明文存储在本地`config.json`中，默认监听本地5678端口作为ws服务

**崩坏3服务器判断策略**

服务器 | 合理的关键词，不区分大小写|
-|-|
安卓 | `安卓`、`Android` |
IOS | `IOS`、`苹果`、`apple`|
全平台（桌面） | `桌面`、`全平台` | 
Bilibili | `Bilibili`、`哔哩`、`B服` | 
UC | `UC` | 
360 | `360` | 
应用宝 | `应用宝`、`宝服` | 
小米 | `小米`、`xiaomi` | 
华为 | `华为`、`huawei` | 
OPPO | `OPPO` | 
VIVO | `VIVO` | 
魅族 | `魅族` | 
酷派 | `酷派` | 
联想 | `联想` | 
金立 | `金立` | 
豌豆荚 | `豌豆荚` | 

## 使用方法
1. 在OBS中将`index.html`添加到`浏览器`显示10人的宽度为`1333`高度为`155`,添加后可再次缩放

![image](https://user-images.githubusercontent.com/12379907/76155454-0c86c880-6127-11ea-819e-c5c1ea8b8286.png)

2. 在队列中点击加入，右侧为直播间展示队列

![image](https://user-images.githubusercontent.com/12379907/76145476-a79a8680-60c4-11ea-961d-e6471382b064.png)

3. 即可在直播间实时展示排队情况

![image](https://user-images.githubusercontent.com/12379907/76155402-88344580-6126-11ea-921a-e9faaf304ec1.png)

![image](https://user-images.githubusercontent.com/12379907/76155322-a483b280-6125-11ea-806f-695e2b5dcc69.png)

![Desktop 2020 03 08 - 10 11 52 03_1_1](https://user-images.githubusercontent.com/12379907/76155392-73f04880-6126-11ea-9cdd-eb05590aa169.gif)
