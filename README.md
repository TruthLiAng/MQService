# ActiveMQ练习项目 #

主要是一个自己的ActiveMQ练习项目，用.net Core在Windows控制台程序中搭建框架完成了消息队列的简单收发。

## 搭建MQ环境 ##

项目主要使用ActiveMQ，可以到[官网](http://activemq.apache.org/)具体查看和下载部署ActiveMQ环境。

## 使用 ##

+ cmd进入ActiveMQ的安装目录

+ 例：

```bash
# e盘下的ActiveMQ是我的安装目录，请改为自己的安装目录

E:\ActiveMq>cd bin

E:\ActiveMq\bin>activemq start

```

+ 然后浏览器进入[网页控制台](http://127.0.0.1:8161)，这里为默认端口8161，需要修改请自行查阅资料。

+ 克隆项目并使用nuget安装相关包

+ 设置`ActiveMQService`为启动项并启动项目

+ 启动后选择`MQSendText`右键选择`调试`-`启动新实例`即可。