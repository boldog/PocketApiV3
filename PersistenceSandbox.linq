<Query Kind="Program">
  <NuGetReference>LiteDB</NuGetReference>
  <NuGetReference>PocketApiV3</NuGetReference>
  <Namespace>LiteDB</Namespace>
  <Namespace>LiteDB.Shell</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>PocketApiV3</Namespace>
</Query>

void Main()
{
	string jsonFile = @"D:\Projects\PocketApiV3\Tests\ApiRetrieve.json";
	string liteDbFile = @"D:\LiteDB\PocketApiV3PersistenceSandbox.db";
	
	var retrieveResponse = DeserializeRetrieveResponse(jsonFile);
	//retrieveResponse.Dump();
	
	var mapper = BsonMapper.Global;
	mapper.Entity<RetrieveResponseItem>().Id(x => x.ItemId);
	
	using (var db = new LiteDatabase(liteDbFile))
	{
		var firstItem = retrieveResponse.Items.First().Value;
		firstItem.Dump();

		var items = db.GetCollection<RetrieveResponseItem>();
		items.Insert(firstItem);
	}
}

// Define other methods and classes here
static RetrieveResponse DeserializeRetrieveResponse(string jsonFileName)
{
	var assembly = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.StartsWith("PocketApiV3"));
	var apiSerializerType = assembly.GetType("PocketApiV3.ApiSerializer");
	var instancePropertyInfo = apiSerializerType.GetField("Instance", BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public);
	object apiSerializer = instancePropertyInfo.GetValue(null);
	//apiSerializer.Dump();

	var deserializeMethodInfo = apiSerializerType.GetMethod("Deserialize", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public, null, CallingConventions.Any, new[] { typeof(string), typeof(Type) }, null);
	//deserializeMethodInfo.Dump();

	string retrieveResponseJson = File.ReadAllText(jsonFileName);
	var retrieveResponse = (RetrieveResponse)deserializeMethodInfo.Invoke(apiSerializer,
		new object[] { retrieveResponseJson, typeof(RetrieveResponse) });
	return retrieveResponse;
}