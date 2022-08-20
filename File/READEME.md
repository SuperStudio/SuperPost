# 数据库

SQLITE 教程：https://www.runoob.com/sqlite/sqlite-tutorial.html

展开栏

```sql
drop table if exists  side_project;
create table if not exists side_project (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
	parent_id INT,
    name VARCHAR(100)
);
insert into side_project (parent_id,name) values (0,'项目1') ,(0,'项目2') ,(0,'项目3');
insert into side_project (parent_id,name) values (1,'项目1.1') ,(1,'项目1.2') ,(2,'项目2.1') ,(3,'项目3.1');
select * from side_project;
```

httpObject

```sql
drop table if exists  side_http;
create table if not exists side_http (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    parent_id INT,
	http_method VARCHAR(100),
    name VARCHAR(100)
);
insert into side_http (parent_id,http_method,name) values (1,'GET','请求') ,(1,'POST','请求') ,(1,'PUT','请求');
insert into side_http (parent_id,http_method,name) values (3,'GET','请求') ,(4,'POST','请求') ,(5,'PUT','请求');
select * from side_project;
select * from side_http;

```

