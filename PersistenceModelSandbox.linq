<Query Kind="Program">
  <NuGetReference>Bogus</NuGetReference>
  <NuGetReference>LiteDB</NuGetReference>
  <NuGetReference>PocketApiV3</NuGetReference>
  <Namespace>Bogus</Namespace>
  <Namespace>Bogus.DataSets</Namespace>
  <Namespace>Bogus.Extensions</Namespace>
  <Namespace>Bogus.Extensions.Extras</Namespace>
  <Namespace>Bogus.Extensions.UnitedStates</Namespace>
  <Namespace>LiteDB</Namespace>
  <Namespace>LiteDB.Shell</Namespace>
  <Namespace>PocketApiV3</Namespace>
</Query>

void Main()
{
	/* Seed 7:
		Titles:
			"Diverse needs-based challenge"
			"local" (partial)
		Tags:
			"salmon"
	*/
	Bogus.Randomizer.Seed = new System.Random(7);

	Stopwatch stopwatch = null;
	Author.Fakes = Author.CreateFaker().Generate(20);
	
	//Item.Fakes = Item.CreateFaker().Generate(1000000);
	Item.Fakes = Item.CreateFaker().Generate(100);
	//Item.Fakes.Take(30).Dump();
	
	var bsonMapper = new BsonMapper();
	bsonMapper.Entity<Item>()
		.DbRef(x => x.Authors, bsonMapper.ResolveCollectionName(typeof(Author)));

	//bsonMapper.Entity<Author>()
	//	.DbRef(x => x.Items, bsonMapper.ResolveCollectionName(typeof(Item)));
	
	//const string DbStartingFileName = @"D:\LiteDB\PersistenceModelSandboxItems - 1000000.db";
	const string DbWorkingFileName = @"D:\LiteDB\PersistenceModelSandboxItems.db";
	//File.Copy(DbStartingFileName, DbWorkingFileName, true);
	
	using (var dbStream = new MemoryStream())
	//using (var dbStream = System.IO.File.Open(DbWorkingFileName, System.IO.FileMode.OpenOrCreate))
	using (var db = new LiteDatabase(dbStream, bsonMapper))
	using (var repository = new LiteRepository(db))
	{
		var single = repository.SingleOrDefault<Item>(Query.EQ(nameof(Item.Id), 1))vis;
		single.Dump();
		
//		stopwatch = Stopwatch.StartNew();
//		db.GetCollection<Author>().InsertBulk(Author.Fakes);
//		stopwatch.Elapsed.Dump("Finished Authors InsertBulk()");
//
//		stopwatch = Stopwatch.StartNew();
//		db.GetCollection<Item>().InsertBulk(Item.Fakes);
//		stopwatch.Elapsed.Dump("Finished Items InsertBulk()");
//
//		repository
//			.Query<Item>()
//			.Include(x => x.Authors)
//			.ToList().Dump();
//
//		repository
//			.Query<Author>()
//			.ToList().Dump();
		
		//authorCollection.Query<Author>().ToList().Dump();
//		stopwatch = Stopwatch.StartNew();
//		itemCollection.EnsureIndex(x => x.Title);
//		//itemCollection.EnsureIndex(x => x.Tags);
//		stopwatch.Elapsed.Dump("Finished EnsureIndex()");

/*
		stopwatch = Stopwatch.StartNew();
		//var matchesByTitle = itemCollection.Find(Query.Contains("Title", "local")).ToArray();
		var matchesByTitle = itemCollection.Find(Query.EQ("Title", "Diverse needs-based challenge")).ToArray();
		//var matchesByTitle = itemCollection.Find(x => x.Title.IndexOf("Local", StringComparison.OrdinalIgnoreCase) != -1).ToArray();
		stopwatch.Elapsed.Dump($"Finished Find() => {matchesByTitle.Length}");
		//matchesByTitle.Dump();
		*/
		
//		stopwatch = Stopwatch.StartNew();
//		var matches = itemCollection.Find(Query.Contains("Tags", "salmon")).ToArray();
//		stopwatch.Elapsed.Dump($"Finished Find() => {matches.Length}");
	}
}

// Define other methods and classes here

public class Item
{
	public long Id { get; set; }
	public string Title { get; set; }
	public List<string> Tags { get; set; }
	public List<Author> Authors { get; set;}
	
	public static Faker<Item> CreateFaker()
	{
		int GetRandomAuthorCount(Bogus.Faker bogusFaker)
		{
			var x = bogusFaker.Random.Double();
			if (x <= 0.5)
				return 0;
			if (x <= 0.95)
				return 1;
			return 2;
		}

		long nextId = 1;
		//var authorFaker = Author.CreateFaker();
		var faker = new Faker<Item>()
			.CustomInstantiator(f => new Item()
			{
				Id = nextId++,
				Title = f.Company.CatchPhrase(),
				Tags = new List<string>(f.Random.WordsArray(0, 3)),
				Authors = f.Random.ListItems(Author.Fakes, GetRandomAuthorCount(f)).ToList()
				//Authors = authorFaker.Generate(GetRandomAuthorCount(f))
			});
			
		return faker;
	}
	
	public static List<Item> Fakes;
}

public class Author
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Url { get; set; }

	public static Faker<Author> CreateFaker()
	{
		long nextId = 1;
		return new Faker<Author>()
			.CustomInstantiator(f =>
				new Author
				{
					Id = nextId++,
					Name = f.Person.FullName,
					Url = f.Internet.Url()
				});
	}

	public static List<Author> Fakes;
}