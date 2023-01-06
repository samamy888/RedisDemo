using Lib.Redis.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;

namespace RedisTest;

[TestClass]
public class RedisTest
{
    [TestMethod]
    public void StringSet()
    {
        RedisClient.Init("localhost");
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

    [TestMethod]
    public void Sets()
    {
        var db = new RedisClient().Database;
        db.StringIncrement("visitCount");

        // Set
        string value = "Hello World";
        db.SetAdd("event", "001");
        db.SetAdd("event", "002");
        db.SetAdd("event", "003");
        var hashGetAll = db.HashGetAll("event");

        // Get 
        var result = db.SetScan("event", "00*");
        result.ToList().ForEach(x => Console.WriteLine(x));

        //�M��O�R��������
        db.SetRemove("event", "002");
    }

    [TestMethod]
    public void Hashset()
    {
        var db = new RedisClient().Database;

        db.HashSet("employee", new HashEntry[]
        {
            new("1", "anson"),
            new("2", "kin"),
            new("3", "jacky"),
        });

        //���X����
        db.HashGetAll("employee").ToList().ForEach(x => Console.WriteLine(x));

        //���X�Y��
        db.HashGet("employee", 2);

        //�R���Y��
        db.HashDelete("employee", 2);

        //�ק���
        db.HashSet("employee", 3, "anson");
    }


    [Serializable]
    class MyClass
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}