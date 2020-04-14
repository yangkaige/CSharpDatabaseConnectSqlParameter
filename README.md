## SqlServer数据连接，带参数的sql语句
示例代码：  
``` C#
  //连接字符串
  string strCon = "Data Source=XXX;Initial Catalog=XXX;Integrated Security=TRUE";
  //查询语句
  string sql = "select * from Table where id=@myId";
  using(SqlConnection con = new SqlConnection(strCon))
  {
    using(SqlCommand com = new SqlCommand(sql,con))
    {
      //连接数据库
      con.Open();
      //添加查询参数
      com.Parameters.Add(new SqlParameter("@myId",SqlDbType.VarChar,20){Value = "XXX"});
      //查询或操作数据库
      //com.ExecuteNonQuery();//常用于查insert,update,delete操纵语句
      //com.ExecuteScalar();//获取单个结果值
      //com.ExecuteReader();//常用于select语句，返回SqlDataReader对象，可通过该对象从数据库服务器内存中获取已查询出的数据；但取出时，必须保持数据库连接
      
      
    }
  }
```
- 注：带参数的sql语句可防止sql注入攻击。
