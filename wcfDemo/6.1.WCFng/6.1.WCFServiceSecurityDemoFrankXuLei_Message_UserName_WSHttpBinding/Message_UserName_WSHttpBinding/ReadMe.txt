1.//Coded By Frank Xu Lei 8/24/2009.《WCF分布式安全开发实践》8示例代码:
消息安全模式、UserName、WSHttpBinding：
自定义托管宿主配置过程包括三个部分的配置：
1）一个“服务类”WCFService：服务契约和操作契约,另外可以使用数据契约。
2）一个“宿主”环境WCFHost：这是一种应用程序域和进程，服务将在该环境中运行。 
3）一个“终结点”Client：由客户端用于访问服务。
// 
2.//Message_UserName_WSHttpBinding
安全模式: 传输
 
互操作性: 与现有的 Web 服务客户端和服务进行互操作
 
身份验证（服务器）:是（使用 HTTP）

身份验证（客户端）:是（UserName Password）
 
完整性: 是
 
保密性: 是
 
Transport: HTTPS
 
绑定: WSHttpBinding
3.//Marks
服务器需要一个有效的可用于安全套接字层 (SSL) 的 X.509 证书 
4.//makecert 
makecert -sr CurrentUser -ss My -n CN=FrankCertificate -sky signature -pe
5.Set certificate trusted.
http://www.cnblogs.com/frank_xl/archive/2009/07/30/1534624.html
http://www.cnblogs.com/frank_xl/archive/2009/03/01/1400751.html
6.
这里用Makecert.exe工具生成证书，可交换密钥，消息安全机制里使用，可以导出密钥。
使用下面的命令：

makecert -sr localmachine -ss My -n CN=WCFServer -sky exchange -pe -r

这是服务端证书。

makecert -sr currentuser -ss My -n CN=WCFClient -sky exchange -pe -r

这是客户端证书。