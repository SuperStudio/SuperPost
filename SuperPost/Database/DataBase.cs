using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPost
{
    public static class DataBase
    {
        //存储位置
        public static string DATA_PATH = "data.sqlite";



        // 数据库
        public static string TABLE_SIDE_PROJECT= "create table if not exists side_project (    id INT PRIMARY KEY,	parent_id INT,    name VARCHAR(100))";
        public static string TABLE_SIDE_HTTP= "create table if not exists side_http (    id INT PRIMARY KEY,    parent_id INT,	httpMethod VARCHAR(100),    name VARCHAR(100))";   

    }
}
