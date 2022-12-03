url：
https://antblazor.com/en-US/
https://dev.to/yongquan/ant-design-blazor-table-4963
https://antblazor.com/en-US/components/table#ant-blazor-17f32a65-0dcd-4f47-b85c-0ccdc275408d

1)Nuget install：
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Sqlite
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design

2)cmd in package manager Console:

2.1 install EntityFrameworkCore
dotnet tool install --global dotnet-ef --version 6.0.0

2.2 migrate
dotnet ef migrations add Initial --project Blazor-Query-Demo
dotnet ef database update --project Blazor-Query-Demo


