
NodeJs
=========================================================================
node-v18.12.1-x64.msi

npm
=========================================================================
检查package版本更新:
...\ClientApp>ncu

检查package版本并更新所有package:
...\ClientApp>ncu -u

检查package版本并更新指定package到指定version (package.json):
ncu -u {package} {version}
e.g:
...\ClientApp>ncu -u bootstrap 5.3.2

检查package版本并更新所有package (package-lock.json): 
npm update {package} {version}
e.g:
...\ClientApp>npm update bootstrap 5.3.2

PrimeNG
=========================================================================
cmd:
安装primeng：
...\ClientApp>npm install primeng
安装primeng到指定版本：
...\ClientApp>npm install primeng@12.0.0

Documents
=========================================================================
Angular 环境
https://codeleading.com/article/15492294338/

PrimeNG 文档
https://primeng.org/installation

Angular 升级
https://update.angular.io/?v=12.0-17.0

安装angular到指定版本
https://blog.csdn.net/zf2451597310/article/details/129989137

一篇文章搞定什么是nodeJs它和NPM关系与项目应用
https://www.jianshu.com/p/d5e6c7726108

package.json 与 package-lock.json文件的区别
https://blog.csdn.net/qq_34402069/article/details/127638319

掌握NPM：package.json、模块卸载、更新、搜索、创建与版本号
https://baijiahao.baidu.com/s?id=1773343839368995343&wfr=spider&for=pc

package.json中 版本号详解
https://zhuanlan.zhihu.com/p/616051512?utm_id=0