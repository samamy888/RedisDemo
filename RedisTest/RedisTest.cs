using Lib.Redis.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;

namespace RedisTest;

[TestClass]
public class RedisTest
{
    [Test]
    public void StringSet()
    {
        var db = new RedisClient().Database;

        // Set
        string value = "Hello World";
        var key = "Test";
        db.StringSet(key, value);

        // set timeout 5min
        db.StringSet(key, value, TimeSpan.FromSeconds(60));

        // Get 
        var test = db.StringGet(key);
    }

    [Test]
    public void Sets()
    {
        var db = new RedisClient().Database;
        db.StringIncrement("visitCount");

        // Set
        db.SetAdd("event", "001");
        db.SetAdd("event", "002");
        db.SetAdd("event", "003");

        // Get 
        var result = db.SetScan("event", "00*");
        result.ToList().ForEach(x => Console.WriteLine(x));

        //然後是刪除的部份
        db.SetRemove("event", "002");
    }

    [Test]
    public void Hashset()
    {
        var db = new RedisClient().Database;

        db.HashSet("employee", new HashEntry[]
        {
            new("1", "anson"),
            new("2", "kin"),
            new("3", "jacky"),
        });

        //取出全部
        db.HashGetAll("employee").ToList().ForEach(x => Console.WriteLine(x));

        //取出某筆
        db.HashGet("employee", 2);

        //刪除某筆
        db.HashDelete("employee", 2);

        //修改資料
        db.HashSet("employee", 3, "anson");
    }


    [Serializable]
    class MyClass
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}