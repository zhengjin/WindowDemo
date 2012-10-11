<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebClient.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            1、简化配置（Simplified configuration） - 根据 baseAddresses 生成默认 endpoint；在应用程序的级别上指定默认的 Binding 配置和 Behavior 配置
            <br />
            相关代码位置：host 端在 WinHost 项目，client 端在 WebClient 项目中的 SimplifiedConfiguration.aspx 页面
        </p>
        <p>
            2、不需要 .svc 的物理文件，而直接在 IIS 上托管 WCF 服务
            <br />
            相关代码位置：host 端在 WebHost 项目中的 web.config 文件，client 端在 WebClient 项目中的 IIS_Hosting_Without_SVC_File.aspx 页面
        </p>
        <p>
            3、标准终结点（Standard Endpoint） - 内置了 8 个已经定义好相关配置的标准终结点（分别为 mexEndpoint, announcementEndpoint, discoveryEndpoint, udpAnnouncementEndpoint, udpDiscoveryEndpoint, workflowControlEndpoint, webHttpEndpoint, webScriptEndpoint）
            <br />
            指定标准终结点的方法见 WebHost 项目中的 web.config 文件
        </p>
        <p>
            4、通过标准终结点中的 webScriptEndpoint 来提供 ajax 服务
            <br />
            相关代码位置：host 端在 WebHost 项目中的 web.config 文件，client 端在 WebHost 项目中的 WebScriptEndpointDemo.aspx 页面
        </p>
        <p>
            5、对 REST 服务支持的增强
            <ul>
                <li>
                    通过标准终结点 webHttpEndpoint 来简化配置（详见：WebHost 项目中的 web.config 文件）
                </li>
                <li>
                    通过对 behavior 的配置来为 REST 服务增加 help 页面，在服务地址上加“/help”即可进入 REST 服务的帮助页面（详见：WebHost 项目中的 web.config 文件）
                    <br />
                    本例的 REST 服务的帮助页面为 http://localhost:14802/RestDemo.svc/help
                </li>
                <li>
                    为 REST 增加 HTTP 缓存配置（详见：WebHost 项目中的 web.config 文件，ServiceLib 项目的 RestDemo.cs 文件）
                </li>
            </ul>
        </p>
        <p>
            6、路由服务（Routing Service） - WCF 对 Web Services Addressing (WS-Addressing) 规范的实现
        </p>
        <p>
            7、工作流服务（Workflow Service） - WCF 对 WF (Workflow Foundation) 的支持
        </p>
        <p>
            8、字节流编码（ByteStream） - 增加了对 ByteStream 的支持。原来只支持 Text, Binary, MTOM
        </p>
        <p>
            9、非破坏性队列接收（Non-destructive queue receive） - 改进了原有的对 MSMQ（消息队列） 的支持
        </p>
        <p>
            10、服务发现（WS-Discovery） - 对 WS-Discovery 协议的支持
        </p>
    </div>
    </form>
</body>
</html>
